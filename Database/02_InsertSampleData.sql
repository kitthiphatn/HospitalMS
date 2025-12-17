-- ===================================================================
-- Hospital Management System - Sample Data
-- ===================================================================
-- สคริปต์นี้จะเพิ่มข้อมูลตัวอย่างเพื่อทดสอบระบบ
-- ===================================================================

USE HospitalDB;
GO

-- ===================================================================
-- 1. เพิ่มข้อมูล Roles (บทบาท)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM Roles WHERE RoleName = N'Admin')
BEGIN
    INSERT INTO Roles (RoleName, Description) VALUES
    (N'Admin', N'ผู้ดูแลระบบ - มีสิทธิ์เต็ม'),
    (N'Doctor', N'แพทย์ - ตรวจรักษาผู้ป่วย'),
    (N'Nurse', N'พยาบาล - ดูแลผู้ป่วย'),
    (N'Receptionist', N'พนักงานต้อนรับ - ลงทะเบียนผู้ป่วย'),
    (N'Pharmacist', N'เภสัชกร - จัดการยา');
    
    PRINT 'เพิ่มข้อมูล Roles เรียบร้อย';
END
GO

-- ===================================================================
-- 2. เพิ่มข้อมูล Users (ผู้ใช้งาน)
-- ===================================================================
-- Password: admin123 (ในระบบจริงต้อง Hash)
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, PasswordHash, FullName, Email, Phone, RoleID) VALUES
    ('admin', 'admin123', N'ผู้ดูแลระบบ', 'admin@hospital.com', '0812345678', 1),
    ('doctor1', 'doctor123', N'นพ.สมชาย ใจดี', 'somchai@hospital.com', '0823456789', 2),
    ('nurse1', 'nurse123', N'พย.สมหญิง รักษา', 'somying@hospital.com', '0834567890', 3),
    ('reception1', 'recep123', N'สมศรี ยิ้มแย้ม', 'somsri@hospital.com', '0845678901', 4),
    ('pharma1', 'pharma123', N'ภก.สมพร ใส่ใจ', 'somporn@hospital.com', '0856789012', 5);
    
    PRINT 'เพิ่มข้อมูล Users เรียบร้อย';
END
GO

-- ===================================================================
-- 3. เพิ่มข้อมูล Doctors (แพทย์)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM Doctors WHERE DoctorCode = 'DOC001')
BEGIN
    INSERT INTO Doctors (DoctorCode, FirstName, LastName, Specialization, Qualification, Phone, Email, LicenseNumber, ConsultationFee) VALUES
    ('DOC001', N'สมชาย', N'ใจดี', N'อายุรกรรม', N'แพทย์บอร์ดอายุรกรรม', '0823456789', 'somchai@hospital.com', 'MD12345', 1000.00),
    ('DOC002', N'สมหมาย', N'รักษา', N'ศัลยกรรม', N'แพทย์บอร์ดศัลยกรรม', '0823456780', 'sommai@hospital.com', 'MD12346', 1500.00),
    ('DOC003', N'สมใจ', N'ดูแล', N'กุมารเวชกรรม', N'แพทย์บอร์ดกุมารเวชกรรม', '0823456781', 'somjai@hospital.com', 'MD12347', 800.00),
    ('DOC004', N'สมศรี', N'เอาใจใส่', N'สูติ-นรีเวช', N'แพทย์บอร์ดสูติ-นรีเวช', '0823456782', 'somsri.d@hospital.com', 'MD12348', 1200.00),
    ('DOC005', N'สมพร', N'ช่วยเหลือ', N'ออร์โธปิดิกส์', N'แพทย์บอร์ดออร์โธปิดิกส์', '0823456783', 'somporn.d@hospital.com', 'MD12349', 1300.00);
    
    PRINT 'เพิ่มข้อมูล Doctors เรียบร้อย';
END
GO

-- ===================================================================
-- 4. เพิ่มข้อมูล Patients (ผู้ป่วย)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM Patients WHERE PatientCode = 'P2024001')
BEGIN
    INSERT INTO Patients (PatientCode, FirstName, LastName, DateOfBirth, Gender, BloodGroup, Phone, Email, Address, EmergencyContact, EmergencyPhone, Allergies, CreatedBy) VALUES
    ('P2024001', N'วิชัย', N'มั่งมี', '1990-05-15', N'ชาย', 'A+', '0891234567', 'wichai@email.com', N'123 ถ.สุขุมวิท กรุงเทพฯ', N'นางสาววิไล มั่งมี', '0891234568', N'แพ้ยาเพนนิซิลิน', 1),
    ('P2024002', N'สุดา', N'ร่ำรวย', '1985-08-20', N'หญิง', 'B+', '0892345678', 'suda@email.com', N'456 ถ.พระราม 4 กรุงเทพฯ', N'นายสมชาย ร่ำรวย', '0892345679', NULL, 1),
    ('P2024003', N'ประเสริฐ', N'ดีงาม', '2010-03-10', N'ชาย', 'O+', '0893456789', 'prasert@email.com', N'789 ถ.ลาดพร้าว กรุงเทพฯ', N'นางสมหญิง ดีงาม', '0893456780', N'แพ้นม', 1),
    ('P2024004', N'มาลี', N'สวยงาม', '1978-12-25', N'หญิง', 'AB+', '0894567890', 'malee@email.com', N'321 ถ.รัชดาภิเษก กรุงเทพฯ', N'นายสมศักดิ์ สวยงาม', '0894567891', NULL, 1),
    ('P2024005', N'สมบูรณ์', N'แข็งแรง', '1955-07-08', N'ชาย', 'A-', '0895678901', 'somboon@email.com', N'654 ถ.พหลโยธิน กรุงเทพฯ', N'นางสมจิตร แข็งแรง', '0895678902', N'แพ้กุ้ง, แพ้ยาแอสไพริน', 1);
    
    PRINT 'เพิ่มข้อมูล Patients เรียบร้อย';
