# 📅 บทที่ 11: Appointment Management - จัดการนัดหมาย

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่มทุกปุ่ม**ใน Designer เพื่อสร้าง Event Handler! [อ่านเพิ่มเติม](00_IMPORTANT_TIPS.md)

## 📋 เป้าหมาย

สร้างระบบจัดการนัดหมาย:
- ✅ แสดงรายการนัดหมาย
- ✅ สร้างนัดหมายใหม่
- ✅ เลือกผู้ป่วยและหมอ
- ✅ กำหนดวันเวลา
- ✅ อัพเดทสถานะ
- ✅ ค้นหานัดหมาย

---

## 🎨 หน้าตาที่จะได้:

### Appointment List:
```
╔═══════════════════════════════════════════════════════════╗
║  📅 Appointment Management                                ║
╠═══════════════════════════════════════════════════════════╣
║  Search: [____________] 🔍  Status: [▼ All]              ║
║  [+ New Appointment] [✏️ Edit] [✅ Complete] [🔄 Refresh] ║
╠═══════════════════════════════════════════════════════════╣
║  Date       │ Time  │ Patient        │ Doctor      │Status║
║  ──────────┼───────┼────────────────┼─────────────┼──────║
║  2024-12-18│ 09:00 │ Vichai Mangmee │ Dr. Somporn │Confirm║
║  2024-12-18│ 10:30 │ Suda Ramruay   │ Dr. Somchai │Confirm║
║  2024-12-19│ 14:00 │ Prasert Deengam│ Dr. Somying │Pending║
╚═══════════════════════════════════════════════════════════╝
```

