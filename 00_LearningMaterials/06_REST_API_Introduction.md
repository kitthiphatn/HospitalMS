# 🌐 บทที่ 6: REST API - Introduction

## 📋 เป้าหมายของบทนี้

ในบทนี้คุณจะได้เรียนรู้:
- ✅ REST API คืออะไร และทำงานอย่างไร
- ✅ เปรียบเทียบกับ Next.js API Routes ที่คุณรู้อยู่แล้ว
- ✅ HTTP Methods (GET, POST, PUT, DELETE)
- ✅ JSON Request/Response
- ✅ การออกแบบ API Endpoints

---

## 🎯 REST API คืออะไร?

**REST API** (Representational State Transfer API) คือ Web Service ที่ให้โปรแกรมอื่นๆ เรียกใช้งานผ่าน HTTP

### ตัวอย่างการใช้งาน:

```
Mobile App  ──┐
              ├──→ REST API ──→ Database
Web App     ──┘
```

**ประโยชน์:**
- ✅ แยก Frontend กับ Backend
- ✅ Mobile App ใช้ API เดียวกับ Web App
- ✅ ง่ายต่อการพัฒนาและบำรุงรักษา

---

## 🔄 เปรียบเทียบ: Next.js vs C# REST API

### คุณรู้อยู่แล้ว: Next.js API Routes

```javascript
// app/api/patients/route.js
export async function GET(request) {
  const patients = await db.patients.findMany();
  return Response.json(patients);
}

export async function POST(request) {
  const data = await request.json();
  const patient = await db.patients.create({ data });
  return Response.json(patient);
}
```

### ใน C# จะเป็น: ASP.NET Web API

```csharp
// Controllers/PatientsController.cs
[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetPatients()
    {
        var patients = DatabaseHelper.ExecuteDataTable("SELECT * FROM Patients");
        return Ok(patients);
    }

    [HttpPost]
    public IActionResult CreatePatient([FromBody] Patient patient)
    {
        // บันทึกลงฐานข้อมูล
        return Ok(patient);
    }
}
```

### สังเกตความเหมือน:

| แนวคิด | Next.js | C# ASP.NET |
|--------|---------|------------|
| **Route** | `app/api/patients/route.js` | `[Route("api/patients")]` |
| **GET Method** | `export async function GET()` | `[HttpGet]` |
| **POST Method** | `export async function POST()` | `[HttpPost]` |
| **Return JSON** | `Response.json(data)` | `Ok(data)` |
| **Request Body** | `await request.json()` | `[FromBody] Patient` |

**เห็นไหมครับ? แนวคิดเหมือนกัน 100%!**

---

## 📡 HTTP Methods

### 1. **GET** - ดึงข้อมูล

**Next.js:**
```javascript
export async function GET() {
  const data = await fetchData();
  return Response.json(data);
}
```

**C#:**
```csharp
[HttpGet]
public IActionResult Get()
{
    var data = GetData();
    return Ok(data);
}
```

**ตัวอย่าง URL:**
```
GET /api/patients           → ดึงผู้ป่วยทั้งหมด
GET /api/patients/1         → ดึงผู้ป่วย ID 1
GET /api/patients?name=John → ค้นหาผู้ป่วยชื่อ John
```

---

### 2. **POST** - เพิ่มข้อมูลใหม่

**Next.js:**
```javascript
export async function POST(request) {
  const data = await request.json();
  const result = await createData(data);
  return Response.json(result);
}
```

**C#:**
```csharp
[HttpPost]
public IActionResult Create([FromBody] Patient patient)
{
    var result = CreatePatient(patient);
    return Ok(result);
}
```

**ตัวอย่าง Request:**
```json
POST /api/patients
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1990-01-01",
  "gender": "Male"
}
```

---

### 3. **PUT** - แก้ไขข้อมูล

**Next.js:**
```javascript
export async function PUT(request) {
  const data = await request.json();
  const result = await updateData(data);
  return Response.json(result);
}
```

**C#:**
```csharp
[HttpPut("{id}")]
public IActionResult Update(int id, [FromBody] Patient patient)
{
    var result = UpdatePatient(id, patient);
    return Ok(result);
}
```

**ตัวอย่าง Request:**
```json
PUT /api/patients/1
Content-Type: application/json

{
  "firstName": "John Updated",
  "lastName": "Doe",
  "phone": "086-123-4567"
}
```

---

### 4. **DELETE** - ลบข้อมูล

**Next.js:**
```javascript
export async function DELETE(request) {
  const { id } = await request.json();
  await deleteData(id);
  return Response.json({ success: true });
}
```

**C#:**
```csharp
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    DeletePatient(id);
    return Ok(new { success = true });
}
```

**ตัวอย่าง Request:**
```
DELETE /api/patients/1
```

---

## 🎨 การออกแบบ API Endpoints

### ✅ Best Practices:

#### 1. **ใช้ Nouns (คำนาม) ไม่ใช่ Verbs (คำกริยา)**

❌ **ไม่ดี:**
```
GET  /api/getPatients
POST /api/createPatient
PUT  /api/updatePatient
```

✅ **ดี:**
```
GET    /api/patients
POST   /api/patients
PUT    /api/patients/1
DELETE /api/patients/1
```

---

#### 2. **ใช้ Plural (พหูพจน์)**

❌ **ไม่ดี:**
```
GET /api/patient
GET /api/doctor
```

✅ **ดี:**
```
GET /api/patients
GET /api/doctors
```

---

#### 3. **ใช้ Nested Resources สำหรับความสัมพันธ์**

```
GET /api/patients/1/appointments      → นัดหมายของผู้ป่วย ID 1
GET /api/doctors/2/appointments       → นัดหมายของหมอ ID 2
GET /api/patients/1/medical-records   → ประวัติการรักษาของผู้ป่วย ID 1
```

