# 📊 บทที่ 8: Dashboard Form - หน้าหลักของระบบ

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่มทุกปุ่ม**ใน Designer เพื่อสร้าง Event Handler! ไม่งั้นปุ่มจะไม่ทำงาน! [อ่านเพิ่มเติม](00_IMPORTANT_TIPS.md)

## 📋 เป้าหมายของบทนี้

ในบทนี้คุณจะได้เรียนรู้:
- ✅ ออกแบบ Dashboard UI
- ✅ แสดงสถิติแบบ Real-time
- ✅ สร้างเมนูหลักสำหรับ Navigation
- ✅ แสดงนัดหมายวันนี้
- ✅ เชื่อมต่อกับ Login Form

---

## 🎨 การออกแบบ Dashboard

### หน้าตาที่จะได้:

```
╔═══════════════════════════════════════════════════════════╗
║  🏥 Hospital Management System                            ║
║  Welcome, System Administrator | Logout                   ║
╠═══════════════════════════════════════════════════════════╣
║                                                            ║
║  📊 System Statistics                                     ║
║  ┌────────────┐ ┌────────────┐ ┌────────────┐ ┌────────┐║
║  │     5      │ │     5      │ │     5      │ │   16   │║
║  │  Patients  │ │  Doctors   │ │Appointments│ │Medicines│║
║  └────────────┘ └────────────┘ └────────────┘ └────────┘║
║                                                            ║
║  🔘 Main Menu                                             ║
║  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐   ║
║  │ Patients │ │ Doctors  │ │Appointments│ │ Medicines│   ║
║  └──────────┘ └──────────┘ └──────────┘ └──────────┘   ║
║  ┌──────────┐ ┌──────────┐ ┌──────────┐                 ║
║  │ Billing  │ │ Reports  │ │ Settings │                 ║
║  └──────────┘ └──────────┘ └──────────┘                 ║
║                                                            ║
║  📅 Today's Appointments (2024-12-17)                     ║
║  ┌────────────────────────────────────────────────────┐  ║
║  │ Time  │ Patient        │ Doctor         │ Status   │  ║
║  ├────────────────────────────────────────────────────┤  ║
║  │ 09:00 │ Vichai Mangmee │ Dr. Somporn   │ Confirmed│  ║
║  │ 10:30 │ Suda Ramruay   │ Dr. Somchai   │ Confirmed│  ║
║  │ 14:00 │ Prasert Deengam│ Dr. Somying   │ Pending  │  ║
║  └────────────────────────────────────────────────────┘  ║
╚═══════════════════════════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนที่ 1: สร้าง Dashboard Form

### 1. สร้าง Form ใหม่

1. ใน Visual Studio คลิกขวาที่โฟลเดอร์ **Forms**
2. เลือก **Add** → **New Folder** → ตั้งชื่อ `Dashboard`
3. คลิกขวาที่โฟลเดอร์ **Dashboard**
4. เลือก **Add** → **Windows Form**
5. ตั้งชื่อ `DashboardForm.cs`
6. คลิก **Add**

---

### 2. ตั้งค่า Form Properties

คลิกที่ Form แล้วตั้งค่า Properties:

| Property | Value |
|----------|-------|
| **Name** | `DashboardForm` |
| **Text** | `Hospital Management System - Dashboard` |
| **Size** | `1200, 700` |
| **StartPosition** | `CenterScreen` |
| **WindowState** | `Maximized` |
| **FormBorderStyle** | `Sizable` |

---

## 📊 ขั้นตอนที่ 2: สร้าง Header Panel

### เพิ่ม Panel สำหรับ Header:

1. ลาก **Panel** จาก Toolbox
2. ตั้งค่า Properties:

| Property | Value |
|----------|-------|
| **Name** | `panelHeader` |
| **Dock** | `Top` |
| **Height** | `60` |
| **BackColor** | `DodgerBlue` |

### เพิ่ม Labels ใน Header:

**Label 1: ชื่อระบบ**
- **Name:** `lblTitle`
- **Text:** `🏥 Hospital Management System`
- **Font:** Segoe UI, 16pt, Bold
- **ForeColor:** White
- **Location:** `20, 15`

**Label 2: ชื่อผู้ใช้**
- **Name:** `lblWelcome`
- **Text:** `Welcome, System Administrator`
- **Font:** Segoe UI, 10pt
- **ForeColor:** White
- **Location:** `900, 20`

**Button: Logout**
- **Name:** `btnLogout`
- **Text:** `Logout`
- **Size:** `80, 30`
- **Location:** `1100, 15`
- **BackColor:** `Red`
- **ForeColor:** White

---

## 📈 ขั้นตอนที่ 3: สร้าง Statistics Panel

### เพิ่ม Panel สำหรับสถิติ:

1. ลาก **Panel** จาก Toolbox
2. ตั้งค่า:

| Property | Value |
|----------|-------|
| **Name** | `panelStats` |
| **Location** | `20, 80` |
| **Size** | `1160, 120` |
| **BorderStyle** | `FixedSingle` |

### เพิ่ม Label หัวข้อ:

- **Name:** `lblStatsTitle`
- **Text:** `📊 System Statistics`
- **Font:** Segoe UI, 12pt, Bold
- **Location:** `10, 10`

### สร้าง Stat Cards (4 ตัว):

**Card 1: Patients**
```
Panel: panelPatientsStat
- Size: 250, 80
- Location: 20, 35
- BackColor: LightBlue
- BorderStyle: FixedSingle

