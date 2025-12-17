# 🔧 บทที่ 5: DatabaseHelper - เชื่อมต่อฐานข้อมูลด้วย C#

## 📋 เป้าหมายของบทนี้

ในบทนี้คุณจะได้เรียนรู้:
- ✅ DatabaseHelper คืออะไร และทำงานอย่างไร
- ✅ วิธีเชื่อมต่อกับ SQL Server จาก C#
- ✅ การใช้งาน Connection String
- ✅ ฟังก์ชันพื้นฐานสำหรับทำงานกับฐานข้อมูล
- ✅ วิธีทดสอบการเชื่อมต่อ

---

## 🎯 DatabaseHelper คืออะไร?

**DatabaseHelper** เป็น Class ที่ช่วยให้เราทำงานกับฐานข้อมูลได้ง่ายขึ้น โดย:

### ปัญหาที่แก้:
❌ **ไม่ใช้ DatabaseHelper** → ต้องเขียนโค้ดเชื่อมต่อซ้ำๆ ทุกครั้ง  
✅ **ใช้ DatabaseHelper** → เขียนครั้งเดียว ใช้ได้ทุกที่

### หน้าที่หลัก:
1. **เชื่อมต่อฐานข้อมูล** - จัดการ Connection String
2. **รัน SQL Query** - SELECT, INSERT, UPDATE, DELETE
3. **จัดการ Error** - แสดง Error ที่เข้าใจง่าย
4. **ปิด Connection อัตโนมัติ** - ป้องกัน Memory Leak

---

## 📁 โครงสร้างไฟล์

```
HospitalMS.DAL/
├── DatabaseHelper.cs          ← ไฟล์หลัก (เชื่อมต่อฐานข้อมูล)
├── Models/                    ← โฟลเดอร์เก็บ Model Classes
│   ├── User.cs
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Appointment.cs
│   └── Medicine.cs
└── Repositories/              ← โฟลเดอร์เก็บ Repository Classes
    ├── UserRepository.cs
    ├── PatientRepository.cs
    └── DoctorRepository.cs
```

---

## 🔌 Connection String คืออะไร?

**Connection String** คือข้อความที่บอกว่าจะเชื่อมต่อกับฐานข้อมูลไหน อย่างไร

### ตัวอย่าง Connection String:

