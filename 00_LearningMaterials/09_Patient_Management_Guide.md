# 👥 บทที่ 9: Patient Management - จัดการข้อมูลผู้ป่วย

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่มทุกปุ่ม**ใน Designer เพื่อสร้าง Event Handler! ไม่งั้นปุ่มจะไม่ทำงาน! [อ่านเพิ่มเติม](00_IMPORTANT_TIPS.md)

## 📋 เป้าหมายของบทนี้

ในบทนี้คุณจะได้เรียนรู้:
- ✅ สร้างหน้าจัดการผู้ป่วย (Patient List)
- ✅ แสดงรายชื่อผู้ป่วยใน DataGridView
- ✅ เพิ่มผู้ป่วยใหม่
- ✅ แก้ไขข้อมูลผู้ป่วย
- ✅ ลบผู้ป่วย (Soft Delete)
- ✅ ค้นหาผู้ป่วย

---

## 🎨 การออกแบบ Patient Management

### หน้าตาที่จะได้:

```
╔═══════════════════════════════════════════════════════════╗
║  👥 Patient Management                                    ║
╠═══════════════════════════════════════════════════════════╣
║  Search: [____________] 🔍                                ║
║  [+ Add New] [✏️ Edit] [🗑️ Delete] [🔄 Refresh]          ║
╠═══════════════════════════════════════════════════════════╣
║  ID │ Code    │ Name           │ Gender │ Phone         │║
║  ───┼─────────┼────────────────┼────────┼───────────────┤║
║  1  │ P2024001│ Vichai Mangmee │ Male   │ 086-123-4567 │║
║  2  │ P2024002│ Suda Ramruay   │ Female │ 087-234-5678 │║
║  3  │ P2024003│ Prasert Deengam│ Male   │ 088-345-6789 │║
║  4  │ P2024004│ Somjai Rakdee  │ Female │ 089-456-7890 │║
║  5  │ P2024005│ Wichai Suksai  │ Male   │ 086-567-8901 │║
╚═══════════════════════════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนที่ 1: สร้าง Patient Management Form

### 1. สร้าง Form ใหม่

1. คลิกขวาที่โฟลเดอร์ **Forms**
2. **Add** → **New Folder** → ตั้งชื่อ `Patients`
3. คลิกขวาที่โฟลเดอร์ **Patients**
4. **Add** → **Windows Form**
5. ตั้งชื่อ `PatientListForm.cs`
6. คลิก **Add**

---

### 2. ตั้งค่า Form Properties

| Property | Value |
|----------|-------|
| **Name** | `PatientListForm` |
| **Text** | `Patient Management` |
| **Size** | `1000, 600` |
| **StartPosition** | `CenterScreen` |
| **FormBorderStyle** | `FixedDialog` |
| **MaximizeBox** | `False` |

---

## 🔍 ขั้นตอนที่ 2: สร้าง Search Panel

### เพิ่ม Panel สำหรับค้นหา:

| Property | Value |
|----------|-------|
| **Name** | `panelSearch` |
| **Dock** | `Top` |
| **Height** | `60` |
| **BackColor** | `WhiteSmoke` |

### เพิ่ม Controls ใน panelSearch:

**Label:**
- Name: `lblSearch`
- Text: `Search:`
- Location: `20, 20`

**TextBox:**
- Name: `txtSearch`
- Size: `300, 25`
- Location: `80, 18`

**Button:**
- Name: `btnSearch`
- Text: `🔍 Search`
- Size: `100, 30`
- Location: `400, 15`
- BackColor: `DodgerBlue`
- ForeColor: `White`

---

## 🔘 ขั้นตอนที่ 3: สร้าง Action Buttons Panel

### เพิ่ม Panel สำหรับปุ่ม:

| Property | Value |
|----------|-------|
| **Name** | `panelActions` |
| **Dock** | `Top` |
| **Height** | `60` |

### เพิ่มปุ่ม 4 ปุ่ม:

**Button 1: Add**
- Name: `btnAdd`
- Text: `+ Add New Patient`
- Size: `150, 35`
- Location: `20, 12`
- BackColor: `Green`
- ForeColor: `White`

**Button 2: Edit**
- Name: `btnEdit`
- Text: `✏️ Edit`
- Size: `100, 35`
- Location: `190, 12`
- BackColor: `Orange`
- ForeColor: `White`

**Button 3: Delete**
- Name: `btnDelete`
- Text: `🗑️ Delete`
- Size: `100, 35`
- Location: `310, 12`
- BackColor: `Red`
- ForeColor: `White`

**Button 4: Refresh**
- Name: `btnRefresh`
- Text: `🔄 Refresh`
- Size: `100, 35`
- Location: `430, 12`
- BackColor: `Gray`
- ForeColor: `White`

---

## 📊 ขั้นตอนที่ 4: สร้าง DataGridView

### เพิ่ม DataGridView:

| Property | Value |
|----------|-------|
| **Name** | `dgvPatients` |
| **Dock** | `Fill` |
| **ReadOnly** | `True` |
| **AllowUserToAddRows** | `False` |
| **SelectionMode** | `FullRowSelect` |
| **MultiSelect** | `False` |
| **AutoSizeColumnsMode** | `Fill` |

---

## 💻 ขั้นตอนที่ 5: เขียนโค้ด PatientListForm.cs

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace HospitalMS.UI.Forms.Patients
{
    public partial class PatientListForm : Form
    {
        public PatientListForm()
        {
            InitializeComponent();
        }

        private void PatientListForm_Load(object sender, EventArgs e)
        {
            LoadPatients();
        }

        private void LoadPatients(string searchTerm = "")
        {
            try
            {
                string query = @"
                    SELECT 
                        PatientID,
                        PatientCode,
                        FirstName + ' ' + LastName AS FullName,
                        Gender,
                        BloodGroup,
                        Phone,
                        Email,
                        CONVERT(VARCHAR(10), DateOfBirth, 103) AS DateOfBirth
                    FROM Patients
                    WHERE IsActive = 1";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (FirstName LIKE @Search 
                                OR LastName LIKE @Search 
                                OR PatientCode LIKE @Search 
                                OR Phone LIKE @Search)";
                }

                query += " ORDER BY PatientCode DESC";

                SqlParameter[] parameters = null;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@Search", $"%{searchTerm}%")
                    };
                }

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvPatients.DataSource = dt;

                // ตั้งค่า Column Headers
                if (dgvPatients.Columns.Count > 0)
                {
                    dgvPatients.Columns["PatientID"].HeaderText = "ID";
                    dgvPatients.Columns["PatientCode"].HeaderText = "Code";
                    dgvPatients.Columns["FullName"].HeaderText = "Full Name";
                    dgvPatients.Columns["Gender"].HeaderText = "Gender";
                    dgvPatients.Columns["BloodGroup"].HeaderText = "Blood Group";
                    dgvPatients.Columns["Phone"].HeaderText = "Phone";
                    dgvPatients.Columns["Email"].HeaderText = "Email";
                    dgvPatients.Columns["DateOfBirth"].HeaderText = "Date of Birth";

                    // ซ่อน ID Column
                    dgvPatients.Columns["PatientID"].Visible = false;
                }

                // แสดงจำนวน
                this.Text = $"Patient Management ({dt.Rows.Count} patients)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPatients(txtSearch.Text.Trim());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadPatients();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PatientFormDialog form = new PatientFormDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["PatientID"].Value);
            PatientFormDialog form = new PatientFormDialog(patientId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string patientName = dgvPatients.SelectedRows[0].Cells["FullName"].Value.ToString();
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete patient: {patientName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["PatientID"].Value);
                    
                    string query = "UPDATE Patients SET IsActive = 0 WHERE PatientID = @PatientID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", patientId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);

                    MessageBox.Show("Patient deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPatients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting patient: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
```