Label (จำนวน):
- Name: lblPatientsCount
- Text: 0
- Font: 24pt, Bold
- Location: 100, 15

Label (ชื่อ):
- Name: lblPatientsLabel
- Text: Patients
- Font: 12pt
- Location: 90, 50
```

**Card 2: Doctors** (คล้ายกัน)
- Location: 290, 35
- BackColor: LightGreen

**Card 3: Appointments** (คล้ายกัน)
- Location: 560, 35
- BackColor: LightCoral

**Card 4: Medicines** (คล้ายกัน)
- Location: 830, 35
- BackColor: LightGoldenrodYellow

---

## 🔘 ขั้นตอนที่ 4: สร้าง Menu Panel

### เพิ่ม Panel สำหรับเมนู:

| Property | Value |
|----------|-------|
| **Name** | `panelMenu` |
| **Location** | `20, 220` |
| **Size** | `1160, 150` |
| **BorderStyle** | `FixedSingle` |

### เพิ่ม Buttons (6 ปุ่ม):

**ปุ่มที่ 1: Patients**
```csharp
Name: btnPatients
Text: 👥 Patients
Size: 150, 60
Location: 20, 40
Font: 12pt
BackColor: DodgerBlue
ForeColor: White
```

**ปุ่มที่ 2-6:** (คล้ายกัน แต่เปลี่ยน Text และ Location)
- Doctors (190, 40)
- Appointments (360, 40)
- Medicines (530, 40)
- Billing (700, 40)
- Reports (870, 40)

---

## 📅 ขั้นตอนที่ 5: สร้าง Appointments Panel

### เพิ่ม DataGridView:

| Property | Value |
|----------|-------|
| **Name** | `dgvTodayAppointments` |
| **Location** | `20, 420` |
| **Size** | `1160, 220` |
| **ReadOnly** | `True` |
| **AllowUserToAddRows** | `False` |
| **SelectionMode** | `FullRowSelect` |

### ตั้งค่า Columns:

```csharp
// เพิ่มใน Form_Load
dgvTodayAppointments.Columns.Add("Time", "Time");
dgvTodayAppointments.Columns.Add("PatientName", "Patient");
dgvTodayAppointments.Columns.Add("DoctorName", "Doctor");
dgvTodayAppointments.Columns.Add("Status", "Status");
dgvTodayAppointments.Columns.Add("Reason", "Reason");
```

---

## 💻 ขั้นตอนที่ 6: เขียนโค้ด

### DashboardForm.cs:

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace HospitalMS.UI.Forms.Dashboard
{
    public partial class DashboardForm : Form
    {
        private string _username;
        private string _fullName;
        private string _roleName;

        public DashboardForm(string username, string fullName, string roleName)
        {
            InitializeComponent();
            _username = username;
            _fullName = fullName;
            _roleName = roleName;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // แสดงชื่อผู้ใช้
            lblWelcome.Text = $"Welcome, {_fullName} ({_roleName})";

            // โหลดสถิติ
            LoadStatistics();

            // โหลดนัดหมายวันนี้
            LoadTodayAppointments();
        }

        private void LoadStatistics()
        {
            try
            {
                // นับจำนวนผู้ป่วย
                string queryPatients = "SELECT COUNT(*) FROM Patients WHERE IsActive = 1";
                int patientsCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryPatients));
                lblPatientsCount.Text = patientsCount.ToString();

                // นับจำนวนหมอ
                string queryDoctors = "SELECT COUNT(*) FROM Doctors WHERE IsActive = 1";
                int doctorsCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryDoctors));
                lblDoctorsCount.Text = doctorsCount.ToString();

                // นับจำนวนนัดหมาย
                string queryAppointments = "SELECT COUNT(*) FROM Appointments";
                int appointmentsCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryAppointments));
                lblAppointmentsCount.Text = appointmentsCount.ToString();

                // นับจำนวนยา
                string queryMedicines = "SELECT COUNT(*) FROM Medicines WHERE IsActive = 1";
                int medicinesCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryMedicines));
                lblMedicinesCount.Text = medicinesCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading statistics: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTodayAppointments()
        {
            try
            {
                string query = @"
                    SELECT 
                        CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS Time,
                        p.FirstName + ' ' + p.LastName AS PatientName,
                        d.FirstName + ' ' + d.LastName AS DoctorName,
                        a.Status,
                        a.Reason
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientID = p.PatientID
                    INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                    WHERE CAST(a.AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
                    ORDER BY a.AppointmentTime";

                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                dgvTodayAppointments.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvTodayAppointments.Rows.Add(
                        row["Time"],
                        row["PatientName"],
                        row["DoctorName"],
                        row["Status"],
                        row["Reason"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                // แสดง Login Form อีกครั้ง
                Application.Restart();
            }
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Patient Management - Coming Soon!", "Info");
            // TODO: เปิด Patient Management Form
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Doctor Management - Coming Soon!", "Info");
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Appointment Management - Coming Soon!", "Info");
        }

        private void btnMedicines_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Medicine Management - Coming Soon!", "Info");
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Billing - Coming Soon!", "Info");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports - Coming Soon!", "Info");
        }
    }
}
```

