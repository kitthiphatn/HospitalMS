-- ============================================
-- Billing Security Enhancements
-- ============================================
-- This script adds security features to the Billing system
-- Run this AFTER 13_Create_Billing_Tables.sql

USE HospitalDB;
GO

-- 1. Add User Tracking columns to Payments table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Payments') AND name = 'CreatedBy')
BEGIN
    ALTER TABLE Payments ADD CreatedBy NVARCHAR(100) NULL;
    PRINT '✓ Added CreatedBy column to Payments';
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Payments') AND name = 'VoidedBy')
BEGIN
    ALTER TABLE Payments ADD VoidedBy NVARCHAR(100) NULL;
    PRINT '✓ Added VoidedBy column to Payments';
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Payments') AND name = 'VoidedDate')
BEGIN
    ALTER TABLE Payments ADD VoidedDate DATETIME NULL;
    PRINT '✓ Added VoidedDate column to Payments';
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Payments') AND name = 'VoidReason')
BEGIN
    ALTER TABLE Payments ADD VoidReason NVARCHAR(500) NULL;
    PRINT '✓ Added VoidReason column to Payments';
END
GO

-- 2. Create Payment Audit Log table
IF OBJECT_ID('PaymentAuditLog', 'U') IS NULL
BEGIN
    CREATE TABLE PaymentAuditLog (
        AuditID INT PRIMARY KEY IDENTITY(1,1),
        PaymentID INT NOT NULL,
        InvoiceID INT NOT NULL,
        Action NVARCHAR(50) NOT NULL, -- 'CREATE', 'VOID', 'MODIFY'
        OldAmount DECIMAL(18,2) NULL,
        NewAmount DECIMAL(18,2) NULL,
        OldStatus NVARCHAR(50) NULL,
        NewStatus NVARCHAR(50) NULL,
        PerformedBy NVARCHAR(100) NOT NULL,
        PerformedDate DATETIME NOT NULL DEFAULT GETDATE(),
        Notes NVARCHAR(500) NULL,
        FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
        FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID)
    );
    PRINT '✓ Created PaymentAuditLog table';
END
GO

-- 3. Create Receipt Numbers table
IF OBJECT_ID('ReceiptNumbers', 'U') IS NULL
BEGIN
    CREATE TABLE ReceiptNumbers (
        ReceiptID INT PRIMARY KEY IDENTITY(1,1),
        ReceiptNumber NVARCHAR(50) NOT NULL UNIQUE,
        PaymentID INT NOT NULL,
        InvoiceID INT NOT NULL,
        GeneratedDate DATETIME NOT NULL DEFAULT GETDATE(),
        PrintedCount INT NOT NULL DEFAULT 0,
        LastPrintedDate DATETIME NULL,
        FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
        FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID)
    );
    PRINT '✓ Created ReceiptNumbers table';
END
GO

-- 4. Create stored procedure for safe payment recording
IF OBJECT_ID('sp_RecordPayment', 'P') IS NOT NULL
    DROP PROCEDURE sp_RecordPayment;
GO

