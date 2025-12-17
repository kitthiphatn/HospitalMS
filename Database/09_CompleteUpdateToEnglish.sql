-- ===================================================================
-- Update ALL Data to English (Complete Update Script)
-- ===================================================================
-- This script updates Users, Doctors, and verifies all data is in English
-- ===================================================================

USE HospitalDB;
GO

PRINT '===================================================================';
PRINT 'Starting Complete Database Update to English...';
PRINT '===================================================================';
GO

-- ===================================================================
-- 1. Update Users
-- ===================================================================
PRINT '';
PRINT '1. Updating Users...';

UPDATE Users SET FullName = 'System Administrator' WHERE Username = 'admin';
UPDATE Users SET FullName = 'Dr. Somchai Jaidee' WHERE Username = 'doctor1';
UPDATE Users SET FullName = 'Nurse Somying Raksa' WHERE Username = 'nurse1';
UPDATE Users SET FullName = 'Somsri Yimyam' WHERE Username = 'reception1';
UPDATE Users SET FullName = 'Pharmacist Somporn Saijai' WHERE Username = 'pharma1';

PRINT '   ✓ Users updated successfully!';
GO

-- ===================================================================
-- 2. Update Doctors
-- ===================================================================
PRINT '';
PRINT '2. Updating Doctors...';

UPDATE Doctors SET FirstName = 'Somporn', LastName = 'Chaiyaporn', Specialization = 'General Medicine' 
WHERE DoctorCode = 'DOC001';

UPDATE Doctors SET FirstName = 'Somchai', LastName = 'Raksa', Specialization = 'Pediatrics' 
WHERE DoctorCode = 'DOC002';

UPDATE Doctors SET FirstName = 'Somying', LastName = 'Saijai', Specialization = 'Cardiology' 
WHERE DoctorCode = 'DOC003';

UPDATE Doctors SET FirstName = 'Somsri', LastName = 'Deemak', Specialization = 'Obstetrics and Gynecology' 
WHERE DoctorCode = 'DOC004';

UPDATE Doctors SET FirstName = 'Sompong', LastName = 'Jaidee', Specialization = 'Orthopedics' 
WHERE DoctorCode = 'DOC005';

PRINT '   ✓ Doctors updated successfully!';
GO

-- ===================================================================
-- 3. Verification - Show All Updated Data
-- ===================================================================
PRINT '';
PRINT '===================================================================';
PRINT 'Verification Results:';
PRINT '===================================================================';
PRINT '';

PRINT 'Users:';
SELECT Username, FullName, Email FROM Users;
PRINT '';

PRINT 'Doctors:';
SELECT DoctorCode, 
       FirstName + ' ' + LastName AS DoctorName,
       Specialization, 
       ConsultationFee
FROM Doctors;
PRINT '';

PRINT 'Patients (Sample):';
SELECT TOP 3 PatientCode, FirstName + ' ' + LastName AS PatientName, Gender, BloodGroup 
FROM Patients;
PRINT '';

PRINT 'Appointments (Sample):';
SELECT TOP 3 
    a.AppointmentDate,
    a.AppointmentTime,
    p.FirstName + ' ' + p.LastName AS PatientName,
    a.Status,
    a.Reason
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
ORDER BY a.AppointmentDate;
PRINT '';

PRINT '===================================================================';
PRINT 'Database Update Complete!';
PRINT 'All data is now in English and ready for GitHub upload!';
PRINT '===================================================================';
