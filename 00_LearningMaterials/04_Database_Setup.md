# 🗄️ บทที่ 4: สร้างฐานข้อมูล Hospital Management System

## 📋 เป้าหมายของบทนี้

ในบทนี้คุณจะได้เรียนรู้:
- ✅ โครงสร้างฐานข้อมูลของระบบโรงพยาบาล
- ✅ วิธีรันสคริปต์ SQL เพื่อสร้างฐานข้อมูล
- ✅ ความหมายของแต่ละตาราง
- ✅ ความสัมพันธ์ระหว่างตาราง (Relationships)
- ✅ ข้อมูลทดสอบสำหรับเริ่มต้น

---

## 🎯 ภาพรวมฐานข้อมูล

ระบบ Hospital Management System ของเรามี **11 ตาราง** หลัก:

### 1. **ตารางจัดการผู้ใช้งาน** 👥
- `Roles` - บทบาท (Admin, Doctor, Nurse, ฯลฯ)
- `Users` - ผู้ใช้งานระบบ
- `ActivityLogs` - บันทึกการใช้งาน

### 2. **ตารางข้อมูลหลัก** 🏥
- `Patients` - ข้อมูลผู้ป่วย
- `Doctors` - ข้อมูลแพทย์

### 3. **ตารางการรักษา** 💊
- `Appointments` - การนัดหมาย
- `MedicalRecords` - บันทึกการรักษา
- `Medicines` - ข้อมูลยา
- `Prescriptions` - ใบสั่งยา

### 4. **ตารางการเงิน** 💰
- `Billing` - บิลค่ารักษา
- `BillDetails` - รายละเอียดค่าใช้จ่าย

---

## 🚀 วิธีสร้างฐานข้อมูล

### ขั้นตอนที่ 1: เปิด SQL Server Management Studio (SSMS)

1. **เปิดโปรแกรม SSMS**
   - ค้นหา "SQL Server Management Studio" ใน Start Menu
   - หรือกด Windows + S แล้วพิมพ์ "SSMS"

2. **เชื่อมต่อกับ SQL Server**
   ```
   Server name: .\SQLEXPRESS
   (หรือ (LocalDB)\MSSQLLocalDB ถ้าใช้ LocalDB)
   
   Authentication: Windows Authentication
   ```
   
3. **คลิก Connect**

---

### ขั้นตอนที่ 2: รันสคริปต์สร้างฐานข้อมูล

1. **เปิดไฟล์ SQL**
   - คลิก **File** → **Open** → **File**
   - ไปที่โฟลเดอร์ `Database`
   - เลือกไฟล์ **`01_CreateDatabase.sql`**

2. **รันสคริปต์**
   - คลิกปุ่ม **Execute** (หรือกด **F5**)
   - รอสักครู่ (ประมาณ 5-10 วินาที)

3. **ตรวจสอบผลลัพธ์**
   
   ในหน้าต่าง **Messages** ด้านล่าง คุณจะเห็น:
   ```
   สร้างฐานข้อมูล HospitalDB เรียบร้อย
   สร้างตาราง Roles เรียบร้อย
   สร้างตาราง Users เรียบร้อย
   สร้างตาราง Patients เรียบร้อย
   ...
   สร้างฐานข้อมูลและตารางทั้งหมดเรียบร้อยแล้ว!
   ```

---

### ขั้นตอนที่ 3: เพิ่มข้อมูลตัวอย่าง

1. **เปิดไฟล์ SQL**
   - คลิก **File** → **Open** → **File**
   - เลือกไฟล์ **`02_InsertSampleData.sql`**

2. **รันสคริปต์**
   - คลิกปุ่ม **Execute** (หรือกด **F5**)

3. **ตรวจสอบผลลัพธ์**
   ```
   เพิ่มข้อมูล Roles เรียบร้อย
   เพิ่มข้อมูล Users เรียบร้อย
   เพิ่มข้อมูล Patients เรียบร้อย
   ...
   ```

---

### ขั้นตอนที่ 4: ตรวจสอบว่าสำเร็จ

1. **ดูฐานข้อมูลที่สร้าง**
   - ใน **Object Explorer** (ด้านซ้าย)
   - ขยาย **Databases**
   - คุณจะเห็น **HospitalDB** 🎉

2. **ดูตารางที่สร้าง**
   - ขยาย **HospitalDB**
   - ขยาย **Tables**
   - คุณจะเห็นตาราง 11 ตาราง