### Appointment Form:
```
╔═══════════════════════════════════════╗
║  Add/Edit Appointment                 ║
╠═══════════════════════════════════════╣
║  Patient:    [▼ Select Patient]       ║
║  Doctor:     [▼ Select Doctor]        ║
║  Date:       [📅 DD/MM/YYYY]          ║
║  Time:       [🕐 HH:MM]               ║
║  Status:     [▼ Pending/Confirmed]    ║
║  Reason:     [_________________]      ║
║              [_________________]      ║
║                                       ║
║       [💾 Save] [❌ Cancel]           ║
╚═══════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนที่ 1: สร้าง Appointment List Form

### 1. สร้าง Form ใหม่

1. คลิกขวาที่โฟลเดอร์ **Forms**
2. **Add** → **New Folder** → ตั้งชื่อ `Appointments`
3. คลิกขวาที่โฟลเดอร์ **Appointments**
4. **Add** → **Windows Form**
5. ตั้งชื่อ `AppointmentListForm.cs`

### 2. ตั้งค่า Form Properties

| Property | Value |
|----------|-------|
| **Name** | `AppointmentListForm` |
| **Text** | `Appointment Management` |
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

**Status ComboBox:**
- Name: `cboStatusFilter`
- Location: `400, 18`
- Size: `150, 25`
- Items: `All`, `Pending`, `Confirmed`, `Completed`, `Cancelled`

**Search Button:**
- Name: `btnSearch`
- Text: `🔍 Search`
- Location: `570, 15`

---

## 🔘 ขั้นตอนที่ 3: สร้าง Action Buttons

### Panel สำหรับปุ่ม:

| Property | Value |
|----------|-------|
| **Name** | `panelActions` |
| **Dock** | `Top` |
| **Height** | `60` |

### ปุ่ม 4 ปุ่ม:

**New Appointment:**
- Name: `btnNew`
- Text: `+ New Appointment`
- Size: `150, 35`
- BackColor: `Green`

**Edit:**
- Name: `btnEdit`
- Text: `✏️ Edit`
- BackColor: `Orange`

**Complete:**
- Name: `btnComplete`
- Text: `✅ Complete`
- BackColor: `Blue`

**Refresh:**
- Name: `btnRefresh`
- Text: `🔄 Refresh`
- BackColor: `Gray`

---

## 📊 ขั้นตอนที่ 4: สร้าง DataGridView

| Property | Value |
|----------|-------|
| **Name** | `dgvAppointments` |
| **Dock** | `Fill` |
| **ReadOnly** | `True` |
| **SelectionMode** | `FullRowSelect` |

---

## 💻 ขั้นตอนที่ 5: เขียนโค้ด AppointmentListForm.cs

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Appointments
{
    public partial class AppointmentListForm : Form
    {
        public AppointmentListForm()
        {
            InitializeComponent();
        }

        private void AppointmentListForm_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Status Filter
            cboStatusFilter.SelectedIndex = 0; // All
            LoadAppointments();
        }

        private void LoadAppointments(string searchTerm = "", string statusFilter = "All")
        {
            try
            {
                string query = @"
                    SELECT 
                        a.AppointmentID,
                        CONVERT(VARCHAR(10), a.AppointmentDate, 103) AS AppointmentDate,
                        CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS AppointmentTime,
                        p.FirstName + ' ' + p.LastName AS PatientName,
                        d.FirstName + ' ' + d.LastName AS DoctorName,
                        a.Status,
                        a.Reason
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientID = p.PatientID
                    INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                    WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (p.FirstName LIKE @Search 
                                OR p.LastName LIKE @Search 
                                OR d.FirstName LIKE @Search 
                                OR d.LastName LIKE @Search)";
                }

                if (statusFilter != "All")
                {
                    query += " AND a.Status = @Status";
                }

                query += " ORDER BY a.AppointmentDate DESC, a.AppointmentTime DESC";

                SqlParameter[] parameters = null;
                var paramList = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    paramList.Add(new SqlParameter("@Search", $"%{searchTerm}%"));
                }

                if (statusFilter != "All")
                {
                    paramList.Add(new SqlParameter("@Status", statusFilter));
                }

                parameters = paramList.Count > 0 ? paramList.ToArray() : null;

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvAppointments.DataSource = dt;

                // ตั้งค่า Columns
                if (dgvAppointments.Columns.Count > 0)
                {
                    dgvAppointments.Columns["AppointmentID"].Visible = false;
                    dgvAppointments.Columns["AppointmentDate"].HeaderText = "Date";
                    dgvAppointments.Columns["AppointmentTime"].HeaderText = "Time";
                    dgvAppointments.Columns["PatientName"].HeaderText = "Patient";
                    dgvAppointments.Columns["DoctorName"].HeaderText = "Doctor";
                    dgvAppointments.Columns["Status"].HeaderText = "Status";
                    dgvAppointments.Columns["Reason"].HeaderText = "Reason";
                }

                this.Text = $"Appointment Management ({dt.Rows.Count} appointments)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAppointments(txtSearch.Text.Trim(), cboStatusFilter.SelectedItem.ToString());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboStatusFilter.SelectedIndex = 0;
            LoadAppointments();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AppointmentFormDialog form = new AppointmentFormDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadAppointments();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);
            AppointmentFormDialog form = new AppointmentFormDialog(appointmentId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadAppointments();
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Mark this appointment as Completed?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

                    string query = "UPDATE Appointments SET Status = 'Completed' WHERE AppointmentID = @AppointmentID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@AppointmentID", appointmentId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Appointment marked as Completed!", "Success");
                    LoadAppointments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }
        }

        private void cboStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAppointments(txtSearch.Text.Trim(), cboStatusFilter.SelectedItem.ToString());
        }
    }
}
```

---

## 📝 ขั้นตอนที่ 6: สร้าง Appointment Form Dialog

### สร้าง Form:

1. **Add** → **Windows Form**
2. ตั้งชื่อ `AppointmentFormDialog.cs`
3. Size: `500, 450`

### Controls:

**Patient ComboBox:**
- Name: `cboPatient`
- Location: `150, 20`

**Doctor ComboBox:**
- Name: `cboDoctor`
- Location: `150, 60`

**Date:**
- Name: `dtpDate`
- Location: `150, 100`

**Time:**
- Name: `dtpTime`
- Location: `150, 140`
- Format: `Time`
- ShowUpDown: `True`

**Status:**
- Name: `cboStatus`
- Items: `Pending`, `Confirmed`, `Completed`, `Cancelled`

**Reason:**
- Name: `txtReason`
- Multiline: `True`
- Size: `300, 60`

---

