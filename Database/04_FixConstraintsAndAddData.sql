-- ===================================================================
-- แก้ไข CHECK CONSTRAINT และเพิ่มข้อมูล Patients และ Appointments
-- ===================================================================

USE HospitalDB;
GO

-- ลบ CHECK CONSTRAINT เดิมที่มีปัญหา
ALTER TABLE Patients DROP CONSTRAINT CK__Patients__Gender__5535A963;
ALTER TABLE Appointments DROP CONSTRAINT CK__Appointme__Statu__66603565;
GO

-- สร้าง CHECK CONSTRAINT ใหม่
ALTER TABLE Patients 
ADD CONSTRAINT CK_Patients_Gender CHECK (Gender IN (N'ชาย', N'หญิง', N'ไม่ระบุ'));

ALTER TABLE Appointments 
ADD CONSTRAINT CK_Appointments_Status CHECK (Status IN (N'รอยืนยัน', N'ยืนยันแล้ว', N'เสร็จสิ้น', N'ยกเลิก'));
GO

PRINT 'แก้ไข CHECK CONSTRAINT เรียบร้อย';
GO

-- ลบข้อมูลเดิม (ถ้ามี)
DELETE FROM Appointments;
DELETE FROM Patients;
GO

-- เพิ่มผู้ป่วย 5 คน
INSERT INTO Patients (PatientCode, FirstName, LastName, DateOfBirth, Gender, BloodGroup, Phone, Email, Address, EmergencyContact, EmergencyPhone, Allergies)
VALUES 
(N'P2024001', N'วิชัย', N'มั่งมี', '1985-05-15', N'ชาย', 'A+', '081-111-1111', 'vichai@email.com', N'123 ถ.สุขุมวิท กรุงเทพฯ', N'สมหญิง มั่งมี', '081-111-1112', N'แพ้เพนนิซิลิน'),
(N'P2024002', N'สุดา', N'ร่ำรวย', '1990-08-20', N'หญิง', 'B+', '082-222-2222', 'suda@email.com', N'456 ถ.พระราม 4 กรุงเทพฯ', N'สมชาย ร่ำรวย', '082-222-2223', NULL),
(N'P2024003', N'ประเสริฐ', N'ดีงาม', '1978-12-10', N'ชาย', 'O+', '083-333-3333', 'prasert@email.com', N'789 ถ.ลาดพร้าว กรุงเทพฯ', N'มาลี ดีงาม', '083-333-3334', N'แพ้นม'),
(N'P2024004', N'มาลี', N'สวยงาม', '1995-03-25', N'หญิง', 'AB+', '084-444-4444', 'malee@email.com', N'321 ถ.รัชดา กรุงเทพฯ', N'สมศักดิ์ สวยงาม', '084-444-4445', NULL),
(N'P2024005', N'สมบูรณ์', N'แข็งแรง', '1982-07-30', N'ชาย', 'A-', '085-555-5555', 'somboon@email.com', N'654 ถ.พหลโยธิน กรุงเทพฯ', N'สมใจ แข็งแรง', '085-555-5556', N'แพ้กุ้ง, แพ้แอสไพริน');

PRINT 'เพิ่มข้อมูล Patients เรียบร้อย (5 คน)';
GO

-- เพิ่มนัดหมาย 5 รายการ
INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, CreatedBy)
VALUES 
(1, 1, '2024-12-18', '09:00', N'ยืนยันแล้ว', N'ตรวจสุขภาพประจำปี', 1),
(2, 2, '2024-12-18', '10:30', N'ยืนยันแล้ว', N'ปวดท้อง', 1),
(3, 3, '2024-12-19', '14:00', N'รอยืนยัน', N'ไข้สูง', 1),
(4, 4, '2024-12-20', '11:00', N'ยืนยันแล้ว', N'ตรวจครรภ์', 1),
(5, 5, '2024-12-21', '15:30', N'ยืนยันแล้ว', N'ปวดหลัง', 1);

PRINT 'เพิ่มข้อมูล Appointments เรียบร้อย (5 รายการ)';
GO

-- ตรวจสอบผลลัพธ์
SELECT 'Patients' AS TableName, COUNT(*) AS RecordCount FROM Patients
UNION ALL
SELECT 'Appointments', COUNT(*) FROM Appointments;
GO

-- แสดงข้อมูลผู้ป่วย
SELECT PatientCode, FirstName + ' ' + LastName AS FullName, Gender, BloodGroup 
FROM Patients;
GO

PRINT '===================================================================';
PRINT 'เพิ่มข้อมูลทดสอบเรียบร้อยแล้ว!';
PRINT '===================================================================';