3. **ตรวจสอบข้อมูล**
   
   รันคำสั่ง SQL นี้:
   ```sql
   USE HospitalDB;
   
   SELECT 'Roles' AS TableName, COUNT(*) AS RecordCount FROM Roles
   UNION ALL
   SELECT 'Users', COUNT(*) FROM Users
   UNION ALL
   SELECT 'Patients', COUNT(*) FROM Patients
   UNION ALL
   SELECT 'Doctors', COUNT(*) FROM Doctors
   UNION ALL
   SELECT 'Medicines', COUNT(*) FROM Medicines
   UNION ALL
   SELECT 'Appointments', COUNT(*) FROM Appointments;
   ```
   
   **ผลลัพธ์ที่ควรได้:**
   ```
   TableName       RecordCount
   Roles           5
   Users           5
   Patients        5
   Doctors         5
   Medicines       8
   Appointments    5
   ```

---

## 📊 รายละเอียดแต่ละตาราง

### 1. ตาราง Roles (บทบาทผู้ใช้งาน)

**ไว้ทำอะไร:** กำหนดสิทธิ์การใช้งานระบบ

**ข้อมูลที่มี:**
| RoleID | RoleName | Description |
|--------|----------|-------------|
| 1 | Admin | ผู้ดูแลระบบ - เข้าถึงได้ทุกอย่าง |
| 2 | Doctor | แพทย์ - ดูข้อมูลผู้ป่วย บันทึกการรักษา |
| 3 | Nurse | พยาบาล - ช่วยงานแพทย์ |
| 4 | Receptionist | พนักงานต้อนรับ - ลงทะเบียนผู้ป่วย นัดหมาย |
| 5 | Pharmacist | เภสัชกร - จัดการยา |

**ตัวอย่างการใช้งาน:**
```sql
-- ดูบทบาททั้งหมด
SELECT * FROM Roles;
```

---

### 2. ตาราง Users (ผู้ใช้งานระบบ)

**ไว้ทำอะไร:** เก็บข้อมูลผู้ใช้งานที่ Login เข้าระบบ

**คอลัมน์สำคัญ:**
- `UserID` - รหัสผู้ใช้ (Auto Increment)
- `Username` - ชื่อผู้ใช้สำหรับ Login
- `PasswordHash` - รหัสผ่าน (เข้ารหัสแล้ว)
- `FullName` - ชื่อ-นามสกุลจริง
- `RoleID` - บทบาท (เชื่อมกับตาราง Roles)
- `IsActive` - สถานะใช้งาน (1=ใช้งาน, 0=ปิดการใช้งาน)

**ผู้ใช้งานทดสอบ:**
| Username | Password | Role | ชื่อ |
|----------|----------|------|------|
| admin | admin123 | Admin | ผู้ดูแลระบบ |
| doctor1 | doctor123 | Doctor | นพ.สมชาย ใจดี |
| nurse1 | nurse123 | Nurse | พย.สมหญิง รักษา |
| reception1 | recep123 | Receptionist | สมศรี ยิ้มแย้ม |
| pharma1 | pharma123 | Pharmacist | ภก.สมพร ใส่ใจ |

**ตัวอย่างการใช้งาน:**
```sql
-- ดูผู้ใช้งานทั้งหมด พร้อมบทบาท
SELECT u.Username, u.FullName, r.RoleName, u.IsActive
FROM Users u
INNER JOIN Roles r ON u.RoleID = r.RoleID;
```

---

### 3. ตาราง Patients (ผู้ป่วย)

**ไว้ทำอะไร:** เก็บข้อมูลผู้ป่วยทั้งหมด

**คอลัมน์สำคัญ:**
- `PatientID` - รหัสผู้ป่วย (Auto Increment)
- `PatientCode` - รหัสผู้ป่วย (เช่น P2024001)
- `FirstName`, `LastName` - ชื่อ-นามสกุล
- `DateOfBirth` - วันเกิด
- `Gender` - เพศ (ชาย/หญิง/ไม่ระบุ)
- `BloodGroup` - กรุ๊ปเลือด (A+, B+, O+, AB+, ฯลฯ)
- `Phone`, `Email` - ข้อมูลติดต่อ
- `Address` - ที่อยู่
- `EmergencyContact`, `EmergencyPhone` - ผู้ติดต่อฉุกเฉิน
- `Allergies` - ประวัติการแพ้ยา/อาหาร
- `MedicalHistory` - ประวัติการรักษา

