-- ===================================
-- สร้างตาราง Billing Management Module
-- ===================================

USE HospitalDB;
GO

-- ===================================
-- DROP ตารางตามลำดับที่ถูกต้อง (ลบ child tables ก่อน)
-- ===================================

-- Drop child tables first
IF OBJECT_ID('MedicalCertificates', 'U') IS NOT NULL
    DROP TABLE MedicalCertificates;
GO

IF OBJECT_ID('Payments', 'U') IS NOT NULL
    DROP TABLE Payments;
GO

IF OBJECT_ID('InvoiceItems', 'U') IS NOT NULL
    DROP TABLE InvoiceItems;
GO

-- Drop parent table last
IF OBJECT_ID('Invoices', 'U') IS NOT NULL
    DROP TABLE Invoices;
GO

-- ===================================
-- 1. ตาราง Invoices (ใบแจ้งหนี้)
-- ===================================

CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceNumber NVARCHAR(50) UNIQUE NOT NULL,     -- เลขที่ใบแจ้งหนี้ (INV-YYYYMMDD-XXXX)
    PatientID INT NOT NULL,
    AppointmentID INT,                               -- อ้างอิงจาก Appointment (ถ้ามี)
    InvoiceDate DATETIME NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME,                                -- วันครบกำหนดชำระ
    SubTotal DECIMAL(10,2) NOT NULL DEFAULT 0,       -- ยอดรวมก่อน VAT
    TaxAmount DECIMAL(10,2) NOT NULL DEFAULT 0,      -- ภาษี VAT
    DiscountAmount DECIMAL(10,2) NOT NULL DEFAULT 0, -- ส่วนลด
    TotalAmount DECIMAL(10,2) NOT NULL DEFAULT 0,    -- ยอดรวมสุทธิ
    PaidAmount DECIMAL(10,2) NOT NULL DEFAULT 0,     -- ยอดที่ชำระแล้ว
    Status NVARCHAR(50) NOT NULL DEFAULT 'Unpaid',   -- Unpaid/Partial/Paid/Cancelled
    Notes NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedBy INT,                                   -- UserID ผู้สร้าง
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID)
);
GO

-- ===================================
-- 2. ตาราง InvoiceItems (รายการในใบแจ้งหนี้)
-- ===================================

CREATE TABLE InvoiceItems (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT NOT NULL,
    ItemType NVARCHAR(50) NOT NULL,                  -- Service/Medicine/Lab/Other
    ItemDescription NVARCHAR(500) NOT NULL,          -- รายละเอียด
    Quantity INT NOT NULL DEFAULT 1,
    UnitPrice DECIMAL(10,2) NOT NULL,
    DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0, -- ส่วนลดต่อรายการ (%)
    Amount DECIMAL(10,2) NOT NULL,                   -- Quantity * UnitPrice * (1 - DiscountPercent/100)
    MedicineID INT,                                  -- ถ้าเป็นยา
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID),
    FOREIGN KEY (MedicineID) REFERENCES Medicines(MedicineID)
);
GO

-- ===================================
-- 3. ตาราง Payments (การชำระเงิน)
-- ===================================

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) NOT NULL,             -- Cash/Credit Card/Debit Card/Bank Transfer/PromptPay/Social Security/Health Insurance
    Amount DECIMAL(10,2) NOT NULL,
    ReferenceNumber NVARCHAR(100),                   -- เลขที่อ้างอิง (เช่น เลขที่โอน, Transaction ID)
    InsuranceProvider NVARCHAR(200),                 -- บริษัทประกัน (ถ้าเป็นประกันสุขภาพ)
    InsuranceClaimNumber NVARCHAR(100),              -- เลขที่เคลม (ถ้าเป็นประกัน)
    SocialSecurityNumber NVARCHAR(50),               -- เลขประกันสังคม (ถ้าเป็นประกันสังคม)
    ApprovalCode NVARCHAR(50),                       -- รหัสอนุมัติ (สำหรับบัตรเครดิต)
    Notes NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedBy INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID)
);
GO