---

#### 4. **ใช้ Query Parameters สำหรับ Filter/Sort/Pagination**

```
GET /api/patients?gender=Male                    → กรองเพศชาย
GET /api/patients?sort=lastName&order=asc        → เรียงตามนามสกุล
GET /api/patients?page=2&limit=10                → Pagination
GET /api/appointments?date=2024-12-17&status=Confirmed
```

---

## 📦 JSON Request/Response

### Request Example:

```json
POST /api/patients
Content-Type: application/json

{
  "patientCode": "P2024006",
  "firstName": "Sarah",
  "lastName": "Johnson",
  "dateOfBirth": "1995-03-15",
  "gender": "Female",
  "bloodGroup": "O+",
  "phone": "086-999-8888",
  "email": "sarah@email.com",
  "address": "456 Main St, Bangkok"
}
```

### Response Example (Success):

```json
HTTP/1.1 200 OK
Content-Type: application/json

{
  "success": true,
  "message": "Patient created successfully",
  "data": {
    "patientId": 6,
    "patientCode": "P2024006",
    "firstName": "Sarah",
    "lastName": "Johnson",
    "createdDate": "2024-12-17T10:30:00"
  }
}
```

### Response Example (Error):

```json
HTTP/1.1 400 Bad Request
Content-Type: application/json

{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "First name is required",
    "Invalid email format"
  ]
}
```

---

## 🔐 HTTP Status Codes

### Success Codes:

| Code | ความหมาย | ใช้เมื่อ |
|------|----------|----------|
| **200 OK** | สำเร็จ | GET, PUT, DELETE สำเร็จ |
| **201 Created** | สร้างสำเร็จ | POST สร้างข้อมูลใหม่สำเร็จ |
| **204 No Content** | สำเร็จแต่ไม่มีข้อมูล | DELETE สำเร็จ |

### Error Codes:

| Code | ความหมาย | ใช้เมื่อ |
|------|----------|----------|
| **400 Bad Request** | Request ผิด | ข้อมูลไม่ครบ, Validation ผิด |
| **401 Unauthorized** | ไม่ได้ Login | ต้อง Login ก่อน |
| **403 Forbidden** | ไม่มีสิทธิ์ | Login แล้วแต่ไม่มีสิทธิ์ |
| **404 Not Found** | ไม่พบข้อมูล | ไม่มี ID นี้ในฐานข้อมูล |
| **500 Internal Server Error** | Server Error | เกิด Error ในโค้ด |

---

## 🎯 ตัวอย่าง API สำหรับ Hospital Management System

### Patients API:

```
GET    /api/patients                 → ดึงผู้ป่วยทั้งหมด
GET    /api/patients/1               → ดึงผู้ป่วย ID 1
POST   /api/patients                 → เพิ่มผู้ป่วยใหม่
PUT    /api/patients/1               → แก้ไขผู้ป่วย ID 1
DELETE /api/patients/1               → ลบผู้ป่วย ID 1
GET    /api/patients/search?q=John   → ค้นหาผู้ป่วย
```

### Doctors API:

```
GET    /api/doctors                  → ดึงหมอทั้งหมด
GET    /api/doctors/1                → ดึงหมอ ID 1
GET    /api/doctors?specialization=Cardiology
```

### Appointments API:

```
GET    /api/appointments             → ดึงนัดหมายทั้งหมด
POST   /api/appointments             → สร้างนัดหมายใหม่
PUT    /api/appointments/1/status    → เปลี่ยนสถานะนัดหมาย
GET    /api/appointments?date=2024-12-17
GET    /api/patients/1/appointments  → นัดหมายของผู้ป่วย ID 1
```

---

## 💡 เปรียบเทียบกับ Mini Chat AI

### Mini Chat AI (Next.js):

```javascript
// app/api/chat/route.js
export async function POST(request) {
  const { message, model } = await request.json();
  
  const response = await fetch(API_URL, {
    method: 'POST',
    body: JSON.stringify({ message, model })
  });
  
  const data = await response.json();
  return Response.json(data);
}
```

### Hospital MS (C# ASP.NET):

```csharp
// Controllers/AppointmentsController.cs
[HttpPost]
public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
{
    var appointment = new Appointment
    {
        PatientID = request.PatientID,
        DoctorID = request.DoctorID,
        AppointmentDate = request.Date,
        Status = "Pending"
    };
    
    var result = await SaveAppointment(appointment);
    return Ok(result);
}
```

**ความเหมือน:**
- ✅ รับ JSON Request
- ✅ ประมวลผล
- ✅ ส่ง JSON Response
- ✅ จัดการ Error

---

## 🚀 ขั้นตอนต่อไป

ในบทถัดไปเราจะ:

1. **สร้าง ASP.NET Web API Project**
2. **เชื่อมต่อกับ HospitalDB**
3. **สร้าง Patients API**
4. **ทดสอบด้วย Postman**
5. **เพิ่ม Authentication**

---

## 📝 สรุป

**REST API:**
- ✅ เป็น Web Service ที่ใช้ HTTP
- ✅ ใช้ JSON สำหรับ Request/Response
- ✅ มี HTTP Methods: GET, POST, PUT, DELETE
- ✅ แนวคิดเหมือนกับ Next.js API Routes
- ✅ ง่ายต่อการเรียนรู้ถ้ามีพื้นฐาน

**ความแตกต่างหลัก:**
- Next.js = JavaScript, File-based routing
- ASP.NET = C#, Attribute-based routing
- แต่**แนวคิดเหมือนกัน 100%**

---

**พร้อมไปต่อบทถัดไปแล้วใช่ไหมครับ?** 🚀
