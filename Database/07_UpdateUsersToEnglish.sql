-- ===================================================================
-- Update Users FullName to English
-- ===================================================================

USE HospitalDB;
GO

-- Update FullName ให้เป็นภาษาอังกฤษ
UPDATE Users SET FullName = 'System Administrator' WHERE Username = 'admin';
UPDATE Users SET FullName = 'Dr. Somchai Jaidee' WHERE Username = 'doctor1';
UPDATE Users SET FullName = 'Nurse Somying Raksa' WHERE Username = 'nurse1';
UPDATE Users SET FullName = 'Somsri Yimyam' WHERE Username = 'reception1';
UPDATE Users SET FullName = 'Pharmacist Somporn Saijai' WHERE Username = 'pharma1';

PRINT 'Updated Users FullName to English successfully!';
GO

-- ตรวจสอบผลลัพธ์
SELECT Username, FullName, Email FROM Users;
GO

PRINT '===================================================================';
PRINT 'All Users FullName are now in English!';
PRINT '===================================================================';
