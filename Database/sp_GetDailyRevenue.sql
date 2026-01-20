-- ============================================
-- Stored Procedure: Daily Revenue Report
-- ============================================
-- Purpose: Calculate daily revenue statistics and transaction details
-- Author: Hospital Management System
-- Date: 2025-12-19

USE HospitalDB;
GO

-- Drop if exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'sp_GetDailyRevenue') AND type in (N'P', N'PC'))
    DROP PROCEDURE sp_GetDailyRevenue;
GO

CREATE PROCEDURE sp_GetDailyRevenue
    @ReportDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- ============================================
    -- Result Set 1: Summary Statistics
    -- ============================================
    SELECT 
        -- Invoice counts
        COUNT(DISTINCT i.InvoiceID) AS TotalInvoices,
        
        -- Revenue totals
        ISNULL(SUM(i.TotalAmount), 0) AS TotalRevenue,
        ISNULL(SUM(i.PaidAmount), 0) AS TotalPaid,
        ISNULL(SUM(i.TotalAmount - i.PaidAmount), 0) AS TotalOutstanding,
        
        -- Payment method breakdown (from payments made on this date)
        ISNULL(SUM(CASE WHEN p.PaymentMethod = 'Cash' THEN p.Amount ELSE 0 END), 0) AS CashPayments,
        ISNULL(SUM(CASE WHEN p.PaymentMethod LIKE '%Credit Card%' OR p.PaymentMethod LIKE '%Debit Card%' THEN p.Amount ELSE 0 END), 0) AS CardPayments,
        ISNULL(SUM(CASE WHEN p.PaymentMethod LIKE '%Transfer%' THEN p.Amount ELSE 0 END), 0) AS TransferPayments,
        ISNULL(SUM(CASE WHEN p.PaymentMethod LIKE '%Insurance%' OR p.PaymentMethod LIKE '%Social Security%' THEN p.Amount ELSE 0 END), 0) AS InsurancePayments,
        ISNULL(SUM(CASE 
            WHEN p.PaymentMethod NOT IN ('Cash') 
            AND p.PaymentMethod NOT LIKE '%Card%' 
            AND p.PaymentMethod NOT LIKE '%Transfer%'
            AND p.PaymentMethod NOT LIKE '%Insurance%'
            AND p.PaymentMethod NOT LIKE '%Social Security%'
            THEN p.Amount 
            ELSE 0 
        END), 0) AS OtherPayments
    FROM Invoices i
    LEFT JOIN Payments p ON i.InvoiceID = p.InvoiceID 
        AND CAST(p.PaymentDate AS DATE) = @ReportDate
    WHERE CAST(i.InvoiceDate AS DATE) = @ReportDate;
    
    -- ============================================
    -- Result Set 2: Detailed Transactions
    -- ============================================
    SELECT 
        i.InvoiceID,
        i.InvoiceNumber,
        pat.FirstName + ' ' + pat.LastName AS PatientName,
        i.InvoiceDate,
        i.TotalAmount,
        i.PaidAmount,
        (i.TotalAmount - i.PaidAmount) AS Balance,
        i.Status,
        STUFF((
            SELECT ', ' + PaymentMethod + ' (฿' + CAST(Amount AS VARCHAR(20)) + ')'
            FROM Payments 
            WHERE InvoiceID = i.InvoiceID
            FOR XML PATH('')
        ), 1, 2, '') AS PaymentMethods,
        (SELECT MAX(PaymentDate) FROM Payments WHERE InvoiceID = i.InvoiceID) AS LastPaymentDate
    FROM Invoices i
    INNER JOIN Patients pat ON i.PatientID = pat.PatientID
    WHERE CAST(i.InvoiceDate AS DATE) = @ReportDate
    ORDER BY i.InvoiceDate DESC, i.InvoiceNumber;
    
END
GO

PRINT '✓ Stored Procedure sp_GetDailyRevenue created successfully!';
GO

-- ============================================
-- Test the stored procedure
-- ============================================
PRINT '';
PRINT 'Testing sp_GetDailyRevenue with today date...';
GO

DECLARE @Today DATE = CAST(GETDATE() AS DATE);
EXEC sp_GetDailyRevenue @ReportDate = @Today;
GO

PRINT '';
PRINT '✓ Daily Revenue Report stored procedure is ready to use!';
