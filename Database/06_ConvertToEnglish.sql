-- ===================================================================
-- Fix Database - Convert to English for Better Compatibility
-- ===================================================================
-- This script will:
-- 1. Drop old CHECK constraints with Thai values
-- 2. Create new CHECK constraints with English values
-- 3. Update existing data to English
-- 4. Add sample data in English
-- ===================================================================

USE HospitalDB;
GO

PRINT 'Starting database conversion to English...';
GO

-- ===================================================================
-- Step 1: Drop old CHECK constraints
-- ===================================================================

-- Drop Patients Gender constraint
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Patients_Gender')
    ALTER TABLE Patients DROP CONSTRAINT CK_Patients_Gender;
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name LIKE 'CK__Patients__Gender%')
BEGIN
    DECLARE @sql1 NVARCHAR(MAX);
    SELECT @sql1 = 'ALTER TABLE Patients DROP CONSTRAINT ' + name 
    FROM sys.check_constraints WHERE name LIKE 'CK__Patients__Gender%';
    EXEC sp_executesql @sql1;
END

-- Drop Appointments Status constraint
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Appointments_Status')
    ALTER TABLE Appointments DROP CONSTRAINT CK_Appointments_Status;
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name LIKE 'CK__Appointme__Statu%')
BEGIN
    DECLARE @sql2 NVARCHAR(MAX);
    SELECT @sql2 = 'ALTER TABLE Appointments DROP CONSTRAINT ' + name 
    FROM sys.check_constraints WHERE name LIKE 'CK__Appointme__Statu%';
    EXEC sp_executesql @sql2;
END

-- Drop Billing PaymentStatus constraint
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name LIKE 'CK__Billing__Payment%')
BEGIN
    DECLARE @sql3 NVARCHAR(MAX);
    SELECT @sql3 = 'ALTER TABLE Billing DROP CONSTRAINT ' + name 
    FROM sys.check_constraints WHERE name LIKE 'CK__Billing__Payment%';
    EXEC sp_executesql @sql3;
END

PRINT 'Old CHECK constraints dropped successfully';
GO

-- ===================================================================
-- Step 2: Create new CHECK constraints with English values
-- ===================================================================

-- Patients Gender: Male, Female, Other
ALTER TABLE Patients 
ADD CONSTRAINT CK_Patients_Gender CHECK (Gender IN ('Male', 'Female', 'Other'));

-- Appointments Status: Pending, Confirmed, Completed, Cancelled
ALTER TABLE Appointments 
ADD CONSTRAINT CK_Appointments_Status CHECK (Status IN ('Pending', 'Confirmed', 'Completed', 'Cancelled'));

-- Billing PaymentStatus: Paid, Partial, Unpaid
ALTER TABLE Billing 
ADD CONSTRAINT CK_Billing_PaymentStatus CHECK (PaymentStatus IN ('Paid', 'Partial', 'Unpaid'));

PRINT 'New CHECK constraints created successfully';
GO

-- ===================================================================
-- Step 3: Update existing data to English
-- ===================================================================

-- Update Patients Gender
UPDATE Patients SET Gender = 'Male' WHERE Gender IN (N'ชาย', 'ชาย');
UPDATE Patients SET Gender = 'Female' WHERE Gender IN (N'หญิง', 'หญิง');
UPDATE Patients SET Gender = 'Other' WHERE Gender IN (N'ไม่ระบุ', 'ไม่ระบุ');

-- Update Appointments Status
UPDATE Appointments SET Status = 'Pending' WHERE Status IN (N'รอยืนยัน', 'รอยืนยัน');
UPDATE Appointments SET Status = 'Confirmed' WHERE Status IN (N'ยืนยันแล้ว', 'ยืนยันแล้ว');
UPDATE Appointments SET Status = 'Completed' WHERE Status IN (N'เสร็จสิ้น', 'เสร็จสิ้น');
UPDATE Appointments SET Status = 'Cancelled' WHERE Status IN (N'ยกเลิก', 'ยกเลิก');

-- Update Billing PaymentStatus
UPDATE Billing SET PaymentStatus = 'Paid' WHERE PaymentStatus IN (N'จ่ายแล้ว', 'จ่ายแล้ว');
UPDATE Billing SET PaymentStatus = 'Partial' WHERE PaymentStatus IN (N'จ่ายบางส่วน', 'จ่ายบางส่วน');
UPDATE Billing SET PaymentStatus = 'Unpaid' WHERE PaymentStatus IN (N'ยังไม่จ่าย', 'ยังไม่จ่าย');