-- ===================================
-- 4. ตาราง MedicalCertificates (ใบรับรองแพทย์)
-- ===================================

CREATE TABLE MedicalCertificates (
    CertificateID INT PRIMARY KEY IDENTITY(1,1),
    CertificateNumber NVARCHAR(50) UNIQUE NOT NULL,  -- MC-YYYYMMDD-XXXX
    PatientID INT NOT NULL,
    RecordID INT,                                    -- อ้างอิงจาก MedicalRecords
    IssueDate DATETIME NOT NULL DEFAULT GETDATE(),
    Diagnosis NVARCHAR(500) NOT NULL,
    MedicalAdvice NVARCHAR(1000),
    SickLeaveDays INT,
    SickLeaveFrom DATE,
    SickLeaveTo DATE,
    DoctorID INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (RecordID) REFERENCES MedicalRecords(RecordID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);
GO

PRINT '✅ Tables created successfully!';
GO

-- ===================================
-- เพิ่มข้อมูลตัวอย่าง (ใช้ PatientID ที่มีอยู่จริง)
-- ===================================

-- ดึง PatientID และ DoctorID ที่มีอยู่จริง
DECLARE @Patient1 INT, @Patient2 INT, @Patient3 INT;
DECLARE @Doctor1 INT, @Doctor2 INT;
DECLARE @Appointment1 INT, @Appointment2 INT;

SELECT TOP 1 @Patient1 = PatientID FROM Patients WHERE IsActive = 1 ORDER BY PatientID;
SELECT @Patient2 = PatientID FROM Patients WHERE IsActive = 1 AND PatientID > @Patient1 ORDER BY PatientID OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;
SELECT @Patient3 = PatientID FROM Patients WHERE IsActive = 1 AND PatientID > @Patient2 ORDER BY PatientID OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;

SELECT TOP 1 @Doctor1 = DoctorID FROM Doctors ORDER BY DoctorID;
SELECT @Doctor2 = DoctorID FROM Doctors WHERE DoctorID > @Doctor1 ORDER BY DoctorID OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;

SELECT TOP 1 @Appointment1 = AppointmentID FROM Appointments ORDER BY AppointmentID;
SELECT @Appointment2 = AppointmentID FROM Appointments WHERE AppointmentID > @Appointment1 ORDER BY AppointmentID OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY;

-- Invoices ตัวอย่าง
IF @Patient1 IS NOT NULL
BEGIN
    INSERT INTO Invoices (InvoiceNumber, PatientID, AppointmentID, InvoiceDate, DueDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaidAmount, Status)
    VALUES 
    ('INV-20241219-0001', @Patient1, @Appointment1, '2024-12-19 09:00:00', '2024-12-26', 1000.00, 70.00, 0.00, 1070.00, 1070.00, 'Paid');
    
    IF @Patient2 IS NOT NULL
    BEGIN
        INSERT INTO Invoices (InvoiceNumber, PatientID, AppointmentID, InvoiceDate, DueDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaidAmount, Status)
        VALUES ('INV-20241219-0002', @Patient2, @Appointment2, '2024-12-19 10:30:00', '2024-12-26', 1500.00, 105.00, 100.00, 1505.00, 500.00, 'Partial');
    END
    
    IF @Patient3 IS NOT NULL
    BEGIN
        INSERT INTO Invoices (InvoiceNumber, PatientID, InvoiceDate, DueDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaidAmount, Status)
        VALUES ('INV-20241219-0003', @Patient3, '2024-12-19 14:00:00', '2024-12-26', 800.00, 56.00, 0.00, 856.00, 0.00, 'Unpaid');
    END
    
    PRINT '✅ Sample invoices created!';
END
ELSE
BEGIN
    PRINT '⚠️ No patients found. Skipping sample data.';
END
GO

PRINT '✅ Billing Management Module tables created successfully!';