## 💻 AppointmentFormDialog.cs โค้ด:

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Appointments
{
    public partial class AppointmentFormDialog : Form
    {
        private int? _appointmentId = null;
        private bool _isEditMode = false;

        public AppointmentFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "New Appointment";
        }

        public AppointmentFormDialog(int appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _isEditMode = true;
            this.Text = "Edit Appointment";
        }

        private void AppointmentFormDialog_Load(object sender, EventArgs e)
        {
            LoadPatients();
            LoadDoctors();
            
            cboStatus.Items.AddRange(new string[] { "Pending", "Confirmed", "Completed", "Cancelled" });
            cboStatus.SelectedIndex = 0;

            if (_isEditMode && _appointmentId.HasValue)
            {
                LoadAppointmentData(_appointmentId.Value);
            }
        }

        private void LoadPatients()
        {
            try
            {
                string query = "SELECT PatientID, FirstName + ' ' + LastName AS FullName FROM Patients WHERE IsActive = 1 ORDER BY FirstName";
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                cboPatient.DisplayMember = "FullName";
                cboPatient.ValueMember = "PatientID";
                cboPatient.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message);
            }
        }

        private void LoadDoctors()
        {
            try
            {
                string query = "SELECT DoctorID, FirstName + ' ' + LastName AS FullName FROM Doctors WHERE IsActive = 1 ORDER BY FirstName";
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                cboDoctor.DisplayMember = "FullName";
                cboDoctor.ValueMember = "DoctorID";
                cboDoctor.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctors: " + ex.Message);
            }
        }

        private void LoadAppointmentData(int appointmentId)
        {
            try
            {
                string query = "SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID";
                SqlParameter[] parameters = { new SqlParameter("@AppointmentID", appointmentId) };
                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cboPatient.SelectedValue = row["PatientID"];
                    cboDoctor.SelectedValue = row["DoctorID"];
                    dtpDate.Value = Convert.ToDateTime(row["AppointmentDate"]);
                    dtpTime.Value = DateTime.Today.Add((TimeSpan)row["AppointmentTime"]);
                    cboStatus.SelectedItem = row["Status"].ToString();
                    txtReason.Text = row["Reason"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboPatient.SelectedIndex == -1 || cboDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Patient and Doctor.");
                return;
            }

            try
            {
                if (_isEditMode)
                {
                    string query = @"UPDATE Appointments SET 
                        PatientID = @PatientID,
                        DoctorID = @DoctorID,
                        AppointmentDate = @Date,
                        AppointmentTime = @Time,
                        Status = @Status,
                        Reason = @Reason
                        WHERE AppointmentID = @AppointmentID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@AppointmentID", _appointmentId.Value),
                        new SqlParameter("@PatientID", cboPatient.SelectedValue),
                        new SqlParameter("@DoctorID", cboDoctor.SelectedValue),
                        new SqlParameter("@Date", dtpDate.Value.Date),
                        new SqlParameter("@Time", dtpTime.Value.TimeOfDay),
                        new SqlParameter("@Status", cboStatus.SelectedItem.ToString()),
                        new SqlParameter("@Reason", txtReason.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Appointment updated!");
                }
                else
                {
                    string query = @"INSERT INTO Appointments 
                        (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, CreatedDate)
                        VALUES (@PatientID, @DoctorID, @Date, @Time, @Status, @Reason, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", cboPatient.SelectedValue),
                        new SqlParameter("@DoctorID", cboDoctor.SelectedValue),
                        new SqlParameter("@Date", dtpDate.Value.Date),
                        new SqlParameter("@Time", dtpTime.Value.TimeOfDay),
                        new SqlParameter("@Status", cboStatus.SelectedItem.ToString()),
                        new SqlParameter("@Reason", txtReason.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Appointment created!");
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
private void btnAppointments_Click(object sender, EventArgs e)
{
    AppointmentListForm form = new AppointmentListForm();
    form.ShowDialog();
}
```

---

## 🧪 ทดสอบ

1. Build (Ctrl + Shift + B)
2. Run (F5)
3. Dashboard → Appointments
4. ทดสอบ:
   - ✅ สร้างนัดหมาย
   - ✅ แก้ไข
   - ✅ Complete
   - ✅ ค้นหา

---

## 📊 สรุป

ได้:
- ✅ Appointment List
- ✅ Create/Edit Appointment
- ✅ Patient/Doctor Selection
- ✅ Date/Time Picker
- ✅ Status Management
- ✅ Search & Filter

**Appointment Management เสร็จสมบูรณ์!** 🎉

---

**พร้อมลงมือทำหรือยังครับ?** 🚀
