# 👨‍⚕️ บทที่ 12: Doctor Management - จัดการข้อมูลแพทย์

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่มทุกปุ่ม**ใน Designer เพื่อสร้าง Event Handler! [อ่านเพิ่มเติม](00_IMPORTANT_TIPS.md)

## 📋 เป้าหมาย

สร้างระบบจัดการแพทย์:
- ✅ แสดงรายการแพทย์
- ✅ เพิ่มแพทย์ใหม่
- ✅ แก้ไขข้อมูลแพทย์
- ✅ ลบแพทย์ (Soft Delete)
- ✅ ค้นหาแพทย์
- ✅ กรองตามแผนก (Specialization)

---

## 🎨 หน้าตาที่จะได้:

### Doctor List:
```
╔═══════════════════════════════════════════════════════════╗
║  👨‍⚕️ Doctor Management                                    ║
╠═══════════════════════════════════════════════════════════╣
║  Search: [____________] 🔍  Dept: [▼ All]                ║
║  [+ Add Doctor] [✏️ Edit] [🗑️ Delete] [🔄 Refresh]       ║
╠═══════════════════════════════════════════════════════════╣
║  Code    │ Name           │ Specialization │ Phone       ║
║  ────────┼────────────────┼────────────────┼─────────────║
║  D0000001│ Dr. Somchai    │ Cardiology     │ 081-234-5678║
║  D0000002│ Dr. Somying    │ Pediatrics     │ 082-345-6789║
╚═══════════════════════════════════════════════════════════╝
```

