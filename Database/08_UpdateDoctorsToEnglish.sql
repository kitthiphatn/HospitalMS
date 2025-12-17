-- ===================================================================
-- Update Doctors to English Names
-- ===================================================================

USE HospitalDB;
GO

PRINT 'Updating Doctors to English...';
GO

-- Update Doctors ให้เป็นภาษาอังกฤษ
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

PRINT 'Doctors updated to English successfully!';
GO

-- ตรวจสอบผลลัพธ์
SELECT DoctorCode, 
       FirstName + ' ' + LastName AS DoctorName,
       Specialization, 
       ConsultationFee
FROM Doctors;
GO

PRINT '===================================================================';
PRINT 'All Doctors are now in English!';
PRINT '===================================================================';
