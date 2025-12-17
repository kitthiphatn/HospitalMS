-- ===================================================================
-- Hospital Management System - Database Schema
-- ===================================================================
-- สคริปต์นี้จะสร้างฐานข้อมูลและตารางทั้งหมดสำหรับระบบบริหารโรงพยาบาล
-- ===================================================================

-- สร้างฐานข้อมูล
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HospitalDB')
BEGIN
    CREATE DATABASE HospitalDB;
    PRINT 'สร้างฐานข้อมูล HospitalDB เรียบร้อย';
END
GO

USE HospitalDB;
GO

-- ===================================================================
-- 1. ตาราง Roles (บทบาทผู้ใช้งาน)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    CREATE TABLE Roles (
        RoleID INT PRIMARY KEY IDENTITY(1,1),
        RoleName NVARCHAR(50) NOT NULL UNIQUE,
        Description NVARCHAR(200),
        CreatedDate DATETIME DEFAULT GETDATE()
    );
    PRINT 'สร้างตาราง Roles เรียบร้อย';
END
GO

-- ===================================================================
-- 2. ตาราง Users (ผู้ใช้งานระบบ)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        FullName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100),
        Phone NVARCHAR(20),
        RoleID INT NOT NULL,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        LastLogin DATETIME,
        CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
    );
    PRINT 'สร้างตาราง Users เรียบร้อย';
END
GO

-- ===================================================================
-- 3. ตาราง Patients (ผู้ป่วย)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Patients')
BEGIN
    CREATE TABLE Patients (
        PatientID INT PRIMARY KEY IDENTITY(1,1),
        PatientCode NVARCHAR(20) NOT NULL UNIQUE,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        DateOfBirth DATE NOT NULL,
        Gender NVARCHAR(10) NOT NULL CHECK (Gender IN (N'ชาย', N'หญิง', N'ไม่ระบุ')),
        BloodGroup NVARCHAR(5) CHECK (BloodGroup IN ('A', 'B', 'AB', 'O', 'A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')),
        Phone NVARCHAR(20),
        Email NVARCHAR(100),
        Address NVARCHAR(300),
        EmergencyContact NVARCHAR(100),
        EmergencyPhone NVARCHAR(20),
        Allergies NVARCHAR(500),
        MedicalHistory NVARCHAR(MAX),
        RegistrationDate DATETIME DEFAULT GETDATE(),
        IsActive BIT DEFAULT 1,
        CreatedBy INT,
        CreatedDate DATETIME DEFAULT GETDATE(),
        ModifiedBy INT,
        ModifiedDate DATETIME,
        CONSTRAINT FK_Patients_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
        CONSTRAINT FK_Patients_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
    );
    PRINT 'สร้างตาราง Patients เรียบร้อย';
END
GO

-- ===================================================================
-- 4. ตาราง Doctors (แพทย์)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Doctors')
BEGIN
    CREATE TABLE Doctors (
        DoctorID INT PRIMARY KEY IDENTITY(1,1),
        DoctorCode NVARCHAR(20) NOT NULL UNIQUE,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Specialization NVARCHAR(100) NOT NULL,
        Qualification NVARCHAR(200),
        Phone NVARCHAR(20),
        Email NVARCHAR(100),
        LicenseNumber NVARCHAR(50),
        ConsultationFee DECIMAL(10,2) DEFAULT 0,
        IsAvailable BIT DEFAULT 1,
        JoinDate DATE DEFAULT GETDATE(),
        CreatedDate DATETIME DEFAULT GETDATE(),
        IsActive BIT DEFAULT 1
    );
    PRINT 'สร้างตาราง Doctors เรียบร้อย';
END
GO

-- ===================================================================
-- 5. ตาราง Appointments (นัดหมาย)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Appointments')
BEGIN
    CREATE TABLE Appointments (
        AppointmentID INT PRIMARY KEY IDENTITY(1,1),
        PatientID INT NOT NULL,
        DoctorID INT NOT NULL,
        AppointmentDate DATE NOT NULL,
        AppointmentTime TIME NOT NULL,
        Status NVARCHAR(20) DEFAULT N'รอยืนยัน' CHECK (Status IN (N'รอยืนยัน', N'ยืนยันแล้ว', N'เสร็จสิ้น', N'ยกเลิก')),
        Reason NVARCHAR(300),
        Notes NVARCHAR(500),
        CreatedBy INT,
        CreatedDate DATETIME DEFAULT GETDATE(),
        ModifiedDate DATETIME,
        CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
        CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
        CONSTRAINT FK_Appointments_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
    );
    PRINT 'สร้างตาราง Appointments เรียบร้อย';
END
GO

-- ===================================================================
-- 6. ตาราง MedicalRecords (บันทึกการรักษา)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicalRecords')
BEGIN
    CREATE TABLE MedicalRecords (
        RecordID INT PRIMARY KEY IDENTITY(1,1),
        PatientID INT NOT NULL,
        DoctorID INT NOT NULL,
        AppointmentID INT,
        VisitDate DATETIME DEFAULT GETDATE(),
        Symptoms NVARCHAR(500),
        Diagnosis NVARCHAR(500),
        Treatment NVARCHAR(MAX),
        Prescription NVARCHAR(MAX),
        FollowUpDate DATE,
        Notes NVARCHAR(MAX),
        CreatedBy INT,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_MedicalRecords_Patients FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
        CONSTRAINT FK_MedicalRecords_Doctors FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
        CONSTRAINT FK_MedicalRecords_Appointments FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID),
        CONSTRAINT FK_MedicalRecords_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
    );
    PRINT 'สร้างตาราง MedicalRecords เรียบร้อย';
