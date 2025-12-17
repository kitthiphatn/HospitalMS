# 🚀 เริ่มต้นสร้างโปรเจกต์ Hospital Management System

## 📋 สิ่งที่ต้องเตรียม

### 1. ติดตั้ง Visual Studio 2022 Community (ฟรี)

#### ขั้นตอนการติดตั้ง:

1. **ดาวน์โหลด Visual Studio**
   - ไปที่: https://visualstudio.microsoft.com/downloads/
   - เลือก **Visual Studio 2022 Community** (ฟรี)
   - ดาวน์โหลดและเปิดไฟล์

2. **เลือก Workloads**
   - ✅ **.NET desktop development** (สำหรับ Windows Forms)
   - ✅ **Data storage and processing** (สำหรับ SQL Server)
   
3. **Individual Components** (ถ้าต้องการ)
   - ✅ SQL Server Express LocalDB (ฐานข้อมูลในเครื่อง)

4. **คลิก Install** และรอสักครู่ (ประมาณ 30-60 นาที)

---

### 2. ติดตั้ง SQL Server (ถ้ายังไม่มี)

#### ตัวเลือกที่ 1: SQL Server Express (แนะนำ - ฟรี)
1. ดาวน์โหลด: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
2. เลือก **Express Edition** (ฟรี)
3. ติดตั้งแบบ **Basic**

#### ตัวเลือกที่ 2: SQL Server LocalDB (มากับ Visual Studio)
- ใช้ได้เลยถ้าติดตั้ง Visual Studio แล้ว
- เหมาะสำหรับการพัฒนาและเรียนรู้

#### ติดตั้ง SQL Server Management Studio (SSMS)
1. ดาวน์โหลด: https://aka.ms/ssmsfullsetup
2. ติดตั้งตามปกติ
3. ใช้จัดการฐานข้อมูลได้ง่ายขึ้น

---

## 🏗️ สร้างโปรเจกต์ใหม่

### ขั้นตอนที่ 1: เปิด Visual Studio

1. เปิด **Visual Studio 2022**
2. คลิก **Create a new project**

### ขั้นตอนที่ 2: เลือก Template

1. ค้นหา: **Windows Forms App (.NET Framework)**
2. เลือก **Windows Forms App (.NET Framework)** (ไม่ใช่ .NET Core)
3. คลิก **Next**

### ขั้นตอนที่ 3: ตั้งค่าโปรเจกต์

```
Project name: HospitalMS.UI
Location: C:\Users\Marke\Desktop\HospitalMS
Solution name: HospitalManagementSystem
Framework: .NET Framework 4.7.2 หรือสูงกว่า
```

4. คลิก **Create**

---

## 📁 สร้างโครงสร้างโปรเจกต์ (3-Tier Architecture)

### ขั้นตอนที่ 1: เพิ่ม Class Library Projects

1. **คลิกขวาที่ Solution** → **Add** → **New Project**
2. เลือก **Class Library (.NET Framework)**
3. สร้าง 3 โปรเจกต์:

```
HospitalMS.DAL   (Data Access Layer - จัดการฐานข้อมูล)
HospitalMS.BLL   (Business Logic Layer - ตรรกะทางธุรกิจ)
HospitalMS.Common (Shared Utilities - เครื่องมือร่วม)
```

### ขั้นตอนที่ 2: โครงสร้างที่ได้

```
Solution 'HospitalManagementSystem'
├── HospitalMS.UI           (Windows Forms - หน้าจอ)
├── HospitalMS.BLL          (Business Logic - ตรรกะ)
├── HospitalMS.DAL          (Data Access - ฐานข้อมูล)
└── HospitalMS.Common       (Utilities - เครื่องมือ)
```

---

## 🔗 เชื่อมโยงโปรเจกต์ (Add References)

### HospitalMS.UI ต้องอ้างอิง:
- HospitalMS.BLL
- HospitalMS.Common

**วิธีเพิ่ม Reference:**
1. คลิกขวาที่ **References** ใน HospitalMS.UI
2. เลือก **Add Reference**
3. เลือก **Projects** → เลือก **HospitalMS.BLL** และ **HospitalMS.Common**
4. คลิก **OK**

### HospitalMS.BLL ต้องอ้างอิง:
- HospitalMS.DAL
- HospitalMS.Common

