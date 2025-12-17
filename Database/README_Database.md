# 🗄️ สร้างฐานข้อมูล - คำแนะนำทีละขั้นตอน

## 📋 ภาพรวม

เราได้สร้างสคริปต์ SQL 2 ไฟล์:
1. **01_CreateDatabase.sql** - สร้างฐานข้อมูลและตาราง
2. **02_InsertSampleData.sql** - เพิ่มข้อมูลตัวอย่างเพื่อทดสอบ

---

## 🚀 วิธีรันสคริปต์

### วิธีที่ 1: ใช้ SQL Server Management Studio (SSMS) - แนะนำ

#### ขั้นตอนที่ 1: เปิด SSMS
1. เปิดโปรแกรม **SQL Server Management Studio**
2. เชื่อมต่อกับ SQL Server:
   - Server name: `.\SQLEXPRESS` หรือ `(LocalDB)\MSSQLLocalDB`
   - Authentication: **Windows Authentication**
   - คลิก **Connect**

#### ขั้นตอนที่ 2: รันสคริปต์สร้างฐานข้อมูล
1. คลิก **File** → **Open** → **File**
2. เลือกไฟล์ `01_CreateDatabase.sql`
3. คลิกปุ่ม **Execute** (หรือกด F5)
4. ตรวจสอบ Messages:
   ```
   สร้างฐานข้อมูล HospitalDB เรียบร้อย
   สร้างตาราง Roles เรียบร้อย
   สร้างตาราง Users เรียบร้อย
   ...
   ```

#### ขั้นตอนที่ 3: รันสคริปต์เพิ่มข้อมูลตัวอย่าง
1. เปิดไฟล์ `02_InsertSampleData.sql`
2. คลิกปุ่ม **Execute** (หรือกด F5)
3. ตรวจสอบ Messages:
   ```
   เพิ่มข้อมูล Roles เรียบร้อย
   เพิ่มข้อมูล Users เรียบร้อย
   ...
   ```

#### ขั้นตอนที่ 4: ตรวจสอบฐานข้อมูล
1. ใน **Object Explorer** → ขยาย **Databases**
2. คุณจะเห็น **HospitalDB**
3. ขยาย **Tables** จะเห็นตารางทั้งหมด

---

### วิธีที่ 2: ใช้ Visual Studio

#### ขั้นตอนที่ 1: เปิด SQL Server Object Explorer
1. ใน Visual Studio → เมนู **View** → **SQL Server Object Explorer**
2. ขยาย **SQL Server** → ขยาย server ของคุณ

#### ขั้นตอนที่ 2: รันสคริปต์
1. คลิกขวาที่ **Databases** → **Add New Database**
2. หรือเปิดไฟล์ `.sql` ใน Visual Studio
3. คลิกปุ่ม **Execute** (สีเขียว)

---

### วิธีที่ 3: ใช้ Command Line (sqlcmd)

```powershell
# รันสคริปต์สร้างฐานข้อมูล
sqlcmd -S .\SQLEXPRESS -i "C:\Users\Marke\Desktop\SEO bytest\HospitalMS\Database\01_CreateDatabase.sql"

# รันสคริปต์เพิ่มข้อมูล
sqlcmd -S .\SQLEXPRESS -i "C:\Users\Marke\Desktop\SEO bytest\HospitalMS\Database\02_InsertSampleData.sql"
```

---

## 📊 โครงสร้างฐานข้อมูล

### ตารางที่สร้าง (11 ตาราง)

#### 1. **Roles** - บทบาทผู้ใช้งาน
```
- Admin (ผู้ดูแลระบบ)
- Doctor (แพทย์)
- Nurse (พยาบาล)
- Receptionist (พนักงานต้อนรับ)
- Pharmacist (เภสัชกร)
```

#### 2. **Users** - ผู้ใช้งานระบบ
```
- UserID, Username, PasswordHash
- FullName, Email, Phone
- RoleID (เชื่อมกับ Roles)
```

#### 3. **Patients** - ผู้ป่วย
```
- PatientID, PatientCode (รหัสผู้ป่วย)
- ชื่อ-นามสกุล, วันเกิด, เพศ
- กรุ๊ปเลือด, เบอร์โทร, อีเมล
- ที่อยู่, ผู้ติดต่อฉุกเฉิน
- ประวัติการแพ้ยา, ประวัติการรักษา
```

#### 4. **Doctors** - แพทย์
```
- DoctorID, DoctorCode
- ชื่อ-นามสกุล, ความเชี่ยวชาญ
- คุณวุฒิ, เลขใบประกอบวิชาชีพ
- ค่าตรวจ, สถานะพร้อมให้บริการ
```

#### 5. **Appointments** - นัดหมาย
```
- AppointmentID
- PatientID, DoctorID
- วันที่-เวลานัด
- สถานะ (รอยืนยัน, ยืนยันแล้ว, เสร็จสิ้น, ยกเลิก)
- เหตุผลในการนัด
```

#### 6. **MedicalRecords** - บันทึกการรักษา
```
- RecordID
- PatientID, DoctorID, AppointmentID
- อาการ, การวินิจฉัย, การรักษา
- ใบสั่งยา, วันนัดครั้งต่อไป
```

#### 7. **Medicines** - ยา
```
- MedicineID, ชื่อยา
- หมวดหมู่, ผู้ผลิต
- ราคา, จำนวนคงเหลือ
- ระดับสั่งซื้อใหม่, วันหมดอายุ
```

#### 8. **Prescriptions** - ใบสั่งยา
```
- PrescriptionID
- RecordID, MedicineID
- ขนาดยา, ความถี่, ระยะเวลา
- จำนวน, คำแนะนำ
```

