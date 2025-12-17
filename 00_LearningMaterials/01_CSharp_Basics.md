# 📚 C# พื้นฐาน - เริ่มต้นจากศูนย์

## 🎯 เป้าหมาย
เรียนรู้พื้นฐาน C# ที่จำเป็นสำหรับการสร้าง Hospital Management System

---

## 1️⃣ C# คืออะไร?

**C# (อ่านว่า "ซี ชาร์ป")** เป็นภาษาโปรแกรมที่พัฒนาโดย Microsoft ใช้สร้าง:
- โปรแกรมบน Windows (Desktop Applications)
- เว็บไซต์ (ASP.NET)
- เกม (Unity)
- แอพมือถือ (Xamarin)

### ทำไมต้องเรียน C#?
✅ ง่ายต่อการเรียนรู้  
✅ มีเครื่องมือช่วยเยอะ (Visual Studio)  
✅ ใช้งานได้จริงในองค์กร  
✅ เงินเดือนดี  

---

## 2️⃣ โครงสร้างพื้นฐาน

### ไฟล์ C# ทั่วไป
```csharp
using System;  // นำเข้า library (เหมือนเครื่องมือที่เราจะใช้)

namespace HospitalMS  // กลุ่มของโค้ด (เหมือนโฟลเดอร์)
{
    class Program  // คลาส (แม่แบบของโปรแกรม)
    {
        static void Main(string[] args)  // จุดเริ่มต้นของโปรแกรม
        {
            Console.WriteLine("Hello, Hospital!");  // แสดงข้อความ
        }
    }
}
```

### อธิบายทีละบรรทัด:
- `using System;` = นำเข้าเครื่องมือพื้นฐาน (เหมือนการเปิดกล่องเครื่องมือ)
- `namespace` = กลุ่มของโค้ด ป้องกันชื่อซ้ำกัน
- `class` = แม่แบบ (Blueprint) ของสิ่งที่เราจะสร้าง
- `Main` = จุดเริ่มต้นโปรแกรม (ทุกโปรแกรมต้องมี)
- `Console.WriteLine()` = แสดงข้อความบนหน้าจอ

---

## 3️⃣ ตัวแปร (Variables)

### ตัวแปรคืออะไร?
**ตัวแปร** = กล่องเก็บข้อมูล มีชื่อและชนิดข้อมูล

### ชนิดข้อมูลพื้นฐาน

```csharp
// ตัวเลขจำนวนเต็ม
int age = 25;                    // อายุ
int patientCount = 100;          // จำนวนผู้ป่วย

// ตัวเลขทศนิยม
decimal price = 150.50m;         // ราคา (ใช้ m ต้องมี)
double weight = 65.5;            // น้ำหนัก

// ข้อความ
string name = "สมชาย";           // ชื่อ
string hospital = "โรงพยาบาลกรุงเทพ";

// ค่าจริง/เท็จ
bool isActive = true;            // เปิดใช้งาน
bool isPaid = false;             // ยังไม่จ่ายเงิน

// วันที่
DateTime today = DateTime.Now;   // วันนี้
DateTime birthDate = new DateTime(1990, 5, 15);  // 15 พ.ค. 1990
```

### กฎการตั้งชื่อตัวแปร
✅ เริ่มด้วยตัวอักษร (a-z, A-Z)  
✅ ใช้ได้: `patientName`, `patient_name`, `patientName123`  
❌ ห้าม: `123patient`, `patient-name`, `patient name`  

### การตั้งชื่อแบบมาตรฐาน (Naming Convention)
```csharp
// camelCase - ตัวแปรทั่วไป
string firstName = "สมชาย";
int patientAge = 30;

// PascalCase - คลาส, เมธอด
class Patient { }
void SavePatient() { }

// UPPER_CASE - ค่าคงที่
const int MAX_PATIENTS = 1000;
```

---

## 4️⃣ การคำนวณ (Operators)

