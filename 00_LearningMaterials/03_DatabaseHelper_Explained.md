# 💻 DatabaseHelper.cs - คำอธิบายโค้ด

## 📋 ภาพรวม

**DatabaseHelper** เป็นคลาสที่ช่วยจัดการการเชื่อมต่อและทำงานกับฐานข้อมูล SQL Server  
ใช้ **Singleton Pattern** เพื่อให้มี instance เดียวในทั้งโปรแกรม

---

## 🎯 Singleton Pattern คืออะไร?

**Singleton** = Design Pattern ที่ทำให้คลาสมี instance (ตัวอย่าง) เดียวเท่านั้น

### ทำไมต้องใช้?
✅ ประหยัด Memory (ไม่ต้องสร้างหลายตัว)  
✅ ควบคุมการเชื่อมต่อฐานข้อมูลได้ดีกว่า  
✅ ใช้งานง่าย เรียกผ่าน `DatabaseHelper.Instance`  

### การใช้งาน:
```csharp
// ❌ ไม่สามารถทำแบบนี้ได้ (Constructor เป็น private)
// DatabaseHelper db = new DatabaseHelper();

// ✅ ต้องใช้แบบนี้
DatabaseHelper db = DatabaseHelper.Instance;
```

---

## 📚 โครงสร้างโค้ด

### 1. Singleton Pattern Implementation

```csharp
private static DatabaseHelper _instance;
private static readonly object _lock = new object();

public static DatabaseHelper Instance
{
    get
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseHelper();
                }
            }
        }
        return _instance;
    }
}
```

**อธิบาย:**
- `_instance` = เก็บ instance เดียวของคลาส
- `_lock` = ใช้ล็อคเพื่อป้องกันการสร้างพร้อมกัน (Thread-safe)
- `lock (_lock)` = ล็อคเพื่อให้มีการสร้างทีละตัว

---

### 2. Connection String

```csharp
private string ConnectionString { get; set; }

private DatabaseHelper()
{
    ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDB"].ConnectionString;
}
```

**อธิบาย:**
- อ่าน Connection String จากไฟล์ `App.config`
- `ConfigurationManager` = คลาสสำหรับอ่านค่าจาก config file
- `["HospitalDB"]` = ชื่อ connection string ที่เราตั้งไว้

---

### 3. GetConnection() - สร้าง Connection

```csharp
public SqlConnection GetConnection()
{
    return new SqlConnection(ConnectionString);
}
```

**อธิบาย:**
- สร้าง `SqlConnection` ใหม่
- ใช้ `ConnectionString` ที่อ่านจาก config

**การใช้งาน:**
```csharp
using (SqlConnection conn = DatabaseHelper.Instance.GetConnection())
{
    conn.Open();
    // ทำงานกับฐานข้อมูล
}
```

---

### 4. ExecuteNonQuery() - INSERT, UPDATE, DELETE

```csharp
public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
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

**อธิบาย:**
- ใช้สำหรับ Query ที่ไม่ต้องการผลลัพธ์ (INSERT, UPDATE, DELETE)
- คืนค่าจำนวนแถวที่ได้รับผลกระทบ
- `using` = ปิด connection อัตโนมัติเมื่อเสร็จ

**การใช้งาน:**
```csharp
// ตัวอย่าง: เพิ่มผู้ป่วยใหม่
string query = "INSERT INTO Patients (PatientCode, FirstName, LastName) VALUES (@Code, @FirstName, @LastName)";

SqlParameter[] parameters = new SqlParameter[]
{
    new SqlParameter("@Code", "P2024006"),
    new SqlParameter("@FirstName", "สมชาย"),
    new SqlParameter("@LastName", "ใจดี")
};

int rowsAffected = DatabaseHelper.Instance.ExecuteNonQuery(query, parameters);
// rowsAffected = 1 (เพิ่มได้ 1 แถว)
```

---

### 5. ExecuteScalar() - ดึงค่าเดียว

```csharp
public object ExecuteScalar(string query, SqlParameter[] parameters = null)
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

**อธิบาย:**
- ใช้สำหรับ Query ที่ต้องการค่าเดียว (COUNT, MAX, SUM, etc.)
- คืนค่าเป็น `object` (ต้อง cast เป็นชนิดที่ต้องการ)

**การใช้งาน:**
```csharp
// ตัวอย่าง: นับจำนวนผู้ป่วย
string query = "SELECT COUNT(*) FROM Patients";
int patientCount = Convert.ToInt32(DatabaseHelper.Instance.ExecuteScalar(query));

// ตัวอย่าง: ตรวจสอบ Login
string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND PasswordHash = @Password";
SqlParameter[] parameters = new SqlParameter[]
{
    new SqlParameter("@Username", "admin"),
    new SqlParameter("@Password", "admin123")
};

int count = Convert.ToInt32(DatabaseHelper.Instance.ExecuteScalar(query, parameters));
if (count > 0)
{
    // Login สำเร็จ
}
```

---

### 6. ExecuteReader() - อ่านหลายแถว

```csharp
public SqlDataReader ExecuteReader(string query, SqlParameter[] parameters = null)
{
    SqlConnection conn = GetConnection();
    SqlCommand cmd = new SqlCommand(query, conn);
    
    if (parameters != null)
        cmd.Parameters.AddRange(parameters);
    
    conn.Open();
    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
}
```

**อธิบาย:**
- ใช้สำหรับ Query ที่ต้องการอ่านข้อมูลหลายแถว
- คืนค่าเป็น `SqlDataReader`
- `CommandBehavior.CloseConnection` = ปิด connection อัตโนมัติเมื่อปิด reader