#### 9. **Billing** - การเรียกเก็บเงิน
```
- BillID, PatientID, AppointmentID
- ยอดรวม, ยอดจ่ายแล้ว, ยอดคงเหลือ
- สถานะการจ่ายเงิน
- วิธีการชำระเงิน
```

#### 10. **BillDetails** - รายละเอียดค่าใช้จ่าย
```
- BillDetailID, BillID
- ประเภทบริการ (ค่าตรวจ, ค่ายา, ค่าแล็บ)
- คำอธิบาย, จำนวน, ราคา
```

#### 11. **ActivityLogs** - บันทึกการใช้งาน
```
- LogID, UserID
- การกระทำ, ตาราง, RecordID
- รายละเอียด, IP Address
```

---

## 🔗 ความสัมพันธ์ระหว่างตาราง (Relationships)

```
Users ──┬─> Patients (CreatedBy)
        ├─> MedicalRecords (CreatedBy)
        ├─> Appointments (CreatedBy)
        ├─> Billing (CreatedBy)
        └─> ActivityLogs

Patients ──┬─> Appointments
           ├─> MedicalRecords
           └─> Billing

Doctors ──┬─> Appointments
          └─> MedicalRecords

Appointments ──┬─> MedicalRecords
               └─> Billing

MedicalRecords ──> Prescriptions

Medicines ──> Prescriptions

Billing ──> BillDetails
```

---

## 🧪 ข้อมูลทดสอบ

### ผู้ใช้งานทดสอบ (Test Users)

| Username | Password | Role | ชื่อ |
|----------|----------|------|------|
| admin | admin123 | Admin | ผู้ดูแลระบบ |
| doctor1 | doctor123 | Doctor | นพ.สมชาย ใจดี |
| nurse1 | nurse123 | Nurse | พย.สมหญิง รักษา |
| reception1 | recep123 | Receptionist | สมศรี ยิ้มแย้ม |
| pharma1 | pharma123 | Pharmacist | ภก.สมพร ใส่ใจ |

### แพทย์ทดสอบ (5 คน)
- นพ.สมชาย ใจดี - อายุรกรรม (ค่าตรวจ 1,000 บาท)
- นพ.สมหมาย รักษา - ศัลยกรรม (ค่าตรวจ 1,500 บาท)
- นพ.สมใจ ดูแล - กุมารเวชกรรม (ค่าตรวจ 800 บาท)
- นพ.สมศรี เอาใจใส่ - สูติ-นรีเวช (ค่าตรวจ 1,200 บาท)
- นพ.สมพร ช่วยเหลือ - ออร์โธปิดิกส์ (ค่าตรวจ 1,300 บาท)

### ผู้ป่วยทดสอบ (5 คน)
- P2024001 - วิชัย มั่งมี (ชาย, กรุ๊ป A+, แพ้เพนนิซิลิน)
- P2024002 - สุดา ร่ำรวย (หญิง, กรุ๊ป B+)
- P2024003 - ประเสริฐ ดีงาม (ชาย, กรุ๊ป O+, แพ้นม)
- P2024004 - มาลี สวยงาม (หญิง, กรุ๊ป AB+)
- P2024005 - สมบูรณ์ แข็งแรง (ชาย, กรุ๊ป A-, แพ้กุ้ง+แอสไพริน)

### ยาทดสอบ (8 รายการ)
- พาราเซตามอล, อะม็อกซีซิลลิน, โอเมพราโซล
- เมทฟอร์มิน, ซีทิริซีน, ไอบูโพรเฟน
- วิตามินซี, ลอราทาดีน

---

## ✅ ตรวจสอบว่าสำเร็จ

### ใช้ SQL Query ตรวจสอบ

```sql
-- ตรวจสอบจำนวนข้อมูลในแต่ละตาราง
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
Roles: 5
Users: 5
Patients: 5
Doctors: 5
Medicines: 8
Appointments: 5
```

---

## 🔧 แก้ปัญหา (Troubleshooting)

### ปัญหา: เชื่อมต่อ SQL Server ไม่ได้

**วิธีแก้:**
1. ตรวจสอบว่า SQL Server Service ทำงานอยู่:
   - เปิด **Services** (กด Win+R → พิมพ์ `services.msc`)
   - หา **SQL Server (SQLEXPRESS)** หรือ **SQL Server (MSSQLSERVER)**
   - ถ้า Stopped → คลิกขวา → **Start**

### ปัญหา: Database มีอยู่แล้ว

**วิธีแก้:**
```sql
-- ลบฐานข้อมูลเดิม (ระวัง! ข้อมูลจะหายหมด)
USE master;
DROP DATABASE HospitalDB;
GO

-- แล้วรันสคริปต์ใหม่อีกครั้ง
```

### ปัญหา: Permission Denied

**วิธีแก้:**
- ตรวจสอบว่าคุณเป็น Administrator
- หรือเปลี่ยนเป็น SQL Server Authentication

---

## 🎯 สรุป

คุณได้:
✅ สร้างฐานข้อมูล HospitalDB  
✅ สร้างตาราง 11 ตาราง  
✅ เพิ่มข้อมูลตัวอย่างเพื่อทดสอบ  
✅ มีผู้ใช้งานทดสอบ 5 คน  
✅ มีข้อมูลแพทย์ ผู้ป่วย ยา และนัดหมาย  

---

## 🚀 ขั้นตอนต่อไป

1. สร้าง **DatabaseHelper.cs** (เชื่อมต่อฐานข้อมูล)
2. สร้าง **LoginForm** (หน้าจอ Login)
3. ทดสอบ Login ด้วย username: `admin` password: `admin123`

**พร้อมไปต่อแล้วใช่ไหมครับ?** 💪