```xml
<connectionStrings>
  <add name="HospitalDB" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### อธิบายแต่ละส่วน:

| ส่วน | ความหมาย | ตัวอย่าง |
|------|----------|----------|
| **Data Source** | ชื่อ SQL Server | `.\SQLEXPRESS` หรือ `localhost` |
| **Initial Catalog** | ชื่อฐานข้อมูล | `HospitalDB` |
| **Integrated Security** | ใช้ Windows Authentication | `True` |
| **User ID** | Username (ถ้าใช้ SQL Auth) | `sa` |
| **Password** | Password (ถ้าใช้ SQL Auth) | `yourpassword` |

---

## 💻 DatabaseHelper.cs - โค้ดหลัก

### ฟังก์ชันสำคัญ:

#### 1. **GetConnection()** - สร้าง Connection
```csharp
public static SqlConnection GetConnection()
{
    string connectionString = ConfigurationManager.ConnectionStrings["HospitalDB"].ConnectionString;
    return new SqlConnection(connectionString);
}
```

**ทำอะไร:** อ่าน Connection String จาก App.config แล้วสร้าง SqlConnection

---

#### 2. **TestConnection()** - ทดสอบการเชื่อมต่อ
```csharp
public static bool TestConnection()
{
    try
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            return true;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Connection Error: " + ex.Message);
        return false;
    }
}
```

**ทำอะไร:** ลองเปิด Connection ถ้าสำเร็จ return true, ถ้าไม่สำเร็จ return false

---

#### 3. **ExecuteNonQuery()** - รัน INSERT/UPDATE/DELETE
```csharp
public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
{
    using (SqlConnection conn = GetConnection())
    {
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
```

**ทำอะไร:** รัน SQL ที่ไม่ต้องการผลลัพธ์ (INSERT, UPDATE, DELETE)  
**Return:** จำนวนแถวที่ได้รับผลกระทบ

**ตัวอย่างการใช้:**
```csharp
string query = "INSERT INTO Patients (PatientCode, FirstName, LastName) VALUES (@Code, @First, @Last)";
SqlParameter[] parameters = {
    new SqlParameter("@Code", "P2024006"),
    new SqlParameter("@First", "John"),
    new SqlParameter("@Last", "Doe")
};
int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
```

---

#### 4. **ExecuteScalar()** - รัน SELECT ที่ได้ค่าเดียว
```csharp
public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
{
    using (SqlConnection conn = GetConnection())
    {
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }
}
```

**ทำอะไร:** รัน SQL ที่ได้ค่าเดียว (COUNT, MAX, SUM, ฯลฯ)  
**Return:** ค่าที่ได้จาก Query

**ตัวอย่างการใช้:**
```csharp
string query = "SELECT COUNT(*) FROM Patients";
int totalPatients = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
```

---

#### 5. **ExecuteDataTable()** - รัน SELECT ที่ได้หลายแถว
```csharp
public static DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
{
    using (SqlConnection conn = GetConnection())
    {
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
```

**ทำอะไร:** รัน SQL แล้วคืนค่าเป็น DataTable  
**Return:** DataTable ที่มีข้อมูลทั้งหมด

**ตัวอย่างการใช้:**
```csharp
string query = "SELECT * FROM Patients WHERE Gender = @Gender";
SqlParameter[] parameters = { new SqlParameter("@Gender", "Male") };
DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(row["FirstName"] + " " + row["LastName"]);
}
```

---

## 🧪 วิธีทดสอบ DatabaseHelper

### ขั้นตอนที่ 1: ตั้งค่า App.config

ในโปรเจค **HospitalMS.UI** ให้เปิดไฟล์ `App.config` แล้วเพิ่ม:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="HospitalDB" 
         connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True" 
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
  
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
  </startup>
</configuration>
```

---

### ขั้นตอนที่ 2: สร้างโปรแกรมทดสอบ

ในโปรเจค **HospitalMS.UI** ให้เปิดไฟล์ `Program.cs` แล้วแก้ไขเป็น:

```csharp
using System;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace HospitalMS.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // ทดสอบการเชื่อมต่อฐานข้อมูล
            if (DatabaseHelper.TestConnection())
            {
                MessageBox.Show("เชื่อมต่อฐานข้อมูลสำเร็จ!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("เชื่อมต่อฐานข้อมูลไม่สำเร็จ!", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
```

---

### ขั้นตอนที่ 3: รันโปรแกรม

**วิธีรัน:**
1. เปิด Visual Studio
2. กด **F5** (หรือคลิก Start)
3. ถ้าเชื่อมต่อสำเร็จ จะเห็น MessageBox "เชื่อมต่อฐานข้อมูลสำเร็จ!"

---

## 🔍 แก้ปัญหาที่พบบ่อย

### ❌ ปัญหา: "Cannot open database"

**สาเหตุ:** ฐานข้อมูล HospitalDB ยังไม่ถูกสร้าง

**วิธีแก้:**
1. เปิด SSMS
2. รันสคริปต์ `01_CreateDatabase.sql`

---

### ❌ ปัญหา: "Login failed for user"

**สาเหตุ:** Connection String ไม่ถูกต้อง

**วิธีแก้:**
- ตรวจสอบ `Data Source` ว่าถูกต้องหรือไม่
- ลอง: `.\SQLEXPRESS`, `localhost`, `(LocalDB)\MSSQLLocalDB`

---

### ❌ ปัญหา: "Could not load file System.Configuration"

**สาเหตุ:** ยังไม่ได้เพิ่ม Reference

**วิธีแก้:**
1. คลิกขวาที่ **References** ใน HospitalMS.DAL
2. เลือก **Add Reference**
3. เลือก **Assemblies** → **Framework**
4. ติ๊ก **System.Configuration**
5. คลิก **OK**

---

## 📊 ตัวอย่างการใช้งานจริง

### ตัวอย่างที่ 1: นับจำนวนผู้ป่วย

```csharp
string query = "SELECT COUNT(*) FROM Patients";
int total = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
MessageBox.Show($"จำนวนผู้ป่วยทั้งหมด: {total} คน");
```

---

### ตัวอย่างที่ 2: ดึงรายชื่อผู้ป่วย

```csharp
string query = "SELECT PatientCode, FirstName, LastName FROM Patients";
DataTable dt = DatabaseHelper.ExecuteDataTable(query);

foreach (DataRow row in dt.Rows)
{
    string code = row["PatientCode"].ToString();
    string name = $"{row["FirstName"]} {row["LastName"]}";
    Console.WriteLine($"{code}: {name}");
}
```

---

### ตัวอย่างที่ 3: เพิ่มผู้ป่วยใหม่

```csharp
string query = @"INSERT INTO Patients 
    (PatientCode, FirstName, LastName, DateOfBirth, Gender, BloodGroup, Phone) 
    VALUES (@Code, @First, @Last, @DOB, @Gender, @Blood, @Phone)";

SqlParameter[] parameters = {
    new SqlParameter("@Code", "P2024006"),
    new SqlParameter("@First", "John"),
    new SqlParameter("@Last", "Doe"),
    new SqlParameter("@DOB", new DateTime(1990, 1, 1)),
    new SqlParameter("@Gender", "Male"),
    new SqlParameter("@Blood", "O+"),
    new SqlParameter("@Phone", "086-666-6666")
};

int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
MessageBox.Show($"เพิ่มผู้ป่วยสำเร็จ! ({rows} row affected)");
```

---

## 🎯 สรุป

ในบทนี้คุณได้เรียนรู้:

✅ **DatabaseHelper** คืออะไร และทำงานอย่างไร  
✅ **Connection String** - วิธีเชื่อมต่อกับฐานข้อมูล  
✅ **ฟังก์ชันหลัก** - ExecuteNonQuery, ExecuteScalar, ExecuteDataTable  
✅ **วิธีทดสอบ** - ตรวจสอบว่าเชื่อมต่อได้หรือไม่  
✅ **แก้ปัญหา** - วิธีแก้ Error ที่พบบ่อย  

---

## 🚀 ขั้นตอนต่อไป

1. **สร้าง Model Classes** - Patient.cs, Doctor.cs, ฯลฯ
2. **สร้าง Repository Classes** - PatientRepository.cs
3. **สร้าง Login Form** - หน้าจอ Login
4. **ทดสอบ Login** - ใช้ username: admin, password: admin123

**พร้อมไปต่อแล้วใช่ไหมครับ?** 💪