CREATE PROCEDURE sp_RecordPayment
    @InvoiceID INT,
    @PaymentDate DATETIME,
    @PaymentMethod NVARCHAR(50),
    @Amount DECIMAL(18,2),
    @ReferenceNumber NVARCHAR(100) = NULL,
    @InsuranceProvider NVARCHAR(200) = NULL,
    @InsuranceClaimNumber NVARCHAR(100) = NULL,
    @SocialSecurityNumber NVARCHAR(50) = NULL,
    @ApprovalCode NVARCHAR(50) = NULL,
    @Notes NVARCHAR(500) = NULL,
    @CreatedBy NVARCHAR(100),
    @PaymentID INT OUTPUT,
    @ReceiptNumber NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @TotalAmount DECIMAL(18,2);
    DECLARE @PaidAmount DECIMAL(18,2);
    DECLARE @Balance DECIMAL(18,2);
    DECLARE @NewPaidAmount DECIMAL(18,2);
    DECLARE @NewStatus NVARCHAR(50);
    DECLARE @OldStatus NVARCHAR(50);
    DECLARE @ErrorMessage NVARCHAR(500);
    
    -- Start Transaction
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Get current invoice data
        SELECT 
            @TotalAmount = TotalAmount,
            @PaidAmount = PaidAmount,
            @Balance = TotalAmount - PaidAmount,
            @OldStatus = Status
        FROM Invoices WITH (UPDLOCK)
        WHERE InvoiceID = @InvoiceID AND IsActive = 1;
        
        -- Validate
        IF @TotalAmount IS NULL
        BEGIN
            SET @ErrorMessage = 'Invoice not found or inactive';
            RAISERROR(@ErrorMessage, 16, 1);
        END
        
        IF @Amount <= 0
        BEGIN
            SET @ErrorMessage = 'Payment amount must be greater than zero';
            RAISERROR(@ErrorMessage, 16, 1);
        END
        
        IF @Amount > @Balance
        BEGIN
            SET @ErrorMessage = 'Payment amount exceeds balance';
            RAISERROR(@ErrorMessage, 16, 1);
        END
        
        -- Insert Payment
        INSERT INTO Payments (
            InvoiceID, PaymentDate, PaymentMethod, Amount,
            ReferenceNumber, InsuranceProvider, InsuranceClaimNumber,
            SocialSecurityNumber, ApprovalCode, Notes, CreatedBy
        )
        VALUES (
            @InvoiceID, @PaymentDate, @PaymentMethod, @Amount,
            @ReferenceNumber, @InsuranceProvider, @InsuranceClaimNumber,
            @SocialSecurityNumber, @ApprovalCode, @Notes, @CreatedBy
        );
        
        SET @PaymentID = SCOPE_IDENTITY();
        
        -- Calculate new status
        SET @NewPaidAmount = @PaidAmount + @Amount;
        
        IF @NewPaidAmount >= @TotalAmount
            SET @NewStatus = 'Paid';
        ELSE IF @NewPaidAmount > 0
            SET @NewStatus = 'Partial';
        ELSE
            SET @NewStatus = 'Unpaid';
        
        -- Update Invoice
        UPDATE Invoices
        SET PaidAmount = @NewPaidAmount,
            Status = @NewStatus,
            ModifiedDate = GETDATE()
        WHERE InvoiceID = @InvoiceID;
        
        -- Generate Receipt Number
        DECLARE @DatePrefix NVARCHAR(10) = FORMAT(GETDATE(), 'yyyyMMdd');
        DECLARE @SequenceNumber INT;
        
        SELECT @SequenceNumber = ISNULL(MAX(CAST(RIGHT(ReceiptNumber, 4) AS INT)), 0) + 1
        FROM ReceiptNumbers
        WHERE ReceiptNumber LIKE 'RCP-' + @DatePrefix + '-%';
        
        SET @ReceiptNumber = 'RCP-' + @DatePrefix + '-' + RIGHT('0000' + CAST(@SequenceNumber AS NVARCHAR), 4);
        
        INSERT INTO ReceiptNumbers (ReceiptNumber, PaymentID, InvoiceID)
        VALUES (@ReceiptNumber, @PaymentID, @InvoiceID);
        
        -- Log to Audit
        INSERT INTO PaymentAuditLog (
            PaymentID, InvoiceID, Action, NewAmount, OldStatus, NewStatus,
            PerformedBy, Notes
        )
        VALUES (
            @PaymentID, @InvoiceID, 'CREATE', @Amount, @OldStatus, @NewStatus,
            @CreatedBy, 'Payment recorded'
        );
        
        -- Commit Transaction
        COMMIT TRANSACTION;
        
        PRINT '✓ Payment recorded successfully';
        PRINT '  Payment ID: ' + CAST(@PaymentID AS NVARCHAR);
        PRINT '  Receipt Number: ' + @ReceiptNumber;
        
    END TRY
    BEGIN CATCH
        -- Rollback on error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH
END
GO

PRINT '';
PRINT '============================================';
PRINT '✓ Billing Security Enhancements Complete!';
PRINT '============================================';
PRINT '';
PRINT 'New Features:';
PRINT '  1. User Tracking (CreatedBy, VoidedBy)';
PRINT '  2. Payment Audit Log';
PRINT '  3. Receipt Number Generation';
PRINT '  4. Transaction Safety (sp_RecordPayment)';
PRINT '';
PRINT 'Next Steps:';
PRINT '  - Update C# code to use sp_RecordPayment';
PRINT '  - Implement Void Payment functionality';
PRINT '  - Add Receipt printing';
GO
