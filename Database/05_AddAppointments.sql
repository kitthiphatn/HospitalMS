-- ===================================================================
-- เพิ่มข้อมูล Appointments (แก้ไข FOREIGN KEY Error)
-- ===================================================================

USE HospitalDB;
GO

-- ลบข้อมูล Appointments เดิม
DELETE FROM Appointments;
GO

-- เพิ่มนัดหมาย 5 รายการ (ใช้ PatientCode แทน PatientID)
INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, CreatedBy)
SELECT p.PatientID, 1, '2024-12-18', '09:00', N'ยืนยันแล้ว', N'ตรวจสุขภาพประจำปี', 1
FROM Patients p WHERE p.PatientCode = N'P2024001'
UNION ALL
SELECT p.PatientID, 2, '2024-12-18', '10:30', N'ยืนยันแล้ว', N'ปวดท้อง', 1
FROM Patients p WHERE p.PatientCode = N'P2024002'
UNION ALL
SELECT p.PatientID, 3, '2024-12-19', '14:00', N'รอยืนยัน', N'ไข้สูง', 1
FROM Patients p WHERE p.PatientCode = N'P2024003'
UNION ALL
SELECT p.PatientID, 4, '2024-12-20', '11:00', N'ยืนยันแล้ว', N'ตรวจครรภ์', 1
FROM Patients p WHERE p.PatientCode = N'P2024004'
UNION ALL
SELECT p.PatientID, 5, '2024-12-21', '15:30', N'ยืนยันแล้ว', N'ปวดหลัง', 1
FROM Patients p WHERE p.PatientCode = N'P2024005';

PRINT 'เพิ่มข้อมูล Appointments เรียบร้อย (5 รายการ)';
GO

-- ตรวจสอบผลลัพธ์
SELECT 'Patients' AS TableName, COUNT(*) AS RecordCount FROM Patients
UNION ALL
SELECT 'Appointments', COUNT(*) FROM Appointments;
GO

-- แสดงข้อมูลนัดหมาย
SELECT 
    a.AppointmentDate,
    a.AppointmentTime,
    p.PatientCode,
    p.FirstName + ' ' + p.LastName AS PatientName,
    d.FirstName + ' ' + d.LastName AS DoctorName,
    a.Status,
    a.Reason
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
ORDER BY a.AppointmentDate, a.AppointmentTime;
GO

PRINT '===================================================================';
PRINT 'เพิ่มข้อมูล Appointments สำเร็จแล้ว!';
PRINT '===================================================================';
