# 🚀 Hospital Management System - Deployment Guide

## การติดตั้งและ Deploy ระบบไปเครื่องอื่น

คู่มือนี้จะแนะนำวิธีการนำระบบ Hospital Management System ไป Deploy บนเครื่องคอมพิวเตอร์เครื่องอื่น เพื่อทดสอบหรือใช้งานจริง

---

## 📋 สิ่งที่ต้องเตรียม

### 1. Software ที่ต้องติดตั้งบนเครื่องปลายทาง

| Software | Version | Download Link | หมายเหตุ |
|----------|---------|---------------|----------|
| **.NET Framework** | 4.7.2 หรือสูงกว่า | [Microsoft .NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) | จำเป็น |
| **SQL Server** | 2017 Express หรือสูงกว่า | [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | จำเป็น |
| **SQL Server Management Studio (SSMS)** | Latest | [SSMS Download](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) | แนะนำ (สำหรับจัดการ Database) |

---

## 🔧 ขั้นตอนการ Deploy

### **Step 1: ติดตั้ง SQL Server**

1. ดาวน์โหลด **SQL Server 2019 Express** (ฟรี)
2. รันไฟล์ติดตั้ง → เลือก **Basic Installation**
3. จดชื่อ Server Instance: `localhost\SQLEXPRESS` หรือ `.\SQLEXPRESS`
4. เปิดใช้งาน **SQL Server Authentication** (Mixed Mode)
5. ตั้งรหัสผ่าน `sa` (ตัวอย่าง: `Admin@123`)

### **Step 2: สร้าง Database**

1. เปิด **SQL Server Management Studio (SSMS)**
2. เชื่อมต่อกับ Server: `localhost\SQLEXPRESS`
   - Authentication: **SQL Server Authentication**
   - Login: `sa`
   - Password: `Admin@123` (หรือที่คุณตั้งไว้)

3. รัน SQL Scripts ตามลำดับ:

```sql
-- ใน SSMS กด New Query แล้ว Copy-Paste Scripts เหล่านี้

-- 1. สร้าง Database
-- File: Database/01_CreateDatabase.sql
-- (เปิดไฟล์แล้ว Copy ทั้งหมดมา Paste)

-- 2. สร้างตารางผู้ใช้และ Login
-- File: Database/02_Create_Users_Table.sql

-- 3. สร้างตารางผู้ป่วย
-- File: Database/03_Create_Patients_Table.sql

-- 4. สร้างตารางแพทย์
-- File: Database/04_Create_Doctors_Table.sql

-- 5. สร้างตารางการนัดหมาย
-- File: Database/05_Create_Appointments_Table.sql

-- 6. สร้างตารางยา
-- File: Database/06_Create_Medicines_Table.sql

-- 7. สร้างตารางเวชระเบียน
-- File: Database/07_Create_MedicalRecords_Table.sql

-- 8. สร้างตารางการเรียกเก็บเงิน
-- File: Database/13_Create_Billing_Tables.sql

-- 9. เพิ่มความปลอดภัย Billing (Optional แต่แนะนำ)
-- File: Database/14_Enhance_Billing_Security.sql
```

### **Step 3: แก้ไข Connection String**

1. เปิดโฟลเดอร์โปรเจค: `HospitalMS/HospitalManagementSystem/`
2. แก้ไขไฟล์ `App.config` ใน Project **Hospitalms.UI**:

```xml
<connectionStrings>
  <add name="HospitalDB" 
       connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=HospitalDB;User ID=sa;Password=Admin@123;Integrated Security=False" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**หมายเหตุ:**
- `Data Source` = ชื่อ SQL Server Instance ของคุณ
- `User ID` = `sa` (หรือ username ที่สร้างไว้)
- `Password` = รหัสผ่านที่ตั้งไว้

### **Step 4: Build โปรเจค**

#### **วิธีที่ 1: ใช้ Visual Studio**

1. เปิด Visual Studio
2. เปิดไฟล์ `HospitalManagementSystem.sln`
3. คลิกขวาที่ Solution → **Clean Solution**
4. คลิกขวาที่ Solution → **Rebuild Solution**
5. รอจนกว่า Build สำเร็จ (ดูที่ Output Window)

#### **วิธีที่ 2: ใช้ Command Line**

```powershell
# เปิด PowerShell ใน folder HospitalManagementSystem
cd "C:\Users\Marke\Desktop\C# hospital\HospitalMS\HospitalManagementSystem"

# Clean
dotnet clean

# Build
dotnet build --configuration Release
```

### **Step 5: Copy ไฟล์ไปเครื่องปลายทาง**

1. หลัง Build สำเร็จ ไฟล์จะอยู่ที่:
   ```
   HospitalManagementSystem/Hospitalms.UI/bin/Release/
   ```

2. Copy ทั้งโฟลเดอร์ `Release` ไปเครื่องปลายทาง

3. ไฟล์ที่สำคัญ:
   - `Hospitalms.UI.exe` (ไฟล์หลัก)
   - `HospitalMS.DAL.dll`
   - `HospitalMS.BLL.dll`
   - `Hospitalms.UI.exe.config` (มี Connection String)
   - ไฟล์ DLL อื่นๆ

---

## 🧪 การทดสอบบนเครื่องอื่น

### **Scenario 1: ทดสอบบนเครื่องเดียวกัน (Local)**

1. เปิด SQL Server บนเครื่อง
2. รัน `Hospitalms.UI.exe`
3. Login ด้วย:
   - Username: `admin`
   - Password: `admin123`

### **Scenario 2: ทดสอบบนเครื่องอื่นในเครือข่ายเดียวกัน (LAN)**

#### **บนเครื่อง Server (ที่มี SQL Server):**

1. เปิด **SQL Server Configuration Manager**
2. ไปที่ **SQL Server Network Configuration** → **Protocols for SQLEXPRESS**
3. เปิดใช้งาน **TCP/IP**
4. คลิกขวา TCP/IP → **Properties** → **IP Addresses**
5. ที่ **IPAll** ตั้ง **TCP Port** = `1433`
6. Restart SQL Server Service

7. เปิด **Windows Firewall**:
   ```powershell
   # เปิด PowerShell as Administrator
   New-NetFirewallRule -DisplayName "SQL Server" -Direction Inbound -Protocol TCP -LocalPort 1433 -Action Allow
   ```

8. จด IP Address ของเครื่อง Server:
   ```powershell
   ipconfig
   # ตัวอย่าง: 192.168.1.100
   ```

#### **บนเครื่อง Client (ที่จะใช้งาน):**

1. แก้ไข `Hospitalms.UI.exe.config`:
   ```xml
   <connectionStrings>
     <add name="HospitalDB" 
          connectionString="Data Source=192.168.1.100,1433;Initial Catalog=HospitalDB;User ID=sa;Password=Admin@123;Integrated Security=False" 
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

2. รัน `Hospitalms.UI.exe`

---

## 🔒 ความปลอดภัย

### **สำหรับการใช้งานจริง:**

1. **เปลี่ยนรหัสผ่าน `sa`** ให้ซับซ้อน
2. **สร้าง User เฉพาะ** สำหรับ Application:
   ```sql
   CREATE LOGIN HospitalApp WITH PASSWORD = 'StrongPassword@2024';
   USE HospitalDB;
   CREATE USER HospitalApp FOR LOGIN HospitalApp;
   EXEC sp_addrolemember 'db_datareader', 'HospitalApp';
   EXEC sp_addrolemember 'db_datawriter', 'HospitalApp';
   ```

3. **Encrypt Connection String** (Advanced)
4. **ใช้ SSL/TLS** สำหรับการเชื่อมต่อ Database

---

## 🐛 Troubleshooting

### **ปัญหา: ไม่สามารถเชื่อมต่อ SQL Server**

**แก้ไข:**
1. ตรวจสอบ SQL Server Service ทำงานหรือไม่:
   ```powershell
   Get-Service MSSQL*
   ```
2. ตรวจสอบ Firewall
3. ตรวจสอบ Connection String ถูกต้องหรือไม่
4. ลอง Ping เครื่อง Server:
   ```powershell
   ping 192.168.1.100
   ```

### **ปัญหา: Login Failed**

**แก้ไข:**
1. ตรวจสอบ Username/Password ใน Database
2. ตรวจสอบ SQL Server Authentication เปิดใช้งานหรือไม่

### **ปัญหา: Missing DLL**

**แก้ไข:**
1. Copy ทั้งโฟลเดอร์ `bin/Release` ไป
2. ติดตั้ง .NET Framework 4.7.2 บนเครื่องปลายทาง

---

## 📦 การสร้าง Installer (Optional)

ถ้าต้องการสร้าง Installer แบบมืออาชีพ สามารถใช้:

1. **Inno Setup** (ฟรี): https://jrsoftware.org/isinfo.php
2. **Advanced Installer** (มีทั้งฟรีและเสียเงิน)
3. **WiX Toolset** (ฟรี แต่ซับซ้อน)

---

## 📞 ติดต่อสอบถาม

หากมีปัญหาหรือข้อสงสัย สามารถติดต่อได้ที่:
- Email: support@hospital-system.com
- GitHub Issues: [Repository Link]

---

## 📝 Checklist การ Deploy

- [ ] ติดตั้ง SQL Server บนเครื่องปลายทาง
- [ ] สร้าง Database และรัน SQL Scripts ทั้งหมด
- [ ] แก้ไข Connection String ใน `App.config`
- [ ] Build โปรเจคแบบ Release
- [ ] Copy ไฟล์ไปเครื่องปลายทาง
- [ ] ทดสอบ Login
- [ ] ทดสอบฟังก์ชันหลัก (Patient, Doctor, Appointment, Billing)
- [ ] Backup Database เป็นประจำ

---

**สำเร็จ! ระบบพร้อมใช้งานแล้ว** 🎉