### Doctor Form:
```
╔═══════════════════════════════════════╗
║  Add/Edit Doctor                      ║
╠═══════════════════════════════════════╣
║  Doctor Code:  [D0000003] (Auto)      ║
║  First Name:   [_________________] *  ║
║  Last Name:    [_________________] *  ║
║  Specialization: [▼ Select Dept] *    ║
║  License No:   [_________________]    ║
║  Phone:        [_________________]    ║
║  Email:        [_________________]    ║
║                                       ║
║       [💾 Save] [❌ Cancel]           ║
╚═══════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนที่ 1: สร้าง Doctor List Form

### 1. สร้าง Form ใหม่

1. คลิกขวาที่โฟลเดอร์ **Forms**
2. **Add** → **New Folder** → ตั้งชื่อ `Doctors`
3. คลิกขวาที่โฟลเดอร์ **Doctors**
4. **Add** → **Windows Form**
5. ตั้งชื่อ `DoctorListForm.cs`

### 2. ตั้งค่า Form Properties

| Property | Value |
|----------|-------|
| **Name** | `DoctorListForm` |
| **Text** | `Doctor Management` |
| **Size** | `1000, 600` |
| **StartPosition** | `CenterScreen` |
| **FormBorderStyle** | `FixedDialog` |
| **MaximizeBox** | `False` |

---

## 🔍 ขั้นตอนที่ 2: สร้าง Search Panel

### Panel สำหรับค้นหา:

| Property | Value |
|----------|-------|
| **Name** | `panelSearch` |
| **Dock** | `Top` |
| **Height** | `60` |
| **BackColor** | `WhiteSmoke` |

### Controls:

**Search TextBox:**
- Name: `txtSearch`
- Location: `80, 18`
- Size: `250, 25`

**Specialization Filter:**
- Name: `cboSpecialization`
- Location: `400, 18`
- Size: `150, 25`

**Search Button:**
- Name: `btnSearch`
- Text: `🔍 Search`
- Location: `570, 15`
- BackColor: `DodgerBlue`

---

## 🔘 ขั้นตอนที่ 3: สร้าง Action Buttons

### Panel สำหรับปุ่ม:

| Property | Value |
|----------|-------|
| **Name** | `panelActions` |
| **Dock** | `Top` |
| **Height** | `60` |

### ปุ่ม 4 ปุ่ม:

**Add Doctor:**
- Name: `btnAdd`
- Text: `+ Add Doctor`
- Size: `130, 35`
- BackColor: `Green`

**Edit:**
- Name: `btnEdit`
- Text: `✏️ Edit`
- BackColor: `Orange`

**Delete:**
- Name: `btnDelete`
- Text: `🗑️ Delete`
- BackColor: `Red`

**Refresh:**
- Name: `btnRefresh`
- Text: `🔄 Refresh`
- BackColor: `Gray`

---

## 📊 ขั้นตอนที่ 4: สร้าง DataGridView

| Property | Value |
|----------|-------|
| **Name** | `dgvDoctors` |
| **Dock** | `Fill` |
| **ReadOnly** | `True` |
| **SelectionMode** | `FullRowSelect` |
| **AllowUserToAddRows** | `False` |

---

## 💻 ขั้นตอนที่ 5: เขียนโค้ด DoctorListForm.cs

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Doctors
{
    public partial class DoctorListForm : Form
    {
        public DoctorListForm()
        {
            InitializeComponent();
        }

        private void DoctorListForm_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Specialization Filter
            cboSpecialization.Items.AddRange(new string[] { 
                "All", "Cardiology", "Pediatrics", "Orthopedics", 
                "Neurology", "Dermatology", "General Practice" 
            });
            cboSpecialization.SelectedIndex = 0;
            LoadDoctors();
        }

        private void LoadDoctors(string searchTerm = "", string specialization = "All")
        {
            try
            {
                string query = @"
                    SELECT 
                        DoctorID,
                        DoctorCode,
                        FirstName + ' ' + LastName AS FullName,
                        Specialization,
                        LicenseNumber,
                        Phone,
                        Email
                    FROM Doctors
                    WHERE IsActive = 1";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (FirstName LIKE @Search 
                                OR LastName LIKE @Search 
                                OR DoctorCode LIKE @Search)";
                }

                if (specialization != "All")
                {
                    query += " AND Specialization = @Specialization";
                }

                query += " ORDER BY DoctorCode";

                var paramList = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    paramList.Add(new SqlParameter("@Search", $"%{searchTerm}%"));
                }

                if (specialization != "All")
                {
                    paramList.Add(new SqlParameter("@Specialization", specialization));
                }

                SqlParameter[] parameters = paramList.Count > 0 ? paramList.ToArray() : null;

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvDoctors.DataSource = dt;

                // ตั้งค่า Columns
                if (dgvDoctors.Columns.Count > 0)
                {
                    dgvDoctors.Columns["DoctorID"].Visible = false;
                    dgvDoctors.Columns["DoctorCode"].HeaderText = "Code";
                    dgvDoctors.Columns["FullName"].HeaderText = "Doctor Name";
                    dgvDoctors.Columns["Specialization"].HeaderText = "Specialization";
                    dgvDoctors.Columns["LicenseNumber"].HeaderText = "License No.";
                    dgvDoctors.Columns["Phone"].HeaderText = "Phone";
                    dgvDoctors.Columns["Email"].HeaderText = "Email";
                }

                this.Text = $"Doctor Management ({dt.Rows.Count} doctors)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctors: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string spec = cboSpecialization.SelectedItem != null ? 
                cboSpecialization.SelectedItem.ToString() : "All";
            LoadDoctors(txtSearch.Text.Trim(), spec);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cboSpecialization.Items.Count > 0)
            {
                cboSpecialization.SelectedIndex = 0;
            }
            LoadDoctors();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DoctorFormDialog form = new DoctorFormDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDoctors();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int doctorId = Convert.ToInt32(dgvDoctors.SelectedRows[0].Cells["DoctorID"].Value);
            DoctorFormDialog form = new DoctorFormDialog(doctorId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDoctors();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string doctorName = dgvDoctors.SelectedRows[0].Cells["FullName"].Value.ToString();
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete Dr. {doctorName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int doctorId = Convert.ToInt32(dgvDoctors.SelectedRows[0].Cells["DoctorID"].Value);

                    string query = "UPDATE Doctors SET IsActive = 0 WHERE DoctorID = @DoctorID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@DoctorID", doctorId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Doctor deleted successfully!", "Success");
                    LoadDoctors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }
        }

        private void cboSpecialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSpecialization.SelectedItem != null)
            {
                LoadDoctors(txtSearch.Text.Trim(), cboSpecialization.SelectedItem.ToString());
            }
        }
    }
}
```

---

## 📝 ขั้นตอนที่ 6: สร้าง Doctor Form Dialog

### สร้าง Form:

1. **Add** → **Windows Form**
2. ตั้งชื่อ `DoctorFormDialog.cs`
3. Size: `500, 450`

### Controls:

**Doctor Code:**
- Name: `txtDoctorCode`
- ReadOnly: `True`

**First Name:**
- Name: `txtFirstName`