**การใช้งาน:**
```csharp
// ตัวอย่าง: ดึงรายชื่อผู้ป่วยทั้งหมด
string query = "SELECT PatientID, FirstName, LastName FROM Patients";

using (SqlDataReader reader = DatabaseHelper.Instance.ExecuteReader(query))
{
    while (reader.Read())
    {
        int id = reader.GetInt32(0);
        string firstName = reader.GetString(1);
        string lastName = reader.GetString(2);
        
        Console.WriteLine($"{id}: {firstName} {lastName}");
    }
}
```

---

### 7. ExecuteDataTable() - คืนค่าเป็น DataTable

```csharp
public DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
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

**อธิบาย:**
- ใช้สำหรับ Query ที่ต้องการข้อมูลเป็น `DataTable`
- เหมาะสำหรับแสดงใน `DataGridView`
- `SqlDataAdapter` = ดึงข้อมูลและเติมลงใน DataTable

**การใช้งาน:**
```csharp
// ตัวอย่าง: แสดงรายชื่อผู้ป่วยใน DataGridView
string query = "SELECT PatientCode, FirstName, LastName, Phone FROM Patients WHERE IsActive = 1";

DataTable dt = DatabaseHelper.Instance.ExecuteDataTable(query);
dataGridView1.DataSource = dt;
```

---

## 🛠️ Helper Methods

### CreateParameter() - สร้าง Parameter

```csharp
public SqlParameter CreateParameter(string parameterName, object value)
{
    return new SqlParameter(parameterName, value ?? DBNull.Value);
}
```

**การใช้งาน:**
```csharp
SqlParameter param = DatabaseHelper.Instance.CreateParameter("@Username", "admin");
```

---

## 💡 ตัวอย่างการใช้งานจริง

### ตัวอย่างที่ 1: ตรวจสอบ Login

```csharp
public bool ValidateLogin(string username, string password)
{
    string query = @"
        SELECT COUNT(*) 
        FROM Users 
        WHERE Username = @Username 
        AND PasswordHash = @Password 
        AND IsActive = 1";
    
    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@Username", username),
        new SqlParameter("@Password", password)
    };
    
    int count = Convert.ToInt32(DatabaseHelper.Instance.ExecuteScalar(query, parameters));
    return count > 0;
}
```

### ตัวอย่างที่ 2: ดึงข้อมูลผู้ใช้

```csharp
public DataTable GetUserInfo(string username)
{
    string query = @"
        SELECT u.UserID, u.Username, u.FullName, u.Email, r.RoleName
        FROM Users u
        INNER JOIN Roles r ON u.RoleID = r.RoleID
        WHERE u.Username = @Username";
    
    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@Username", username)
    };
    
    return DatabaseHelper.Instance.ExecuteDataTable(query, parameters);
}
```

### ตัวอย่างที่ 3: เพิ่มผู้ป่วยใหม่

```csharp
public bool AddPatient(string code, string firstName, string lastName, DateTime dob, string gender)
{
    string query = @"
        INSERT INTO Patients (PatientCode, FirstName, LastName, DateOfBirth, Gender, CreatedBy)
        VALUES (@Code, @FirstName, @LastName, @DOB, @Gender, @CreatedBy)";
    
    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@Code", code),
        new SqlParameter("@FirstName", firstName),
        new SqlParameter("@LastName", lastName),
        new SqlParameter("@DOB", dob),
        new SqlParameter("@Gender", gender),
        new SqlParameter("@CreatedBy", 1) // UserID ของผู้สร้าง
    };
    
    int rowsAffected = DatabaseHelper.Instance.ExecuteNonQuery(query, parameters);
    return rowsAffected > 0;
}
```

---

## 🔒 SQL Injection Prevention

### ❌ ไม่ถูกต้อง (เสี่ยง SQL Injection)
```csharp
string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
```

### ✅ ถูกต้อง (ใช้ Parameters)
```csharp
string query = "SELECT * FROM Users WHERE Username = @Username";
SqlParameter[] parameters = new SqlParameter[]
{
    new SqlParameter("@Username", username)
};
```

**ทำไม?**
- ถ้า username = `admin' OR '1'='1` จะทำให้ Query เป็น:
  ```sql
  SELECT * FROM Users WHERE Username = 'admin' OR '1'='1'
  ```
  ผลลัพธ์: ดึงข้อมูลทุกคนออกมา! (อันตราย!)

- แต่ถ้าใช้ Parameters จะถือว่า `admin' OR '1'='1` เป็นข้อความทั้งหมด (ปลอดภัย)

---

## 🎯 สรุป

### DatabaseHelper มี Methods หลัก:

| Method | ใช้สำหรับ | คืนค่า |
|--------|-----------|--------|
| `ExecuteNonQuery()` | INSERT, UPDATE, DELETE | จำนวนแถว |
| `ExecuteScalar()` | COUNT, MAX, SUM | ค่าเดียว |
| `ExecuteReader()` | SELECT (อ่านทีละแถว) | SqlDataReader |
| `ExecuteDataTable()` | SELECT (ทั้งหมด) | DataTable |
| `ExecuteStoredProcedure()` | เรียก SP | จำนวนแถว |

### ข้อดี:
✅ ใช้งานง่าย  
✅ ป้องกัน SQL Injection  
✅ จัดการ Connection อัตโนมัติ  
✅ มี instance เดียว (Singleton)  
✅ มี Error Handling  

---

## 🚀 ขั้นตอนต่อไป

1. สร้าง **User.cs** (Model)
2. สร้าง **UserRepository.cs** (จัดการข้อมูล User)
3. สร้าง **LoginForm.cs** (หน้าจอ Login)

**พร้อมไปต่อแล้วใช่ไหมครับ?** 💪
