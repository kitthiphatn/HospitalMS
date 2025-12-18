# 🛠️ บทที่ 7: ASP.NET Web API - Setup & Implementation

## 📋 เป้าหมายของบทนี้

ในบทนี้คุณจะได้เรียนรู้:
- ✅ สร้าง ASP.NET Web API Project
- ✅ ตั้งค่า CORS (Cross-Origin Resource Sharing)
- ✅ เชื่อมต่อกับ HospitalDB
- ✅ สร้าง Patients API แบบสมบูรณ์
- ✅ ทดสอบด้วย Postman/Browser

---

## 🚀 ขั้นตอนที่ 1: สร้าง Web API Project

### วิธีที่ 1: ใช้ Visual Studio (แนะนำ)

1. **เปิด Visual Studio 2022**

2. **สร้าง Project ใหม่**
   - คลิก **Create a new project**
   - ค้นหา **"ASP.NET Core Web API"**
   - เลือก **ASP.NET Core Web API** (C#)
   - คลิก **Next**

3. **ตั้งค่า Project**
   - **Project name:** `HospitalMS.API`
   - **Location:** `C:\Users\Marke\Desktop\C# hospital\HospitalMS\`
   - คลิก **Next**

4. **Additional Information**
   - **Framework:** .NET 6.0 หรือ .NET 8.0
   - **Authentication type:** None (จะทำเองทีหลัง)
   - ✅ ติ๊ก **Use controllers**
   - ✅ ติ๊ก **Enable OpenAPI support** (Swagger)
   - ❌ ไม่ติ๊ก **Configure for HTTPS** (ทำทีหลัง)
   - คลิก **Create**

---

### วิธีที่ 2: ใช้ Command Line

```powershell
cd "C:\Users\Marke\Desktop\C# hospital\HospitalMS"
dotnet new webapi -n HospitalMS.API
cd HospitalMS.API
```

---

## 📁 โครงสร้าง Project ที่ได้

```
HospitalMS.API/
├── Controllers/
│   └── WeatherForecastController.cs  ← ตัวอย่าง (ลบได้)
├── Properties/
│   └── launchSettings.json
├── appsettings.json                   ← ตั้งค่า Connection String
├── Program.cs                         ← ไฟล์หลัก
└── HospitalMS.API.csproj
```

---

## 🔧 ขั้นตอนที่ 2: ตั้งค่า Connection String

### แก้ไขไฟล์ `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "HospitalDB": "Data Source=.\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 🔌 ขั้นตอนที่ 3: เพิ่ม Reference ไปยัง HospitalMS.DAL

### วิธีที่ 1: ใช้ Visual Studio

1. คลิกขวาที่ **Dependencies** ใน HospitalMS.API
2. เลือก **Add Project Reference...**
3. ติ๊กถูก **HospitalMS.DAL**
4. คลิก **OK**

### วิธีที่ 2: ใช้ Command Line

```powershell
cd HospitalMS.API
dotnet add reference ../HospitalManagementSystem/HospitalMS.DAL/HospitalMS.DAL.csproj
```

---

## ⚙️ ขั้นตอนที่ 4: ตั้งค่า CORS

### แก้ไขไฟล์ `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// เพิ่ม CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ใช้ CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
```

**อธิบาย CORS:**
- **CORS** = Cross-Origin Resource Sharing
- อนุญาตให้ Web App จาก domain อื่นเรียกใช้ API ได้
- เหมือนกับ Next.js ที่ต้องตั้งค่า `headers` ใน `next.config.js`

---

## 👥 ขั้นตอนที่ 5: สร้าง Patients Controller

### สร้างไฟล์ `Controllers/PatientsController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using HospitalMS.DAL;
using HospitalMS.DAL.Models;

namespace HospitalMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PatientsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/patients
        [HttpGet]
        public IActionResult GetAllPatients()
        {
            try
            {
                string query = @"SELECT PatientID, PatientCode, FirstName, LastName, 
                                DateOfBirth, Gender, BloodGroup, Phone, Email 
                                FROM Patients WHERE IsActive = 1";
                
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);
                
                return Ok(new
                {
                    success = true,
                    data = dt,
                    count = dt.Rows.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving patients",
                    error = ex.Message
                });
            }
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {
            try
            {
                string query = @"SELECT * FROM Patients WHERE PatientID = @PatientID";
                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", id)
                };
                
                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                
                if (dt.Rows.Count == 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Patient with ID {id} not found"
                    });
                }
                
                return Ok(new
                {
                    success = true,
                    data = dt.Rows[0]
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving patient",
                    error = ex.Message
                });
            }
        }

        // POST: api/patients
        [HttpPost]
        public IActionResult CreatePatient([FromBody] PatientRequest request)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(request.FirstName))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "First name is required"
                    });
                }

                string query = @"INSERT INTO Patients 
                    (PatientCode, FirstName, LastName, DateOfBirth, Gender, 
                     BloodGroup, Phone, Email, Address, IsActive, CreatedDate)
                    VALUES 
                    (@PatientCode, @FirstName, @LastName, @DateOfBirth, @Gender,
                     @BloodGroup, @Phone, @Email, @Address, 1, GETDATE());
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientCode", request.PatientCode),
                    new SqlParameter("@FirstName", request.FirstName),
                    new SqlParameter("@LastName", request.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@DateOfBirth", request.DateOfBirth),
                    new SqlParameter("@Gender", request.Gender),
                    new SqlParameter("@BloodGroup", request.BloodGroup ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", request.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@Email", request.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Address", request.Address ?? (object)DBNull.Value)
                };

                var newId = DatabaseHelper.ExecuteScalar(query, parameters);

                return CreatedAtAction(nameof(GetPatient), new { id = newId }, new
                {
                    success = true,
                    message = "Patient created successfully",
                    data = new { patientId = newId }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error creating patient",
                    error = ex.Message
                });
            }
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] PatientRequest request)
        {
            try
            {
                string query = @"UPDATE Patients SET 
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Phone = @Phone,
                    Email = @Email,
                    Address = @Address,
                    ModifiedDate = GETDATE()
                    WHERE PatientID = @PatientID";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", id),
                    new SqlParameter("@FirstName", request.FirstName),
                    new SqlParameter("@LastName", request.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", request.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@Email", request.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Address", request.Address ?? (object)DBNull.Value)
                };

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected == 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Patient with ID {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Patient updated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error updating patient",
                    error = ex.Message
                });
            }
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                // Soft delete
                string query = @"UPDATE Patients SET IsActive = 0, ModifiedDate = GETDATE() 
                                WHERE PatientID = @PatientID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", id)
                };

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected == 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Patient with ID {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Patient deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error deleting patient",
                    error = ex.Message
                });
            }
        }

        // GET: api/patients/search?q=John
        [HttpGet("search")]
        public IActionResult SearchPatients([FromQuery] string q)
        {
            try
            {
                string query = @"SELECT PatientID, PatientCode, FirstName, LastName, 
                                Phone, Email FROM Patients 
                                WHERE IsActive = 1 
                                AND (FirstName LIKE @Search OR LastName LIKE @Search 
                                     OR PatientCode LIKE @Search)";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@Search", $"%{q}%")
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                return Ok(new
                {
                    success = true,
                    data = dt,
                    count = dt.Rows.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error searching patients",
                    error = ex.Message
                });
            }
        }
    }

    // Request Model
    public class PatientRequest
    {
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
```

---

## 🧪 ขั้นตอนที่ 6: ทดสอบ API

### 1. รันโปรแกรม

```powershell
cd HospitalMS.API
dotnet run
```

หรือกด **F5** ใน Visual Studio

### 2. เปิด Swagger UI

เปิด Browser ไปที่:
```
https://localhost:7xxx/swagger
```

(Port อาจจะต่างกัน ดูใน Console)

### 3. ทดสอบ API Endpoints

**GET /api/patients** - ดึงผู้ป่วยทั้งหมด
```
Response:
{
  "success": true,
  "data": [...],
  "count": 5
}
```

**GET /api/patients/1** - ดึงผู้ป่วย ID 1
```
Response:
{
  "success": true,
  "data": {
    "patientId": 1,
    "firstName": "Vichai",
    "lastName": "Mangmee",
    ...
  }
}
```

**POST /api/patients** - เพิ่มผู้ป่วยใหม่
```json
Request Body:
{
  "patientCode": "P2024007",
  "firstName": "Test",
  "lastName": "Patient",
  "dateOfBirth": "2000-01-01",
  "gender": "Male",
  "bloodGroup": "O+",
  "phone": "086-000-0000"
}
```

---

## 🔍 ทดสอบด้วย Postman

### 1. ดาวน์โหลด Postman
https://www.postman.com/downloads/

### 2. สร้าง Collection ใหม่
- Collection Name: `Hospital MS API`

### 3. เพิ่ม Requests

**GET All Patients:**
```
GET https://localhost:7xxx/api/patients
```

**GET Patient by ID:**
```
GET https://localhost:7xxx/api/patients/1
```

**POST Create Patient:**
```
POST https://localhost:7xxx/api/patients
Content-Type: application/json

{
  "patientCode": "P2024008",
  "firstName": "Jane",
  "lastName": "Smith",
  "dateOfBirth": "1992-05-20",
  "gender": "Female",
  "bloodGroup": "A+",
  "phone": "087-111-2222",
  "email": "jane@email.com"
}
```

---

## 📊 เปรียบเทียบกับ Next.js

### Next.js API Route:
```javascript
// app/api/patients/route.js
export async function GET() {
  const patients = await db.patients.findMany();
  return Response.json({ success: true, data: patients });
}
```

### ASP.NET Web API:
```csharp
// Controllers/PatientsController.cs
[HttpGet]
public IActionResult GetAllPatients()
{
    var patients = GetPatients();
    return Ok(new { success = true, data = patients });
}
```

**เห็นไหมครับ? เหมือนกันมาก!**

---

## 🎯 สรุป

ในบทนี้เราได้:
- ✅ สร้าง ASP.NET Web API Project
- ✅ ตั้งค่า CORS
- ✅ เชื่อมต่อกับ HospitalDB
- ✅ สร้าง Patients API ครบทุก CRUD
- ✅ ทดสอบด้วย Swagger และ Postman

**ขั้นตอนต่อไป:**
- เพิ่ม Authentication (JWT)
- สร้าง Doctors API
- สร้าง Appointments API
- Deploy API

---

**พร้อมไปต่อหรือยังครับ?** 🚀