---

## 📝 ขั้นตอนที่ 6: สร้าง Patient Form Dialog

### สร้าง Form สำหรับเพิ่ม/แก้ไขผู้ป่วย:

1. คลิกขวาที่โฟลเดอร์ **Patients**
2. **Add** → **Windows Form**
3. ตั้งชื่อ `PatientFormDialog.cs`

### ออกแบบ UI:

**Form Properties:**
- Size: `500, 600`
- StartPosition: `CenterParent`
- FormBorderStyle: `FixedDialog`
- MaximizeBox: `False`
- MinimizeBox: `False`

**Controls ที่ต้องมี:**
- TextBox: `txtPatientCode`, `txtFirstName`, `txtLastName`, `txtPhone`, `txtEmail`, `txtAddress`
- DateTimePicker: `dtpDateOfBirth`
- ComboBox: `cboGender`, `cboBloodGroup`
- Button: `btnSave`, `btnCancel`

---

## 🔗 ขั้นตอนที่ 7: เชื่อมกับ Dashboard

### แก้ไข DashboardForm.cs:

```csharp
using HospitalMS.UI.Forms.Patients; // เพิ่มบรรทัดนี้

private void btnPatients_Click(object sender, EventArgs e)
{
    PatientListForm form = new PatientListForm();
    form.ShowDialog();
}
```

---

## 🧪 ขั้นตอนที่ 8: ทดสอบ

1. **Build** (Ctrl + Shift + B)
2. **Run** (F5)
3. **Login** → **Dashboard**
4. **คลิกปุ่ม Patients**
5. **ทดสอบ:**
   - ✅ แสดงรายชื่อผู้ป่วย
   - ✅ ค้นหาผู้ป่วย
   - ✅ Refresh
   - ✅ (Add/Edit/Delete จะทำในขั้นตอนถัดไป)

---

## 📊 สรุป

ในบทนี้เราได้:
- ✅ สร้าง Patient List Form
- ✅ แสดงรายชื่อผู้ป่วยใน DataGridView
- ✅ ค้นหาผู้ป่วย
- ✅ Refresh ข้อมูล
- ✅ เชื่อมกับ Dashboard

**ขั้นตอนต่อไป:**
- สร้าง Patient Form Dialog (เพิ่ม/แก้ไข)
- ทดสอบ CRUD ให้ครบ
- เพิ่มฟีเจอร์เพิ่มเติม

---

**พร้อมลงมือทำหรือยังครับ?** 🚀
