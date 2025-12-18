# 📝 บทที่ 10: Patient Form Dialog - เพิ่ม/แก้ไขผู้ป่วย

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่ม Save และ Cancel**ใน Designer เพื่อสร้าง Event Handler! ไม่งั้นปุ่มจะไม่ทำงาน! [อ่านเพิ่มเติม](00_IMPORTANT_TIPS.md)

## 📋 เป้าหมาย

สร้าง Form Dialog สำหรับ:
- ✅ เพิ่มผู้ป่วยใหม่
- ✅ แก้ไขข้อมูลผู้ป่วย
- ✅ Validation ข้อมูล
- ✅ บันทึกลงฐานข้อมูล

---

## 🎨 หน้าตาที่จะได้:

```
╔═══════════════════════════════════════╗
║  Add/Edit Patient                     ║
╠═══════════════════════════════════════╣
║  Patient Code:  [P2024___]            ║
║  First Name:    [____________]        ║
║  Last Name:     [____________]        ║
║  Date of Birth: [📅 DD/MM/YYYY]       ║
║  Gender:        [▼ Male/Female]       ║
║  Blood Group:   [▼ A+/B+/O+/AB+...]   ║
║  Phone:         [____________]        ║
║  Email:         [____________]        ║
║  Address:       [____________]        ║
║                 [____________]        ║
║                                       ║
║         [💾 Save] [❌ Cancel]         ║
╚═══════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนที่ 1: สร้าง Form

### 1. สร้าง Form ใหม่

1. คลิกขวาที่โฟลเดอร์ **Patients**
2. **Add** → **Windows Form**
3. ตั้งชื่อ `PatientFormDialog.cs`
4. คลิก **Add**

### 2. ตั้งค่า Form Properties

| Property | Value |
|----------|-------|
| **Name** | `PatientFormDialog` |
| **Text** | `Add Patient` |
| **Size** | `500, 550` |
| **StartPosition** | `CenterParent` |
| **FormBorderStyle** | `FixedDialog` |
| **MaximizeBox** | `False` |
| **MinimizeBox** | `False` |

---

## 📝 ขั้นตอนที่ 2: เพิ่ม Controls

### Labels และ TextBoxes:

**Patient Code:**
- Label: `lblPatientCode`, Text: `Patient Code:`
- TextBox: `txtPatientCode`, Location: `150, 20`

**First Name:**
- Label: `lblFirstName`, Text: `First Name: *`
- TextBox: `txtFirstName`, Location: `150, 60`

**Last Name:**
- Label: `lblLastName`, Text: `Last Name: *`
- TextBox: `txtLastName`, Location: `150, 100`

**Date of Birth:**
- Label: `lblDateOfBirth`, Text: `Date of Birth: *`
- DateTimePicker: `dtpDateOfBirth`, Location: `150, 140`

**Gender:**
- Label: `lblGender`, Text: `Gender: *`
- ComboBox: `cboGender`, Location: `150, 180`
  - Items: `Male`, `Female`

**Blood Group:**
- Label: `lblBloodGroup`, Text: `Blood Group:`
- ComboBox: `cboBloodGroup`, Location: `150, 220`
  - Items: `A+`, `A-`, `B+`, `B-`, `O+`, `O-`, `AB+`, `AB-`

**Phone:**
- Label: `lblPhone`, Text: `Phone:`
- TextBox: `txtPhone`, Location: `150, 260`

**Email:**
- Label: `lblEmail`, Text: `Email:`
- TextBox: `txtEmail`, Location: `150, 300`

**Address:**
- Label: `lblAddress`, Text: `Address:`
- TextBox: `txtAddress`, Location: `150, 340`, Multiline: `True`, Size: `300, 60`

### Buttons:

**Save:**
- Name: `btnSave`
- Text: `💾 Save`
- Location: `150, 450`
- Size: `120, 35`
- BackColor: `Green`
- ForeColor: `White`

**Cancel:**
- Name: `btnCancel`
- Text: `❌ Cancel`
- Location: `290, 450`
- Size: `120, 35`
- BackColor: `Gray`
- ForeColor: `White`
- DialogResult: `Cancel`

---

## 💻 ขั้นตอนที่ 3: เขียนโค้ด

### PatientFormDialog.cs:

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Patients
{
    public partial class PatientFormDialog : Form
    {
        private int? _patientId = null;
        private bool _isEditMode = false;

        // Constructor สำหรับ Add
        public PatientFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "Add Patient";
        }

        // Constructor สำหรับ Edit
        public PatientFormDialog(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            _isEditMode = true;
            this.Text = "Edit Patient";
        }

        private void PatientFormDialog_Load(object sender, EventArgs e)
        {
            // ตั้งค่า ComboBoxes
            cboGender.Items.AddRange(new string[] { "Male", "Female" });
            cboBloodGroup.Items.AddRange(new string[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" });

            if (_isEditMode && _patientId.HasValue)
            {
                LoadPatientData(_patientId.Value);
            }
            else
            {
                // Generate Patient Code
                txtPatientCode.Text = GeneratePatientCode();
            }
        }

        private string GeneratePatientCode()
        {
            try
            {
                string query = "SELECT TOP 1 PatientCode FROM Patients ORDER BY PatientID DESC";
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != null)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(1)) + 1;
                    return $"P{number:D7}";
                }
                else
                {
                    return "P0000001";
                }
            }
            catch
            {
                return "P0000001";
            }
        }

        private void LoadPatientData(int patientId)
        {
            try
            {
                string query = @"SELECT * FROM Patients WHERE PatientID = @PatientID";
                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patientId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtPatientCode.Text = row["PatientCode"].ToString();
                    txtPatientCode.ReadOnly = true;
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    dtpDateOfBirth.Value = Convert.ToDateTime(row["DateOfBirth"]);
                    cboGender.SelectedItem = row["Gender"].ToString();
                    cboBloodGroup.SelectedItem = row["BloodGroup"].ToString();
                    txtPhone.Text = row["Phone"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtAddress.Text = row["Address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Gender.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                if (_isEditMode && _patientId.HasValue)
                {
                    // Update
                    string query = @"UPDATE Patients SET 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        DateOfBirth = @DateOfBirth,
                        Gender = @Gender,
                        BloodGroup = @BloodGroup,
                        Phone = @Phone,
                        Email = @Email,
                        Address = @Address,
                        ModifiedDate = GETDATE()
                        WHERE PatientID = @PatientID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", _patientId.Value),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@DateOfBirth", dtpDateOfBirth.Value),
                        new SqlParameter("@Gender", cboGender.SelectedItem.ToString()),
                        new SqlParameter("@BloodGroup", cboBloodGroup.SelectedItem?.ToString() ?? (object)DBNull.Value),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim()),
                        new SqlParameter("@Address", txtAddress.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Patient updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert
                    string query = @"INSERT INTO Patients 
                        (PatientCode, FirstName, LastName, DateOfBirth, Gender, BloodGroup, 
                         Phone, Email, Address, IsActive, CreatedDate)
                        VALUES 
                        (@PatientCode, @FirstName, @LastName, @DateOfBirth, @Gender, @BloodGroup,
                         @Phone, @Email, @Address, 1, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientCode", txtPatientCode.Text.Trim()),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@DateOfBirth", dtpDateOfBirth.Value),
                        new SqlParameter("@Gender", cboGender.SelectedItem.ToString()),
                        new SqlParameter("@BloodGroup", cboBloodGroup.SelectedItem?.ToString() ?? (object)DBNull.Value),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim()),
                        new SqlParameter("@Address", txtAddress.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Patient added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving patient: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
```

---

## 🔗 ขั้นตอนที่ 4: เชื่อมกับ Patient List

### แก้ไข PatientListForm.cs:

```csharp
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
```

---

## 🧪 ทดสอบ

1. **Build** (Ctrl + Shift + B)
2. **Run** (F5)
3. **ทดสอบ Add:**
   - คลิก Add New Patient
   - กรอกข้อมูล
   - Save
4. **ทดสอบ Edit:**
   - เลือกผู้ป่วย
   - คลิก Edit
   - แก้ไขข้อมูล
   - Save

---

## 📊 สรุป

ได้:
- ✅ Patient Form Dialog
- ✅ Add Patient
- ✅ Edit Patient
- ✅ Validation
- ✅ Auto-generate Patient Code

**Patient Management เสร็จสมบูรณ์!** 🎉

---

**พร้อมทดสอบหรือยังครับ?** 🚀
