# 🏥 บทที่ 14: Medical Records Module - ประวัติการรักษา

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่มทุกปุ่ม**ใน Designer เพื่อสร้าง Event Handler!

## 📋 เป้าหมาย

สร้างระบบจัดการประวัติการรักษา:
- ✅ บันทึกประวัติการรักษา (Medical Records)
- ✅ จัดการโรคประจำตัว (Chronic Diseases)
- ✅ บันทึกประวัติการแพ้ (Allergies)
- ✅ ดูประวัติการรักษาทั้งหมดของผู้ป่วย
- ✅ เชื่อมกับ Patient Management

---

## 🗄️ โครงสร้างฐานข้อมูล

### 1. MedicalRecords (ประวัติการรักษา)
```sql
- RecordID (PK)
- PatientID (FK)
- AppointmentID (FK)
- VisitDate
- ChiefComplaint (อาการสำคัญ)
- Diagnosis (การวินิจฉัย)
- Treatment (การรักษา)
- Prescription (ใบสั่งยา)
- Notes
- DoctorID (FK)
```

### 2. ChronicDiseases (โรคประจำตัว)
```sql
- ChronicDiseaseID (PK)
- PatientID (FK)
- DiseaseName
- DiagnosedDate
- Severity (Mild/Moderate/Severe)
- Status (Active/Controlled/Remission)
- Notes
```

### 3. Allergies (ประวัติการแพ้)
```sql
- AllergyID (PK)
- PatientID (FK)
- AllergyType (Drug/Food/Environmental)
- AllergyName
- Reaction
- Severity
```

---

## 🎨 หน้าตาที่จะได้