**ตัวอย่างการใช้งาน:**
```sql
-- ดูผู้ป่วยทั้งหมด
SELECT PatientCode, FirstName + ' ' + LastName AS FullName, 
       Gender, BloodGroup, Phone
FROM Patients
WHERE IsActive = 1;

-- ค้นหาผู้ป่วยที่แพ้ยา
SELECT PatientCode, FirstName + ' ' + LastName AS FullName, Allergies
FROM Patients
WHERE Allergies IS NOT NULL;
```

---

### 4. ตาราง Doctors (แพทย์)

**ไว้ทำอะไร:** เก็บข้อมูลแพทย์

**คอลัมน์สำคัญ:**
- `DoctorID` - รหัสแพทย์
- `DoctorCode` - รหัสแพทย์ (เช่น D001)
- `FirstName`, `LastName` - ชื่อ-นามสกุล
- `Specialization` - ความเชี่ยวชาญ (อายุรกรรม, ศัลยกรรม, ฯลฯ)
- `Qualification` - คุณวุฒิ
- `LicenseNumber` - เลขใบประกอบวิชาชีพ
- `ConsultationFee` - ค่าตรวจ
- `IsAvailable` - สถานะพร้อมให้บริการ

**ตัวอย่างการใช้งาน:**
```sql
-- ดูแพทย์ทั้งหมดที่พร้อมให้บริการ
SELECT DoctorCode, FirstName + ' ' + LastName AS DoctorName, 
       Specialization, ConsultationFee
FROM Doctors
WHERE IsAvailable = 1 AND IsActive = 1;
```

---

### 5. ตาราง Appointments (นัดหมาย)

**ไว้ทำอะไร:** เก็บข้อมูลการนัดหมายระหว่างผู้ป่วยและแพทย์

**คอลัมน์สำคัญ:**
- `AppointmentID` - รหัสนัดหมาย
- `PatientID` - รหัสผู้ป่วย
- `DoctorID` - รหัสแพทย์
- `AppointmentDate` - วันที่นัด
- `AppointmentTime` - เวลานัด
- `Status` - สถานะ (รอยืนยัน, ยืนยันแล้ว, เสร็จสิ้น, ยกเลิก)
- `Reason` - เหตุผลในการนัด

**ตัวอย่างการใช้งาน:**
```sql
-- ดูนัดหมายวันนี้
SELECT a.AppointmentDate, a.AppointmentTime,
       p.FirstName + ' ' + p.LastName AS PatientName,
       d.FirstName + ' ' + d.LastName AS DoctorName,
       a.Status
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
WHERE CAST(a.AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY a.AppointmentTime;
```

---

### 6. ตาราง MedicalRecords (บันทึกการรักษา)

**ไว้ทำอะไร:** บันทึกข้อมูลการรักษาแต่ละครั้ง

**คอลัมน์สำคัญ:**
- `RecordID` - รหัสบันทึก
- `PatientID` - รหัสผู้ป่วย
- `DoctorID` - รหัสแพทย์
- `AppointmentID` - รหัสนัดหมาย (ถ้ามี)
- `VisitDate` - วันที่มาพบแพทย์
- `Symptoms` - อาการ
- `Diagnosis` - การวินิจฉัย
- `Treatment` - การรักษา
- `Prescription` - ใบสั่งยา
- `FollowUpDate` - วันนัดครั้งต่อไป

**ตัวอย่างการใช้งาน:**
```sql
-- ดูประวัติการรักษาของผู้ป่วย
SELECT mr.VisitDate,
       d.FirstName + ' ' + d.LastName AS DoctorName,
       mr.Symptoms, mr.Diagnosis, mr.Treatment
FROM MedicalRecords mr
INNER JOIN Doctors d ON mr.DoctorID = d.DoctorID
WHERE mr.PatientID = 1
ORDER BY mr.VisitDate DESC;
```

---

### 7. ตาราง Medicines (ยา)

**ไว้ทำอะไร:** เก็บข้อมูลยาในคลังยา