**Last Name:**
- Name: `txtLastName`

**Specialization:**
- Name: `cboSpecialization`
- Items: Cardiology, Pediatrics, etc.

**License Number:**
- Name: `txtLicenseNumber`

**Phone:**
- Name: `txtPhone`

**Email:**
- Name: `txtEmail`

**Buttons:**
- `btnSave` - Green
- `btnCancel` - Red

---

## 💻 DoctorFormDialog.cs โค้ด:

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Doctors
{
    public partial class DoctorFormDialog : Form
    {
        private int? _doctorId = null;
        private bool _isEditMode = false;

        public DoctorFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "Add Doctor";
        }

        public DoctorFormDialog(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;
            _isEditMode = true;
            this.Text = "Edit Doctor";
        }

        private void DoctorFormDialog_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Specialization
            cboSpecialization.Items.AddRange(new string[] { 
                "Cardiology", "Pediatrics", "Orthopedics", 
                "Neurology", "Dermatology", "General Practice" 
            });

            if (_isEditMode && _doctorId.HasValue)
            {
                LoadDoctorData(_doctorId.Value);
            }
            else
            {
                txtDoctorCode.Text = GenerateDoctorCode();
            }
        }

        private string GenerateDoctorCode()
        {
            try
            {
                string query = "SELECT TOP 1 DoctorCode FROM Doctors ORDER BY DoctorID DESC";
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != null)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(1)) + 1;
                    return $"D{number:D7}";
                }
                else
                {
                    return "D0000001";
                }
            }
            catch
            {
                return "D0000001";
            }
        }

        private void LoadDoctorData(int doctorId)
        {
            try
            {
                string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";
                SqlParameter[] parameters = {
                    new SqlParameter("@DoctorID", doctorId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtDoctorCode.Text = row["DoctorCode"].ToString();
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    cboSpecialization.SelectedItem = row["Specialization"].ToString();
                    txtLicenseNumber.Text = row["LicenseNumber"].ToString();
                    txtPhone.Text = row["Phone"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name.", "Validation Error");
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name.", "Validation Error");
                txtLastName.Focus();
                return false;
            }

            if (cboSpecialization.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Specialization.", "Validation Error");
                cboSpecialization.Focus();
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
                if (_isEditMode && _doctorId.HasValue)
                {
                    // Update
                    string query = @"UPDATE Doctors SET 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Specialization = @Specialization,
                        LicenseNumber = @LicenseNumber,
                        Phone = @Phone,
                        Email = @Email,
                        ModifiedDate = GETDATE()
                        WHERE DoctorID = @DoctorID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@DoctorID", _doctorId.Value),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@Specialization", cboSpecialization.SelectedItem.ToString()),
                        new SqlParameter("@LicenseNumber", txtLicenseNumber.Text.Trim()),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Doctor updated successfully!");
                }
                else
                {
                    // Insert
                    string query = @"INSERT INTO Doctors 
                        (DoctorCode, FirstName, LastName, Specialization, LicenseNumber, 
                         Phone, Email, IsActive, CreatedDate)
                        VALUES 
                        (@DoctorCode, @FirstName, @LastName, @Specialization, @LicenseNumber,
                         @Phone, @Email, 1, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@DoctorCode", txtDoctorCode.Text.Trim()),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@Specialization", cboSpecialization.SelectedItem.ToString()),
                        new SqlParameter("@LicenseNumber", txtLicenseNumber.Text.Trim()),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Doctor added successfully!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

## 🔗 เชื่อมกับ Dashboard

```csharp
private void btnDoctors_Click(object sender, EventArgs e)
{
    DoctorListForm form = new DoctorListForm();
    form.ShowDialog();
}
```

---

## 🧪 ทดสอบ

1. Build (Ctrl + Shift + B)
2. Run (F5)
3. Dashboard → Doctors
4. ทดสอบ:
   - ✅ Add Doctor
   - ✅ Edit
   - ✅ Delete
   - ✅ Search
   - ✅ Filter by Specialization

---

## 📊 สรุป

ได้:
- ✅ Doctor List Form
- ✅ Doctor Form Dialog
- ✅ CRUD Operations
- ✅ Search & Filter
- ✅ Auto Doctor Code
- ✅ Specialization Management

**Doctor Management เสร็จสมบูรณ์!** 🎉

---

**พร้อมลงมือทำหรือยังครับ?** 🚀