---

## 🔗 ขั้นตอนที่ 7: เชื่อมต่อกับ Login Form

### แก้ไข LoginForm.cs:

```csharp
// ใน btnLogin_Click เมื่อ Login สำเร็จ
if (dt.Rows.Count > 0)
{
    DataRow user = dt.Rows[0];
    string username = user["Username"].ToString();
    string fullName = user["FullName"].ToString();
    string roleName = user["RoleName"].ToString();

    // ซ่อน Login Form
    this.Hide();

    // เปิด Dashboard
    DashboardForm dashboard = new DashboardForm(username, fullName, roleName);
    dashboard.FormClosed += (s, args) => this.Close();
    dashboard.Show();
}
```

---

## 🧪 ขั้นตอนที่ 8: ทดสอบ

### 1. Build โปรเจค
```
Ctrl + Shift + B
```

### 2. รันโปรแกรม
```
F5
```

### 3. ทดสอบ
1. Login ด้วย `admin` / `admin123`
2. ควรเห็น Dashboard
3. ตรวจสอบ:
   - ✅ สถิติแสดงถูกต้อง
   - ✅ นัดหมายวันนี้แสดงผล
   - ✅ ปุ่มเมนูทำงาน
   - ✅ Logout ได้

---

## 🎨 ปรับแต่งเพิ่มเติม (Optional)

### 1. เพิ่มไอคอน
```csharp
btnPatients.Text = "👥\nPatients";
btnDoctors.Text = "👨‍⚕️\nDoctors";
btnAppointments.Text = "📅\nAppointments";
```

### 2. เพิ่ม Hover Effect
```csharp
private void btnPatients_MouseEnter(object sender, EventArgs e)
{
    btnPatients.BackColor = Color.Blue;
}

private void btnPatients_MouseLeave(object sender, EventArgs e)
{
    btnPatients.BackColor = Color.DodgerBlue;
}
```

### 3. เพิ่ม Refresh Button
```csharp
private void btnRefresh_Click(object sender, EventArgs e)
{
    LoadStatistics();
    LoadTodayAppointments();
    MessageBox.Show("Data refreshed!", "Success");
}
```

---

## 📝 สรุป

ในบทนี้เราได้:
- ✅ สร้าง Dashboard Form
- ✅ แสดงสถิติแบบ Real-time
- ✅ สร้างเมนูหลัก
- ✅ แสดงนัดหมายวันนี้
- ✅ เชื่อมต่อกับ Login Form

**ขั้นตอนต่อไป:**
- สร้าง Patient Management Form
- เพิ่มฟังก์ชัน CRUD
- เชื่อมกับ Dashboard

---

**พร้อมไปต่อหรือยังครับ?** 🚀