```csharp
// คณิตศาสตร์
int a = 10;
int b = 3;

int sum = a + b;        // 13 (บวก)
int diff = a - b;       // 7 (ลบ)
int product = a * b;    // 30 (คูณ)
int quotient = a / b;   // 3 (หาร - เอาเฉพาะจำนวนเต็ม)
int remainder = a % b;  // 1 (หารเอาเศษ)

// เพิ่ม/ลดค่า
int count = 0;
count++;                // count = 1 (เพิ่ม 1)
count--;                // count = 0 (ลด 1)
count += 5;             // count = 5 (เพิ่ม 5)

// เปรียบเทียบ
bool isEqual = (a == b);      // false (เท่ากันไหม)
bool isNotEqual = (a != b);   // true (ไม่เท่ากัน)
bool isGreater = (a > b);     // true (มากกว่า)
bool isLess = (a < b);        // false (น้อยกว่า)

// ตรกะ (Logic)
bool result1 = true && false;  // false (และ - ทั้งสองต้องเป็น true)
bool result2 = true || false;  // true (หรือ - อย่างใดอย่างหนึ่งเป็น true)
bool result3 = !true;          // false (ไม่ - กลับค่า)
```

---

## 5️⃣ การควบคุมการทำงาน (Control Flow)

### If-Else (ถ้า-ไม่ใช่)
```csharp
int age = 65;

if (age >= 60)
{
    Console.WriteLine("ผู้สูงอายุ - ลด 50%");
}
else if (age >= 18)
{
    Console.WriteLine("ผู้ใหญ่ - ราคาปกติ");
}
else
{
    Console.WriteLine("เด็ก - ลด 20%");
}
```

### Switch-Case (เลือกกรณี)
```csharp
string bloodType = "A";

switch (bloodType)
{
    case "A":
        Console.WriteLine("กรุ๊ปเลือด A");
        break;
    case "B":
        Console.WriteLine("กรุ๊ปเลือด B");
        break;
    case "O":
        Console.WriteLine("กรุ๊ปเลือด O");
        break;
    case "AB":
        Console.WriteLine("กรุ๊ปเลือด AB");
        break;
    default:
        Console.WriteLine("ไม่ระบุ");
        break;
}
```

---

## 6️⃣ การวนซ้ำ (Loops)

### For Loop (วนตามจำนวนครั้ง)
```csharp
// แสดงเลข 1-5
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine("ครั้งที่ " + i);
}
// ผลลัพธ์:
// ครั้งที่ 1
// ครั้งที่ 2
// ครั้งที่ 3
// ครั้งที่ 4
// ครั้งที่ 5
```

### While Loop (วนตามเงื่อนไข)
```csharp
int count = 1;
while (count <= 3)
{
    Console.WriteLine("นับ: " + count);
    count++;
}
// ผลลัพธ์:
// นับ: 1
// นับ: 2
// นับ: 3
```

### Foreach Loop (วนใน Collection)
```csharp
string[] patients = { "สมชาย", "สมหญิง", "สมศรี" };

foreach (string patient in patients)
{
    Console.WriteLine("ผู้ป่วย: " + patient);
}
// ผลลัพธ์:
// ผู้ป่วย: สมชาย
// ผู้ป่วย: สมหญิง
// ผู้ป่วย: สมศรี
```

---

## 7️⃣ Array (อาเรย์ - ชุดข้อมูล)

```csharp
// ประกาศ Array
string[] doctors = new string[3];  // สร้างช่องว่าง 3 ช่อง
doctors[0] = "นพ.สมชาย";
doctors[1] = "นพ.สมหญิง";
doctors[2] = "นพ.สมศรี";

// หรือกำหนดค่าเลย
int[] roomNumbers = { 101, 102, 103, 104 };

// เข้าถึงข้อมูล
Console.WriteLine(doctors[0]);     // นพ.สมชาย
Console.WriteLine(roomNumbers[2]); // 103

// จำนวนข้อมูล
int count = doctors.Length;        // 3
```

---

## 8️⃣ List (ลิสต์ - ชุดข้อมูลที่ยืดหยุ่น)

```csharp
using System.Collections.Generic;  // ต้องมีบรรทัดนี้

// สร้าง List
List<string> patients = new List<string>();

// เพิ่มข้อมูล
patients.Add("สมชาย");
patients.Add("สมหญิง");
patients.Add("สมศรี");

// ลบข้อมูล
patients.Remove("สมหญิง");

// นับจำนวน
int total = patients.Count;  // 2

// เข้าถึงข้อมูล
string first = patients[0];  // สมชาย

// วนลูป
foreach (string patient in patients)
{
    Console.WriteLine(patient);
}
```

---

## 9️⃣ Method (เมธอด - ฟังก์ชัน)

### Method คืออะไร?
**Method** = กลุ่มของคำสั่งที่ทำงานเฉพาะอย่าง (เหมือนสูตรสำเร็จ)