**คอลัมน์สำคัญ:**
- `MedicineID` - รหัสยา
- `MedicineName` - ชื่อยา
- `Category` - หมวดหมู่ (ยาแก้ปวด, ยาปฏิชีวนะ, ฯลฯ)
- `Manufacturer` - ผู้ผลิต
- `UnitPrice` - ราคาต่อหน่วย
- `StockQuantity` - จำนวนคงเหลือ
- `ReorderLevel` - ระดับที่ต้องสั่งซื้อใหม่
- `ExpiryDate` - วันหมดอายุ

**ตัวอย่างการใช้งาน:**
```sql
-- ดูยาที่ใกล้หมด (ต่ำกว่าระดับสั่งซื้อ)
SELECT MedicineName, StockQuantity, ReorderLevel, UnitPrice
FROM Medicines
WHERE StockQuantity <= ReorderLevel AND IsActive = 1;

-- ดูยาที่ใกล้หมดอายุ (ภายใน 3 เดือน)
SELECT MedicineName, ExpiryDate, StockQuantity
FROM Medicines
WHERE ExpiryDate <= DATEADD(MONTH, 3, GETDATE())
ORDER BY ExpiryDate;
```

---

### 8. ตาราง Prescriptions (ใบสั่งยา)

**ไว้ทำอะไร:** เก็บรายละเอียดยาที่แพทย์สั่ง

**คอลัมน์สำคัญ:**
- `PrescriptionID` - รหัสใบสั่งยา
- `RecordID` - รหัสบันทึกการรักษา
- `MedicineID` - รหัสยา
- `Dosage` - ขนาดยา (เช่น 500mg)
- `Frequency` - ความถี่ (เช่น วันละ 3 ครั้ง)
- `Duration` - ระยะเวลา (เช่น 7 วัน)
- `Quantity` - จำนวน
- `Instructions` - คำแนะนำ (เช่น รับประทานหลังอาหาร)

---

### 9. ตาราง Billing (บิลค่ารักษา)

**ไว้ทำอะไร:** เก็บข้อมูลบิลค่ารักษา

**คอลัมน์สำคัญ:**
- `BillID` - รหัสบิล
- `PatientID` - รหัสผู้ป่วย
- `AppointmentID` - รหัสนัดหมาย
- `BillDate` - วันที่ออกบิล
- `TotalAmount` - ยอดรวมทั้งหมด
- `PaidAmount` - ยอดที่จ่ายแล้ว
- `BalanceAmount` - ยอดคงเหลือ (คำนวณอัตโนมัติ)
- `PaymentStatus` - สถานะการจ่าย (จ่ายแล้ว, จ่ายบางส่วน, ยังไม่จ่าย)
- `PaymentMethod` - วิธีชำระเงิน (เงินสด, บัตรเครดิต, ฯลฯ)

**ตัวอย่างการใช้งาน:**
```sql
-- ดูบิลที่ยังไม่จ่าย
SELECT b.BillID, 
       p.FirstName + ' ' + p.LastName AS PatientName,
       b.TotalAmount, b.PaidAmount, b.BalanceAmount,
       b.PaymentStatus
FROM Billing b
INNER JOIN Patients p ON b.PatientID = p.PatientID
WHERE b.PaymentStatus != N'จ่ายแล้ว'
ORDER BY b.BillDate DESC;
```

---

### 10. ตาราง BillDetails (รายละเอียดค่าใช้จ่าย)

**ไว้ทำอะไร:** เก็บรายละเอียดค่าใช้จ่ายแต่ละรายการในบิล

**คอลัมน์สำคัญ:**
- `BillDetailID` - รหัสรายละเอียด
- `BillID` - รหัสบิล
- `ServiceType` - ประเภทบริการ (ค่าตรวจ, ค่ายา, ค่าห้อง, ฯลฯ)
- `Description` - คำอธิบาย
- `Quantity` - จำนวน
- `UnitPrice` - ราคาต่อหน่วย
- `Amount` - ยอดรวม (คำนวณอัตโนมัติ)

---

### 11. ตาราง ActivityLogs (บันทึกการใช้งาน)

**ไว้ทำอะไร:** บันทึกการกระทำต่างๆ ในระบบ (Audit Trail)

**คอลัมน์สำคัญ:**
- `LogID` - รหัส Log
- `UserID` - ผู้ใช้งาน
- `Action` - การกระทำ (Login, Create, Update, Delete)
- `TableName` - ตารางที่ถูกกระทำ
- `RecordID` - รหัสข้อมูลที่ถูกกระทำ
- `Details` - รายละเอียด
- `IPAddress` - IP Address
- `CreatedDate` - วันเวลาที่เกิดเหตุการณ์