### Patient Medical History Form:
```
╔═══════════════════════════════════════════════════════════╗
║  📋 Medical History - Vichai Mangmee (P2024001)          ║
╠═══════════════════════════════════════════════════════════╣
║  [Medical Records] [Chronic Diseases] [Allergies]        ║
╠═══════════════════════════════════════════════════════════╣
║  📝 Medical Records:                                      ║
║  ┌────────────────────────────────────────────────────┐  ║
║  │ Date       │ Diagnosis      │ Doctor      │ Action │  ║
║  ├────────────────────────────────────────────────────┤  ║
║  │ 2024-12-18 │ Flu           │ Dr. Somchai │ [View] │  ║
║  │ 2024-12-10 │ Checkup       │ Dr. Suda    │ [View] │  ║
║  └────────────────────────────────────────────────────┘  ║
║  [+ Add Record] [Edit] [Delete]                          ║
║                                                           ║
║  🏥 Chronic Diseases:                                     ║
║  • Diabetes Type 2 (Controlled) - Since 2020-05-15      ║
║  • Hypertension (Active) - Since 2018-03-20             ║
║  [+ Add Disease] [Edit] [Delete]                         ║
║                                                           ║
║  ⚠️ Allergies:                                            ║
║  • Penicillin (Drug) - Severe: Skin rash               ║
║  • Peanuts (Food) - Severe: Anaphylaxis                ║
║  [+ Add Allergy] [Edit] [Delete]                         ║
╚═══════════════════════════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนการสร้าง

### 1. รัน SQL Script

```sql
-- รันไฟล์นี้
Database\12_Create_MedicalRecords_Tables.sql
```

---

### 2. สร้าง Model Classes

**MedicalRecord.cs:**
```csharp
namespace HospitalMS.DAL.Models
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int? AppointmentID { get; set; }
        public DateTime VisitDate { get; set; }
        public string ChiefComplaint { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
        public int DoctorID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
```

**ChronicDisease.cs:**
```csharp
namespace HospitalMS.DAL.Models
{
    public class ChronicDisease
    {
        public int ChronicDiseaseID { get; set; }
        public int PatientID { get; set; }
        public string DiseaseName { get; set; }
        public DateTime? DiagnosedDate { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
```

**Allergy.cs:**
```csharp
namespace HospitalMS.DAL.Models
{
    public class Allergy
    {
        public int AllergyID { get; set; }
        public int PatientID { get; set; }
        public string AllergyType { get; set; }
        public string AllergyName { get; set; }
        public string Reaction { get; set; }
        public string Severity { get; set; }
        public bool IsActive { get; set; }
    }
}
```

---

### 3. สร้าง Medical History Form

**Form Properties:**
- Name: `PatientMedicalHistoryForm`
- Size: `1000, 700`
- Text: `Medical History`

**Tab Control:**
- Name: `tabControl`
- Dock: `Fill`
- Tabs: 
  1. Medical Records
  2. Chronic Diseases
  3. Allergies

---

### 4. Tab 1: Medical Records

**DataGridView:**
- Name: `dgvMedicalRecords`
- Columns: VisitDate, Diagnosis, Doctor, ChiefComplaint

**Buttons:**
- `btnAddRecord` - Add New Record
- `btnEditRecord` - Edit
- `btnDeleteRecord` - Delete
- `btnViewRecord` - View Details

---

### 5. Tab 2: Chronic Diseases

**DataGridView:**
- Name: `dgvChronicDiseases`
- Columns: DiseaseName, DiagnosedDate, Severity, Status

**Buttons:**
- `btnAddDisease` - Add Disease
- `btnEditDisease` - Edit
- `btnDeleteDisease` - Delete

---

### 6. Tab 3: Allergies

**DataGridView:**
- Name: `dgvAllergies`
- Columns: AllergyType, AllergyName, Reaction, Severity

**Buttons:**
- `btnAddAllergy` - Add Allergy
- `btnEditAllergy` - Edit
- `btnDeleteAllergy` - Delete

---

## 💻 โค้ดตัวอย่าง

### PatientMedicalHistoryForm.cs

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.MedicalRecords
{
    public partial class PatientMedicalHistoryForm : Form
    {
        private int _patientId;
        private string _patientName;

        public PatientMedicalHistoryForm(int patientId, string patientName)
        {
            InitializeComponent();
            _patientId = patientId;
            _patientName = patientName;
            this.Text = $"Medical History - {patientName}";
        }

        private void PatientMedicalHistoryForm_Load(object sender, EventArgs e)
        {
            LoadMedicalRecords();
            LoadChronicDiseases();
            LoadAllergies();
        }

        private void LoadMedicalRecords()
        {
            try
            {
                string query = @"
                    SELECT 
                        mr.RecordID,
                        CONVERT(VARCHAR(10), mr.VisitDate, 103) AS VisitDate,
                        mr.ChiefComplaint,
                        mr.Diagnosis,
                        d.FirstName + ' ' + d.LastName AS DoctorName
                    FROM MedicalRecords mr
                    INNER JOIN Doctors d ON mr.DoctorID = d.DoctorID
                    WHERE mr.PatientID = @PatientID AND mr.IsActive = 1
                    ORDER BY mr.VisitDate DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", _patientId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvMedicalRecords.DataSource = dt;

                if (dgvMedicalRecords.Columns.Count > 0)
                {
                    dgvMedicalRecords.Columns["RecordID"].Visible = false;
                    dgvMedicalRecords.Columns["VisitDate"].HeaderText = "Visit Date";
                    dgvMedicalRecords.Columns["ChiefComplaint"].HeaderText = "Chief Complaint";
                    dgvMedicalRecords.Columns["Diagnosis"].HeaderText = "Diagnosis";
                    dgvMedicalRecords.Columns["DoctorName"].HeaderText = "Doctor";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading medical records: " + ex.Message);
            }
        }

        private void LoadChronicDiseases()
        {
            try
            {
                string query = @"
                    SELECT 
                        ChronicDiseaseID,
                        DiseaseName,
                        CONVERT(VARCHAR(10), DiagnosedDate, 103) AS DiagnosedDate,
                        Severity,
                        Status,
                        Notes
                    FROM ChronicDiseases
                    WHERE PatientID = @PatientID AND IsActive = 1
                    ORDER BY DiagnosedDate DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", _patientId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvChronicDiseases.DataSource = dt;

                if (dgvChronicDiseases.Columns.Count > 0)
                {
                    dgvChronicDiseases.Columns["ChronicDiseaseID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chronic diseases: " + ex.Message);
            }
        }

        private void LoadAllergies()
        {
            try
            {
                string query = @"
                    SELECT 
                        AllergyID,
                        AllergyType,
                        AllergyName,
                        Reaction,
                        Severity
                    FROM Allergies
                    WHERE PatientID = @PatientID AND IsActive = 1
                    ORDER BY Severity DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", _patientId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvAllergies.DataSource = dt;

                if (dgvAllergies.Columns.Count > 0)
                {
                    dgvAllergies.Columns["AllergyID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading allergies: " + ex.Message);
            }
        }
    }
}
```

---

## 🔗 เชื่อมกับ Patient Management

### แก้ไข PatientListForm.cs:

```csharp
// เพิ่มปุ่ม Medical History
private void btnMedicalHistory_Click(object sender, EventArgs e)
{
    if (dgvPatients.SelectedRows.Count == 0)
    {
        MessageBox.Show("Please select a patient.", "Warning");
        return;
    }

    int patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["PatientID"].Value);
    string patientName = dgvPatients.SelectedRows[0].Cells["FullName"].Value.ToString();

    PatientMedicalHistoryForm form = new PatientMedicalHistoryForm(patientId, patientName);
    form.ShowDialog();
}
```

---

## 📝 สรุป

**Medical Records Module ประกอบด้วย:**

✅ **3 ตารางหลัก:**
- MedicalRecords (ประวัติการรักษา)
- ChronicDiseases (โรคประจำตัว)
- Allergies (ประวัติการแพ้)

✅ **ฟีเจอร์:**
- บันทึกประวัติการรักษาแต่ละครั้ง
- จัดการโรคประจำตัว
- บันทึกประวัติการแพ้ยา/อาหาร
- ดูประวัติทั้งหมดของผู้ป่วย

✅ **เชื่อมกับ:**
- Patient Management
- Doctor Management
- Appointment Management

---

**พร้อมใช้งานแล้วครับ!** 🎉