```csharp
// Method ที่ไม่มีการคืนค่า (void)
void SayHello()
{
    Console.WriteLine("สวัสดี!");
}

// Method ที่มีพารามิเตอร์
void Greet(string name)
{
    Console.WriteLine("สวัสดี " + name);
}

// Method ที่คืนค่า
int Add(int a, int b)
{
    return a + b;
}

// Method คำนวณส่วนลด
decimal CalculateDiscount(decimal price, int ageGroup)
{
    if (ageGroup == 1) // เด็ก
        return price * 0.8m;  // ลด 20%
    else if (ageGroup == 3) // ผู้สูงอายุ
        return price * 0.5m;  // ลด 50%
    else
        return price;  // ราคาปกติ
}

// การเรียกใช้
SayHello();                    // แสดง: สวัสดี!
Greet("สมชาย");                // แสดง: สวัสดี สมชาย
int sum = Add(5, 3);           // sum = 8
decimal finalPrice = CalculateDiscount(100, 1);  // 80
```

---

## 🔟 Class และ Object (คลาสและออบเจ็กต์)

### Class คืออะไร?
**Class** = แม่แบบ (Blueprint) สำหรับสร้างสิ่งของ  
**Object** = สิ่งของที่สร้างจาก Class

### ตัวอย่าง: คลาสผู้ป่วย

```csharp
// สร้าง Class
class Patient
{
    // Properties (คุณสมบัติ)
    public string Name;
    public int Age;
    public string BloodType;
    
    // Method (พฤติกรรม)
    public void ShowInfo()
    {
        Console.WriteLine($"ชื่อ: {Name}");
        Console.WriteLine($"อายุ: {Age} ปี");
        Console.WriteLine($"กรุ๊ปเลือด: {BloodType}");
    }
    
    public bool IsSenior()
    {
        return Age >= 60;
    }
}

// การใช้งาน
Patient patient1 = new Patient();  // สร้าง Object
patient1.Name = "สมชาย ใจดี";
patient1.Age = 65;
patient1.BloodType = "A";

patient1.ShowInfo();  // แสดงข้อมูล

if (patient1.IsSenior())
{
    Console.WriteLine("ผู้สูงอายุ - มีส่วนลด");
}
```

### Constructor (ตัวสร้าง)
```csharp
class Patient
{
    public string Name;
    public int Age;
    
    // Constructor - ทำงานตอนสร้าง Object
    public Patient(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine("สร้างผู้ป่วยใหม่: " + name);
    }
}

// การใช้งาน
Patient p1 = new Patient("สมชาย", 30);  // แสดง: สร้างผู้ป่วยใหม่: สมชาย
```

---

## 1️⃣1️⃣ Properties (คุณสมบัติแบบมีการควบคุม)

```csharp
class Patient
{
    // Field (ตัวแปรภายใน)
    private string _name;
    private int _age;
    
    // Property - ควบคุมการเข้าถึง
    public string Name
    {
        get { return _name; }
        set 
        { 
            if (!string.IsNullOrEmpty(value))
                _name = value;
            else
                throw new Exception("ชื่อต้องไม่ว่าง!");
        }
    }
    
    public int Age
    {
        get { return _age; }
        set 
        { 
            if (value >= 0 && value <= 150)
                _age = value;
            else
                throw new Exception("อายุไม่ถูกต้อง!");
        }
    }
    
    // Auto-Property (สั้นกว่า)
    public string BloodType { get; set; }
}

// การใช้งาน
Patient p = new Patient();
p.Name = "สมชาย";  // ผ่าน set
p.Age = 25;        // ผ่าน set
string name = p.Name;  // ผ่าน get
```

---

## 1️⃣2️⃣ String Operations (การทำงานกับข้อความ)

```csharp
string firstName = "สมชาย";
string lastName = "ใจดี";

// รวมข้อความ
string fullName1 = firstName + " " + lastName;  // สมชาย ใจดี
string fullName2 = $"{firstName} {lastName}";   // สมชาย ใจดี (แนะนำ)

// ความยาว
int length = fullName1.Length;  // 11

// ตัวพิมพ์ใหญ่/เล็ก
string upper = firstName.ToUpper();  // สมชาย
string lower = firstName.ToLower();  // สมชาย

// ตัดช่องว่าง
string text = "  สวัสดี  ";
string trimmed = text.Trim();  // "สวัสดี"

// แทนที่
string message = "Hello World";
string newMsg = message.Replace("World", "Hospital");  // Hello Hospital

// แยกข้อความ
string data = "สมชาย,30,A";
string[] parts = data.Split(',');  // ["สมชาย", "30", "A"]

// ตรวจสอบ
bool isEmpty = string.IsNullOrEmpty(firstName);  // false
bool contains = fullName1.Contains("สมชาย");     // true
```