END
GO

-- ===================================================================
-- 7. ตาราง Medicines (ยา)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Medicines')
BEGIN
    CREATE TABLE Medicines (
        MedicineID INT PRIMARY KEY IDENTITY(1,1),
        MedicineName NVARCHAR(200) NOT NULL,
        Category NVARCHAR(100),
        Manufacturer NVARCHAR(200),
        UnitPrice DECIMAL(10,2) NOT NULL,
        StockQuantity INT DEFAULT 0,
        ReorderLevel INT DEFAULT 10,
        ExpiryDate DATE,
        Description NVARCHAR(500),
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
    PRINT 'สร้างตาราง Medicines เรียบร้อย';
END
GO

-- ===================================================================
-- 8. ตาราง Prescriptions (ใบสั่งยา)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Prescriptions')
BEGIN
    CREATE TABLE Prescriptions (
        PrescriptionID INT PRIMARY KEY IDENTITY(1,1),
        RecordID INT NOT NULL,
        MedicineID INT NOT NULL,
        Dosage NVARCHAR(100),
        Frequency NVARCHAR(100),
        Duration NVARCHAR(50),
        Quantity INT NOT NULL,
        Instructions NVARCHAR(300),
        CreatedDate DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Prescriptions_MedicalRecords FOREIGN KEY (RecordID) REFERENCES MedicalRecords(RecordID),
        CONSTRAINT FK_Prescriptions_Medicines FOREIGN KEY (MedicineID) REFERENCES Medicines(MedicineID)
    );
    PRINT 'สร้างตาราง Prescriptions เรียบร้อย';
END
GO

-- ===================================================================
-- 9. ตาราง Billing (การเรียกเก็บเงิน)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Billing')
BEGIN
    CREATE TABLE Billing (
        BillID INT PRIMARY KEY IDENTITY(1,1),
        PatientID INT NOT NULL,
        AppointmentID INT,
        BillDate DATETIME DEFAULT GETDATE(),
        TotalAmount DECIMAL(10,2) NOT NULL,
        PaidAmount DECIMAL(10,2) DEFAULT 0,
        BalanceAmount AS (TotalAmount - PaidAmount) PERSISTED,
        PaymentStatus NVARCHAR(20) DEFAULT N'ยังไม่จ่าย' CHECK (PaymentStatus IN (N'จ่ายแล้ว', N'จ่ายบางส่วน', N'ยังไม่จ่าย')),
        PaymentMethod NVARCHAR(50),
        CreatedBy INT,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Billing_Patients FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
        CONSTRAINT FK_Billing_Appointments FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID),
        CONSTRAINT FK_Billing_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
    );
    PRINT 'สร้างตาราง Billing เรียบร้อย';
END
GO

-- ===================================================================
-- 10. ตาราง BillDetails (รายละเอียดค่าใช้จ่าย)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BillDetails')
BEGIN
    CREATE TABLE BillDetails (
        BillDetailID INT PRIMARY KEY IDENTITY(1,1),
        BillID INT NOT NULL,
        ServiceType NVARCHAR(100) NOT NULL,
        Description NVARCHAR(300),
        Quantity INT DEFAULT 1,
        UnitPrice DECIMAL(10,2) NOT NULL,
        Amount AS (Quantity * UnitPrice) PERSISTED,
        CONSTRAINT FK_BillDetails_Billing FOREIGN KEY (BillID) REFERENCES Billing(BillID)
    );
    PRINT 'สร้างตาราง BillDetails เรียบร้อย';
END
GO

-- ===================================================================
-- 11. ตาราง ActivityLogs (บันทึกการใช้งาน)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ActivityLogs')
BEGIN
    CREATE TABLE ActivityLogs (
        LogID INT PRIMARY KEY IDENTITY(1,1),
        UserID INT,
        Action NVARCHAR(200) NOT NULL,
        TableName NVARCHAR(100),
        RecordID INT,
        Details NVARCHAR(MAX),
        IPAddress NVARCHAR(50),
        CreatedDate DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_ActivityLogs_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
    );
    PRINT 'สร้างตาราง ActivityLogs เรียบร้อย';
END
GO

-- ===================================================================
-- สร้าง Indexes เพื่อเพิ่มประสิทธิภาพ
-- ===================================================================
CREATE NONCLUSTERED INDEX IX_Patients_PatientCode ON Patients(PatientCode);
CREATE NONCLUSTERED INDEX IX_Patients_Phone ON Patients(Phone);
CREATE NONCLUSTERED INDEX IX_Doctors_DoctorCode ON Doctors(DoctorCode);
CREATE NONCLUSTERED INDEX IX_Appointments_Date ON Appointments(AppointmentDate);
CREATE NONCLUSTERED INDEX IX_Appointments_Status ON Appointments(Status);
CREATE NONCLUSTERED INDEX IX_Billing_PaymentStatus ON Billing(PaymentStatus);
GO

PRINT '===================================================================';
PRINT 'สร้างฐานข้อมูลและตารางทั้งหมดเรียบร้อยแล้ว!';
PRINT '===================================================================';