---

## 🔗 ความสัมพันธ์ระหว่างตาราง (Relationships)

### แผนภาพความสัมพันธ์

```
┌─────────┐
│  Roles  │
└────┬────┘
     │
     ▼
┌─────────┐         ┌──────────────┐         ┌──────────┐
│  Users  │────────▶│  Patients    │────────▶│Appointments│
└────┬────┘         └──────┬───────┘         └─────┬────┘
     │                     │                        │
     │                     ▼                        ▼
     │              ┌──────────────┐         ┌──────────────┐
     └─────────────▶│MedicalRecords│◀────────│   Doctors    │
                    └──────┬───────┘         └──────────────┘
                           │
                           ▼
                    ┌──────────────┐
                    │Prescriptions │
                    └──────┬───────┘
                           │
                           ▼
                    ┌──────────────┐
                    │  Medicines   │
                    └──────────────┘
```

### อธิบายความสัมพันธ์

1. **Users → Patients**: ผู้ใช้งานสร้างข้อมูลผู้ป่วย
2. **Patients → Appointments**: ผู้ป่วยทำการนัดหมาย
3. **Doctors → Appointments**: แพทย์รับนัดหมาย
4. **Appointments → MedicalRecords**: นัดหมายสร้างบันทึกการรักษา
5. **MedicalRecords → Prescriptions**: บันทึกการรักษามีใบสั่งยา
6. **Medicines → Prescriptions**: ยาถูกใช้ในใบสั่งยา
7. **Patients → Billing**: ผู้ป่วยได้รับบิล
8. **Billing → BillDetails**: บิลมีรายละเอียดค่าใช้จ่าย

---

## 🧪 ข้อมูลทดสอบ

### ผู้ใช้งานทดสอบ (Test Users)

| Username | Password | Role | ใช้ทำอะไร |
|----------|----------|------|-----------|
| admin | admin123 | Admin | ทดสอบระบบทั้งหมด |
| doctor1 | doctor123 | Doctor | ทดสอบการบันทึกการรักษา |
| nurse1 | nurse123 | Nurse | ทดสอบการช่วยงานแพทย์ |
| reception1 | recep123 | Receptionist | ทดสอบการลงทะเบียนผู้ป่วย |
| pharma1 | pharma123 | Pharmacist | ทดสอบการจัดการยา |

### ผู้ป่วยทดสอบ (5 คน)

- **P2024001** - วิชัย มั่งมี (ชาย, กรุ๊ป A+, แพ้เพนนิซิลิน)
- **P2024002** - สุดา ร่ำรวย (หญิง, กรุ๊ป B+)
- **P2024003** - ประเสริฐ ดีงาม (ชาย, กรุ๊ป O+, แพ้นม)
- **P2024004** - มาลี สวยงาม (หญิง, กรุ๊ป AB+)
- **P2024005** - สมบูรณ์ แข็งแรง (ชาย, กรุ๊ป A-, แพ้กุ้ง+แอสไพริน)

### แพทย์ทดสอบ (5 คน)

- **D001** - นพ.สมชาย ใจดี - อายุรกรรม (ค่าตรวจ 1,000 บาท)
- **D002** - นพ.สมหมาย รักษา - ศัลยกรรม (ค่าตรวจ 1,500 บาท)
- **D003** - นพ.สมใจ ดูแล - กุมารเวชกรรม (ค่าตรวจ 800 บาท)
- **D004** - นพ.สมศรี เอาใจใส่ - สูติ-นรีเวช (ค่าตรวจ 1,200 บาท)
- **D005** - นพ.สมพร ช่วยเหลือ - ออร์โธปิดิกส์ (ค่าตรวจ 1,300 บาท)

---

## 🔧 แก้ปัญหา (Troubleshooting)

### ❌ ปัญหา: เชื่อมต่อ SQL Server ไม่ได้

**อาการ:**
```
Cannot connect to .\SQLEXPRESS
```

**วิธีแก้:**

1. **ตรวจสอบ SQL Server Service**
   - กด `Win + R` → พิมพ์ `services.msc` → Enter
   - หา **SQL Server (SQLEXPRESS)**
   - ถ้า Status = Stopped → คลิกขวา → **Start**