END
GO

-- ===================================================================
-- 5. เพิ่มข้อมูล Medicines (ยา)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM Medicines WHERE MedicineName = N'พาราเซตามอล 500mg')
BEGIN
    INSERT INTO Medicines (MedicineName, Category, Manufacturer, UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, Description) VALUES
    (N'พาราเซตามอล 500mg', N'ยาแก้ปวด-ลดไข้', N'GPO', 2.50, 1000, 100, '2025-12-31', N'ยาแก้ปวด ลดไข้'),
    (N'อะม็อกซีซิลลิน 500mg', N'ยาปฏิชีวนะ', N'GPO', 5.00, 500, 50, '2025-12-31', N'ยาปฏิชีวนะ'),
    (N'โอเมพราโซล 20mg', N'ยารักษากรดไหลย้อน', N'Siam Pharmaceutical', 8.00, 300, 30, '2025-12-31', N'ยาลดกรดในกระเพาะ'),
    (N'เมทฟอร์มิน 500mg', N'ยาเบาหวาน', N'Berlin Pharmaceutical', 3.00, 800, 80, '2025-12-31', N'ยาควบคุมระดับน้ำตาลในเลือด'),
    (N'ซีทิริซีน 10mg', N'ยาแก้แพ้', N'T.O. Chemicals', 4.00, 400, 40, '2025-12-31', N'ยาแก้แพ้ แก้คัน'),
    (N'ไอบูโพรเฟน 400mg', N'ยาแก้ปวด-ลดอักเสบ', N'GPO', 3.50, 600, 60, '2025-12-31', N'ยาแก้ปวด ลดการอักเสบ'),
    (N'วิตามินซี 1000mg', N'วิตามิน', N'Mega We Care', 10.00, 200, 20, '2025-12-31', N'วิตามินซี เสริมภูมิคุ้มกัน'),
    (N'ลอราทาดีน 10mg', N'ยาแก้แพ้', N'T.O. Chemicals', 5.50, 350, 35, '2025-12-31', N'ยาแก้แพ้ ไม่ง่วง');
    
    PRINT 'เพิ่มข้อมูล Medicines เรียบร้อย';
END
GO

-- ===================================================================
-- 6. เพิ่มข้อมูล Appointments (นัดหมาย)
-- ===================================================================
IF NOT EXISTS (SELECT * FROM Appointments WHERE PatientID = 1 AND AppointmentDate = '2024-12-12')
BEGIN
    INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, CreatedBy) VALUES
    (1, 1, '2024-12-12', '09:00:00', N'ยืนยันแล้ว', N'ปวดท้อง', 4),
    (2, 4, '2024-12-12', '10:00:00', N'ยืนยันแล้ว', N'ตรวจครรภ์', 4),
    (3, 3, '2024-12-12', '11:00:00', N'รอยืนยัน', N'ไข้สูง', 4),
    (4, 2, '2024-12-13', '14:00:00', N'ยืนยันแล้ว', N'ปวดหลัง', 4),
    (5, 1, '2024-12-13', '15:00:00', N'ยืนยันแล้ว', N'ตรวจสุขภาพประจำปี', 4);
    
    PRINT 'เพิ่มข้อมูล Appointments เรียบร้อย';
END
GO

PRINT '===================================================================';
PRINT 'เพิ่มข้อมูลตัวอย่างเรียบร้อยแล้ว!';
PRINT '===================================================================';
PRINT '';
PRINT 'ข้อมูล Login ทดสอบ:';
PRINT 'Username: admin     Password: admin123   (ผู้ดูแลระบบ)';
PRINT 'Username: doctor1   Password: doctor123  (แพทย์)';
PRINT 'Username: nurse1    Password: nurse123   (พยาบาล)';
PRINT 'Username: reception1 Password: recep123  (พนักงานต้อนรับ)';
PRINT 'Username: pharma1   Password: pharma123  (เภสัชกร)';
PRINT '===================================================================';