---

## 1️⃣3️⃣ Try-Catch (จัดการข้อผิดพลาด)

```csharp
try
{
    // โค้ดที่อาจเกิดข้อผิดพลาด
    int age = int.Parse("abc");  // จะ Error เพราะแปลงไม่ได้
}
catch (FormatException ex)
{
    // จัดการเมื่อเกิด Error
    Console.WriteLine("กรุณาใส่ตัวเลข!");
    Console.WriteLine("Error: " + ex.Message);
}
catch (Exception ex)
{
    // จัดการ Error ทั่วไป
    Console.WriteLine("เกิดข้อผิดพลาด: " + ex.Message);
}
finally
{
    // ทำงานเสมอ ไม่ว่าจะ Error หรือไม่
    Console.WriteLine("เสร็จสิ้น");
}
```

---

## 1️⃣4️⃣ Null Handling (จัดการค่าว่าง)

```csharp
// Nullable Types
int? age = null;  // int ที่สามารถเป็น null ได้

if (age.HasValue)
{
    Console.WriteLine("อายุ: " + age.Value);
}
else
{
    Console.WriteLine("ไม่ระบุอายุ");
}

// Null Coalescing Operator
string name = null;
string displayName = name ?? "ไม่ระบุชื่อ";  // ถ้า name เป็น null ใช้ "ไม่ระบุชื่อ"

// Null Conditional Operator
Patient patient = null;
string patientName = patient?.Name;  // ถ้า patient เป็น null จะได้ null ไม่ Error
```

---

## 📝 แบบฝึกหัด

### แบบฝึกหัดที่ 1: คำนวณค่ารักษา
```csharp
// สร้าง Method คำนวณค่ารักษา
// - ค่าตรวจ 500 บาท
// - เด็ก (อายุ < 18) ลด 20%
// - ผู้สูงอายุ (อายุ >= 60) ลด 50%
// - คนทั่วไป ราคาปกติ

decimal CalculateFee(int age)
{
    decimal baseFee = 500m;
    
    if (age < 18)
        return baseFee * 0.8m;  // ลด 20%
    else if (age >= 60)
        return baseFee * 0.5m;  // ลด 50%
    else
        return baseFee;
}

// ทดสอบ
Console.WriteLine(CalculateFee(10));  // 400
Console.WriteLine(CalculateFee(30));  // 500
Console.WriteLine(CalculateFee(65));  // 250
```

### แบบฝึกหัดที่ 2: สร้างคลาส Doctor
```csharp
class Doctor
{
    public string Name { get; set; }
    public string Specialization { get; set; }
    public decimal ConsultationFee { get; set; }
    
    public void ShowInfo()
    {
        Console.WriteLine($"ชื่อ: {Name}");
        Console.WriteLine($"ความเชี่ยวชาญ: {Specialization}");
        Console.WriteLine($"ค่าตรวจ: {ConsultationFee} บาท");
    }
}

// ทดสอบ
Doctor doc = new Doctor();
doc.Name = "นพ.สมชาย";
doc.Specialization = "ศัลยกรรม";
doc.ConsultationFee = 1000m;
doc.ShowInfo();
```

---

## 🎯 สรุป

คุณได้เรียนรู้:
✅ ตัวแปรและชนิดข้อมูล  
✅ การคำนวณและเปรียบเทียบ  
✅ If-Else, Switch, Loops  
✅ Array และ List  
✅ Method (Function)  
✅ Class และ Object  
✅ Properties  
✅ String Operations  
✅ Error Handling  

---

## 🚀 ขั้นตอนต่อไป

ตอนนี้คุณพร้อมแล้วที่จะ:
1. ติดตั้ง Visual Studio
2. สร้างโปรเจกต์ Hospital Management System
3. เริ่มเขียน Login Form
4. เชื่อมต่อ Database

**พร้อมเริ่มต้นแล้วใช่ไหมครับ?** 💪