PRINT 'Existing data updated to English successfully';
GO

-- ===================================================================
-- Step 4: Clear and add fresh sample data
-- ===================================================================

-- Clear existing data
DELETE FROM Appointments;
DELETE FROM Patients;
GO

-- Add Patients (5 people)
INSERT INTO Patients (PatientCode, FirstName, LastName, DateOfBirth, Gender, BloodGroup, Phone, Email, Address, EmergencyContact, EmergencyPhone, Allergies)
VALUES 
('P2024001', 'Vichai', 'Mangmee', '1985-05-15', 'Male', 'A+', '081-111-1111', 'vichai@email.com', '123 Sukhumvit Rd, Bangkok', 'Somying Mangmee', '081-111-1112', 'Penicillin'),
('P2024002', 'Suda', 'Ramruay', '1990-08-20', 'Female', 'B+', '082-222-2222', 'suda@email.com', '456 Rama 4 Rd, Bangkok', 'Somchai Ramruay', '082-222-2223', NULL),
('P2024003', 'Prasert', 'Deengam', '1978-12-10', 'Male', 'O+', '083-333-3333', 'prasert@email.com', '789 Ladprao Rd, Bangkok', 'Malee Deengam', '083-333-3334', 'Milk'),
('P2024004', 'Malee', 'Suayngam', '1995-03-25', 'Female', 'AB+', '084-444-4444', 'malee@email.com', '321 Ratchada Rd, Bangkok', 'Somsak Suayngam', '084-444-4445', NULL),
('P2024005', 'Somboon', 'Kaengrang', '1982-07-30', 'Male', 'A-', '085-555-5555', 'somboon@email.com', '654 Phahonyothin Rd, Bangkok', 'Somjai Kaengrang', '085-555-5556', 'Shrimp, Aspirin');

PRINT 'Sample Patients added successfully (5 records)';
GO

-- Add Appointments (5 records)
INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, CreatedBy)
SELECT p.PatientID, 1, '2024-12-18', '09:00', 'Confirmed', 'Annual health checkup', 1
FROM Patients p WHERE p.PatientCode = 'P2024001'
UNION ALL
SELECT p.PatientID, 2, '2024-12-18', '10:30', 'Confirmed', 'Stomach pain', 1
FROM Patients p WHERE p.PatientCode = 'P2024002'
UNION ALL
SELECT p.PatientID, 3, '2024-12-19', '14:00', 'Pending', 'High fever', 1
FROM Patients p WHERE p.PatientCode = 'P2024003'
UNION ALL
SELECT p.PatientID, 4, '2024-12-20', '11:00', 'Confirmed', 'Pregnancy checkup', 1
FROM Patients p WHERE p.PatientCode = 'P2024004'
UNION ALL
SELECT p.PatientID, 5, '2024-12-21', '15:30', 'Confirmed', 'Back pain', 1
FROM Patients p WHERE p.PatientCode = 'P2024005';

PRINT 'Sample Appointments added successfully (5 records)';
GO

-- ===================================================================
-- Step 5: Verify results
-- ===================================================================

PRINT '';
PRINT '===================================================================';
PRINT 'Database Conversion Summary:';
PRINT '===================================================================';

-- Count records
SELECT 'Patients' AS TableName, COUNT(*) AS RecordCount FROM Patients
UNION ALL
SELECT 'Appointments', COUNT(*) FROM Appointments;

PRINT '';
PRINT 'Sample Patients:';
SELECT PatientCode, FirstName + ' ' + LastName AS FullName, Gender, BloodGroup 
FROM Patients;

PRINT '';
PRINT 'Sample Appointments:';
SELECT 
    a.AppointmentDate,
    a.AppointmentTime,
    p.FirstName + ' ' + p.LastName AS PatientName,
    d.FirstName + ' ' + d.LastName AS DoctorName,
    a.Status,
    a.Reason
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
ORDER BY a.AppointmentDate, a.AppointmentTime;

PRINT '';
PRINT '===================================================================';
PRINT 'Database conversion completed successfully!';
PRINT 'All data now uses English values for better compatibility.';
PRINT '===================================================================';