2. **ลองเปลี่ยน Server Name**
   - ลอง: `(LocalDB)\MSSQLLocalDB`
   - หรือ: `localhost\SQLEXPRESS`
   - หรือ: `.`

---

### ❌ ปัญหา: Database มีอยู่แล้ว

**อาการ:**
```
Database 'HospitalDB' already exists
```

**วิธีแก้:**

**ตัวเลือกที่ 1: ลบฐานข้อมูลเดิม (ข้อมูลจะหายหมด!)**
```sql
USE master;
DROP DATABASE HospitalDB;
GO
```

**ตัวเลือกที่ 2: ใช้ฐานข้อมูลเดิมต่อ**
- ไม่ต้องทำอะไร ใช้ฐานข้อมูลที่มีอยู่ได้เลย

---

### ❌ ปัญหา: Permission Denied

**อาการ:**
```
User does not have permission to create database
```

**วิธีแก้:**

1. **รัน SSMS แบบ Administrator**
   - คลิกขวาที่ SSMS → **Run as administrator**

2. **เปลี่ยนเป็น SQL Server Authentication**
   - ใช้ username: `sa`
   - (ต้องตั้งค่า sa password ก่อน)

---

## ✅ เช็คลิสต์ความสำเร็จ

หลังจากทำตามขั้นตอนแล้ว คุณควรมี:

- ✅ ฐานข้อมูล **HospitalDB** ถูกสร้างแล้ว
- ✅ มีตาราง **11 ตาราง** ครบถ้วน
- ✅ มีข้อมูลทดสอบ:
  - 5 Roles
  - 5 Users
  - 5 Patients
  - 5 Doctors
  - 8 Medicines
  - 5 Appointments
- ✅ สามารถ Login ด้วย `admin` / `admin123` ได้

---

## 🚀 ขั้นตอนต่อไป

ตอนนี้คุณมีฐานข้อมูลพร้อมใช้งานแล้ว! ขั้นตอนต่อไปคือ:

### 1. สร้าง DatabaseHelper Class
- เรียนรู้วิธีเชื่อมต่อกับฐานข้อมูลจาก C#
- อ่านเอกสาร: `03_DatabaseHelper_Explained.md`

### 2. สร้าง Login Form
- ออกแบบหน้าจอ Login
- เขียนโค้ดตรวจสอบ Username/Password

### 3. ทดสอบระบบ
- ทดสอบ Login ด้วย username: `admin` password: `admin123`
- ตรวจสอบว่าเข้าสู่ระบบได้

---

## 💡 เคล็ดลับ

### 1. ดูข้อมูลในตาราง
```sql
-- ดูข้อมูลทั้งหมดในตาราง Patients
SELECT * FROM Patients;

-- ดูข้อมูลเฉพาะคอลัมน์ที่ต้องการ
SELECT PatientCode, FirstName, LastName, Phone 
FROM Patients;
```

### 2. ค้นหาข้อมูล
```sql
-- ค้นหาผู้ป่วยจากชื่อ
SELECT * FROM Patients 
WHERE FirstName LIKE N'%สม%';

-- ค้นหาแพทย์จากความเชี่ยวชาญ
SELECT * FROM Doctors 
WHERE Specialization LIKE N'%อายุร%';
```

### 3. นับจำนวนข้อมูล
```sql
-- นับจำนวนผู้ป่วยทั้งหมด
SELECT COUNT(*) AS TotalPatients FROM Patients;

-- นับจำนวนนัดหมายวันนี้
SELECT COUNT(*) AS TodayAppointments 
FROM Appointments 
WHERE CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE);
```

---

## 📚 สรุป

ในบทนี้คุณได้เรียนรู้:

✅ โครงสร้างฐานข้อมูล 11 ตาราง  
✅ วิธีรันสคริปต์ SQL  
✅ ความหมายของแต่ละตาราง  
✅ ความสัมพันธ์ระหว่างตาราง  
✅ การใช้งาน SQL Query พื้นฐาน  
✅ การแก้ปัญหาที่พบบ่อย  

**ยินดีด้วย! คุณพร้อมเขียนโค้ด C# เชื่อมต่อกับฐานข้อมูลแล้ว!** 🎉

---

**บทต่อไป:** [05_DatabaseHelper_Implementation.md](05_DatabaseHelper_Implementation.md)