### HospitalMS.DAL ต้องอ้างอิง:
- HospitalMS.Common

---

## 📦 ติดตั้ง NuGet Packages

### ติดตั้งใน HospitalMS.DAL:

1. คลิกขวาที่โปรเจกต์ **HospitalMS.DAL**
2. เลือก **Manage NuGet Packages**
3. คลิก **Browse**
4. ค้นหาและติดตั้ง:

```
✅ System.Data.SqlClient (สำหรับเชื่อมต่อ SQL Server)
✅ Dapper (Optional - ช่วยทำงานกับ Database ง่ายขึ้น)
```

---

## 🗂️ สร้างโฟลเดอร์ในแต่ละโปรเจกต์

### HospitalMS.UI
```
Forms/
├── Login/
├── Dashboard/
├── Patients/
├── Doctors/
├── Appointments/
└── Billing/

UserControls/
Resources/
```

### HospitalMS.BLL
```
Services/
Validators/
```

### HospitalMS.DAL
```
Models/
Repositories/
```

### HospitalMS.Common
```
Helpers/
Constants/
Extensions/
```

**วิธีสร้างโฟลเดอร์:**
1. คลิกขวาที่โปรเจกต์
2. เลือก **Add** → **New Folder**
3. ตั้งชื่อตามด้านบน

---

## 🎨 ปรับแต่ง Form1 เป็น LoginForm

### ขั้นตอนที่ 1: เปลี่ยนชื่อไฟล์

1. ใน **HospitalMS.UI** → คลิกขวาที่ **Form1.cs**
2. เลือก **Rename** → เปลี่ยนเป็น **LoginForm.cs**
3. ตอบ **Yes** เมื่อถามว่าจะเปลี่ยนชื่อ class ด้วยไหม

### ขั้นตอนที่ 2: ย้ายไฟล์

1. ลาก **LoginForm.cs** ไปใส่ในโฟลเดอร์ **Forms/Login/**

---

## ⚙️ ตั้งค่า App.config (Connection String)

### เปิดไฟล์ App.config ใน HospitalMS.UI

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <!-- สำหรับ SQL Server Express -->
    <add name="HospitalDB" 
         connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True" 
         providerName="System.Data.SqlClient"/>
    
    <!-- หรือ สำหรับ LocalDB -->
    <!--
    <add name="HospitalDB" 
         connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HospitalDB.mdf;Integrated Security=True" 
         providerName="System.Data.SqlClient"/>
    -->
  </connectionStrings>
  
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
  </startup>
</configuration>
```

**อธิบาย:**
- `Data Source` = ชื่อ Server (.\SQLEXPRESS หรือ (LocalDB)\MSSQLLocalDB)
- `Initial Catalog` = ชื่อฐานข้อมูล
- `Integrated Security=True` = ใช้ Windows Authentication

---

## ✅ ทดสอบว่าโปรเจกต์ทำงานได้

### กด F5 หรือคลิก Start

- ถ้าเห็นหน้าต่าง Form ขึ้นมา = **สำเร็จ!** ✅
- ถ้า Error = ตรวจสอบ .NET Framework version

---

## 📚 ไฟล์ที่จะสร้างในขั้นตอนต่อไป

1. **Database Schema** (สคริปต์สร้างฐานข้อมูล)
2. **DatabaseHelper.cs** (เชื่อมต่อฐานข้อมูล)
3. **LoginForm.cs** (หน้าจอ Login)
4. **Patient.cs** (Model ผู้ป่วย)
5. **PatientRepository.cs** (จัดการข้อมูลผู้ป่วย)

---

## 🎯 สรุป

คุณได้:
✅ ติดตั้ง Visual Studio 2022  
✅ ติดตั้ง SQL Server  
✅ สร้างโปรเจกต์ Windows Forms  
✅ สร้างโครงสร้าง 3-Tier Architecture  
✅ เชื่อมโยงโปรเจกต์  
✅ ตั้งค่า Connection String  

---

## 🚀 ขั้นตอนต่อไป

1. สร้างฐานข้อมูล (Database Schema)
2. สร้าง DatabaseHelper Class
3. ออกแบบ Login Form
4. เขียนโค้ด Login

**พร้อมไปต่อแล้วใช่ไหมครับ?** 💪
