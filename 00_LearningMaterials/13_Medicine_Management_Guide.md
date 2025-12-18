# 💊 บทที่ 13: Medicine Management - จัดการข้อมูลยา

> ⚠️ **สำคัญมาก!** อย่าลืม**ดับเบิลคลิกปุ่มทุกปุ่ม**ใน Designer เพื่อสร้าง Event Handler! [อ่านเพิ่มเติม](00_IMPORTANT_TIPS.md)

## 📋 เป้าหมาย

สร้างระบบจัดการยา:
- ✅ แสดงรายการยา
- ✅ เพิ่มยาใหม่
- ✅ แก้ไขข้อมูลยา
- ✅ ลบยา (Soft Delete)
- ✅ ค้นหายา
- ✅ กรองตามหมวดหมู่
- ✅ ติดตาม Stock (คงเหลือ)

---

## 🎨 หน้าตาที่จะได้:

### Medicine List:
```
╔═══════════════════════════════════════════════════════════╗
║  💊 Medicine Management                                    ║
╠═══════════════════════════════════════════════════════════╣
║  Search: [____________] 🔍  Category: [▼ All]            ║
║  [+ Add Medicine] [✏️ Edit] [🗑️ Delete] [🔄 Refresh]     ║
╠═══════════════════════════════════════════════════════════╣
║  Code    │ Name           │ Category   │ Stock │ Price   ║
║  ────────┼────────────────┼────────────┼───────┼─────────║
║  M0000001│ Paracetamol    │ Painkiller │ 500   │ 5.00    ║
║  M0000002│ Amoxicillin    │ Antibiotic │ 250   │ 15.00   ║
╚═══════════════════════════════════════════════════════════╝
```

### Medicine Form:
```
╔═══════════════════════════════════════╗
║  Add/Edit Medicine                    ║
╠═══════════════════════════════════════╣
║  Medicine Code: [M0000003] (Auto)     ║
║  Name:          [_________________] * ║
║  Category:      [▼ Select Category] * ║
║  Manufacturer:  [_________________]   ║
║  Unit Price:    [_________________] * ║
║  Stock Qty:     [_________________] * ║
║  Reorder Level: [_________________]   ║
║  Description:   [_________________]   ║
║                                       ║
║       [💾 Save] [❌ Cancel]           ║
╚═══════════════════════════════════════╝
```

---

## 🛠️ ขั้นตอนที่ 1: สร้าง Medicine List Form

### 1. สร้าง Form ใหม่

1. คลิกขวาที่โฟลเดอร์ **Forms**
2. **Add** → **New Folder** → ตั้งชื่อ `Medicines`
3. คลิกขวาที่โฟลเดอร์ **Medicines**
4. **Add** → **Windows Form**
5. ตั้งชื่อ `MedicineListForm.cs`

### 2. ตั้งค่า Form Properties

| Property | Value |
|----------|-------|
| **Name** | `MedicineListForm` |
| **Text** | `Medicine Management` |
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

**Category Filter:**
- Name: `cboCategory`
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

**Add Medicine:**
- Name: `btnAdd`
- Text: `+ Add Medicine`
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
| **Name** | `dgvMedicines` |
| **Dock** | `Fill` |
| **ReadOnly** | `True` |
| **SelectionMode** | `FullRowSelect` |
| **AllowUserToAddRows** | `False` |

---

## 💻 ขั้นตอนที่ 5: เขียนโค้ด MedicineListForm.cs

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Medicines
{
    public partial class MedicineListForm : Form
    {
        public MedicineListForm()
        {
            InitializeComponent();
        }

        private void MedicineListForm_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Category Filter
            cboCategory.Items.AddRange(new string[] { 
                "All", "Painkiller", "Antibiotic", "Antiviral", 
                "Vitamin", "Supplement", "Other" 
            });
            cboCategory.SelectedIndex = 0;
            LoadMedicines();
        }

        private void LoadMedicines(string searchTerm = "", string category = "All")
        {
            try
            {
                string query = @"
                    SELECT 
                        MedicineID,
                        MedicineCode,
                        Name,
                        Category,
                        Manufacturer,
                        UnitPrice,
                        StockQuantity,
                        ReorderLevel
                    FROM Medicines
                    WHERE IsActive = 1";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (Name LIKE @Search 
                                OR MedicineCode LIKE @Search 
                                OR Manufacturer LIKE @Search)";
                }

                if (category != "All")
                {
                    query += " AND Category = @Category";
                }

                query += " ORDER BY MedicineCode";

                var paramList = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    paramList.Add(new SqlParameter("@Search", $"%{searchTerm}%"));
                }

                if (category != "All")
                {
                    paramList.Add(new SqlParameter("@Category", category));
                }

                SqlParameter[] parameters = paramList.Count > 0 ? paramList.ToArray() : null;

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvMedicines.DataSource = dt;

                // ตั้งค่า Columns
                if (dgvMedicines.Columns.Count > 0)
                {
                    dgvMedicines.Columns["MedicineID"].Visible = false;
                    dgvMedicines.Columns["MedicineCode"].HeaderText = "Code";
                    dgvMedicines.Columns["MedicineCode"].Width = 100;
                    dgvMedicines.Columns["Name"].HeaderText = "Medicine Name";
                    dgvMedicines.Columns["Name"].Width = 200;
                    dgvMedicines.Columns["Category"].HeaderText = "Category";
                    dgvMedicines.Columns["Category"].Width = 120;
                    dgvMedicines.Columns["Manufacturer"].HeaderText = "Manufacturer";
                    dgvMedicines.Columns["Manufacturer"].Width = 150;
                    dgvMedicines.Columns["UnitPrice"].HeaderText = "Price";
                    dgvMedicines.Columns["UnitPrice"].Width = 80;
                    dgvMedicines.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                    dgvMedicines.Columns["StockQuantity"].HeaderText = "Stock";
                    dgvMedicines.Columns["StockQuantity"].Width = 80;
                    dgvMedicines.Columns["ReorderLevel"].HeaderText = "Reorder";
                    dgvMedicines.Columns["ReorderLevel"].Width = 80;

                    // เปลี่ยนสีแถวที่ Stock ต่ำกว่า Reorder Level
                    foreach (DataGridViewRow row in dgvMedicines.Rows)
                    {
                        int stock = Convert.ToInt32(row.Cells["StockQuantity"].Value);
                        int reorder = Convert.ToInt32(row.Cells["ReorderLevel"].Value);
                        
                        if (stock <= reorder)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        }
                    }
                }

                this.Text = $"Medicine Management ({dt.Rows.Count} medicines)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading medicines: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string cat = cboCategory.SelectedItem != null ? 
                cboCategory.SelectedItem.ToString() : "All";
            LoadMedicines(txtSearch.Text.Trim(), cat);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex = 0;
            }
            LoadMedicines();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MedicineFormDialog form = new MedicineFormDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMedicines();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a medicine to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int medicineId = Convert.ToInt32(dgvMedicines.SelectedRows[0].Cells["MedicineID"].Value);
            MedicineFormDialog form = new MedicineFormDialog(medicineId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMedicines();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a medicine to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string medicineName = dgvMedicines.SelectedRows[0].Cells["Name"].Value.ToString();
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete {medicineName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int medicineId = Convert.ToInt32(dgvMedicines.SelectedRows[0].Cells["MedicineID"].Value);

                    string query = "UPDATE Medicines SET IsActive = 0 WHERE MedicineID = @MedicineID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@MedicineID", medicineId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Medicine deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMedicines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedItem != null)
            {
                LoadMedicines(txtSearch.Text.Trim(), cboCategory.SelectedItem.ToString());
            }
        }

        private void dgvMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
```

---

## 📝 ขั้นตอนที่ 6: สร้าง Medicine Form Dialog

### สร้าง Form:

1. **Add** → **Windows Form**
2. ตั้งชื่อ `MedicineFormDialog.cs`
3. Size: `500, 500`

### Controls:

**Medicine Code:**
- Name: `txtMedicineCode`
- ReadOnly: `True`

**Name:**
- Name: `txtName`

**Category:**
- Name: `cboCategory`
- Items: Painkiller, Antibiotic, etc.

**Manufacturer:**
- Name: `txtManufacturer`

**Unit Price:**
- Name: `txtUnitPrice`

**Stock Quantity:**
- Name: `txtStockQuantity`

**Reorder Level:**
- Name: `txtReorderLevel`

**Description:**
- Name: `txtDescription`
- Multiline: `True`

**Buttons:**
- `btnSave` - Green
- `btnCancel` - Red

---

## 💻 MedicineFormDialog.cs โค้ด:

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Medicines
{
    public partial class MedicineFormDialog : Form
    {
        private int? _medicineId = null;
        private bool _isEditMode = false;

        public MedicineFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "Add Medicine";
        }

        public MedicineFormDialog(int medicineId)
        {
            InitializeComponent();
            _medicineId = medicineId;
            _isEditMode = true;
            this.Text = "Edit Medicine";
        }

        private void MedicineFormDialog_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Category
            cboCategory.Items.AddRange(new string[] { 
                "Painkiller", "Antibiotic", "Antiviral", 
                "Vitamin", "Supplement", "Other" 
            });

            if (_isEditMode && _medicineId.HasValue)
            {
                LoadMedicineData(_medicineId.Value);
            }
            else
            {
                txtMedicineCode.Text = GenerateMedicineCode();
                txtStockQuantity.Text = "0";
                txtReorderLevel.Text = "10";
            }
        }

        private string GenerateMedicineCode()
        {
            try
            {
                string query = "SELECT TOP 1 MedicineCode FROM Medicines ORDER BY MedicineID DESC";
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != null)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(1)) + 1;
                    return $"M{number:D7}";
                }
                else
                {
                    return "M0000001";
                }
            }
            catch
            {
                return "M0000001";
            }
        }

        private void LoadMedicineData(int medicineId)
        {
            try
            {
                string query = "SELECT * FROM Medicines WHERE MedicineID = @MedicineID";
                SqlParameter[] parameters = {
                    new SqlParameter("@MedicineID", medicineId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtMedicineCode.Text = row["MedicineCode"].ToString();
                    txtName.Text = row["Name"].ToString();
                    cboCategory.SelectedItem = row["Category"].ToString();
                    txtManufacturer.Text = row["Manufacturer"].ToString();
                    txtUnitPrice.Text = row["UnitPrice"].ToString();
                    txtStockQuantity.Text = row["StockQuantity"].ToString();
                    txtReorderLevel.Text = row["ReorderLevel"].ToString();
                    txtDescription.Text = row["Description"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading medicine data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter Medicine Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cboCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCategory.Focus();
                return false;
            }

            decimal price;
            if (!decimal.TryParse(txtUnitPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Please enter valid Unit Price.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitPrice.Focus();
                return false;
            }

            int stock;
            if (!int.TryParse(txtStockQuantity.Text, out stock) || stock < 0)
            {
                MessageBox.Show("Please enter valid Stock Quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockQuantity.Focus();
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
                if (_isEditMode && _medicineId.HasValue)
                {
                    // Update
                    string query = @"UPDATE Medicines SET 
                        Name = @Name,
                        Category = @Category,
                        Manufacturer = @Manufacturer,
                        UnitPrice = @UnitPrice,
                        StockQuantity = @StockQuantity,
                        ReorderLevel = @ReorderLevel,
                        Description = @Description,
                        ModifiedDate = GETDATE()
                        WHERE MedicineID = @MedicineID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@MedicineID", _medicineId.Value),
                        new SqlParameter("@Name", txtName.Text.Trim()),
                        new SqlParameter("@Category", cboCategory.SelectedItem.ToString()),
                        new SqlParameter("@Manufacturer", txtManufacturer.Text.Trim()),
                        new SqlParameter("@UnitPrice", decimal.Parse(txtUnitPrice.Text)),
                        new SqlParameter("@StockQuantity", int.Parse(txtStockQuantity.Text)),
                        new SqlParameter("@ReorderLevel", int.Parse(txtReorderLevel.Text)),
                        new SqlParameter("@Description", txtDescription.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Medicine updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert
                    string query = @"INSERT INTO Medicines 
                        (MedicineCode, Name, Category, Manufacturer, UnitPrice, 
                         StockQuantity, ReorderLevel, Description, IsActive, CreatedDate)
                        VALUES 
                        (@MedicineCode, @Name, @Category, @Manufacturer, @UnitPrice,
                         @StockQuantity, @ReorderLevel, @Description, 1, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@MedicineCode", txtMedicineCode.Text.Trim()),
                        new SqlParameter("@Name", txtName.Text.Trim()),
                        new SqlParameter("@Category", cboCategory.SelectedItem.ToString()),
                        new SqlParameter("@Manufacturer", txtManufacturer.Text.Trim()),
                        new SqlParameter("@UnitPrice", decimal.Parse(txtUnitPrice.Text)),
                        new SqlParameter("@StockQuantity", int.Parse(txtStockQuantity.Text)),
                        new SqlParameter("@ReorderLevel", int.Parse(txtReorderLevel.Text)),
                        new SqlParameter("@Description", txtDescription.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Medicine added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving medicine: " + ex.Message, "Error",
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

## 🔗 เชื่อมกับ Dashboard

```csharp
private void btnMedicines_Click(object sender, EventArgs e)
{
    MedicineListForm form = new MedicineListForm();
    form.ShowDialog();
}
```

---

## 🧪 ทดสอบ

1. Build (Ctrl + Shift + B)
2. Run (F5)
3. Dashboard → Medicines
4. ทดสอบ:
   - ✅ Add Medicine
   - ✅ Edit
   - ✅ Delete
   - ✅ Search
   - ✅ Filter by Category
   - ✅ ดู Stock Warning (สีแดง)

---

## 📊 สรุป

ได้:
- ✅ Medicine List Form
- ✅ Medicine Form Dialog
- ✅ CRUD Operations
- ✅ Search & Filter
- ✅ Auto Medicine Code
- ✅ Stock Management
- ✅ Low Stock Warning

**Medicine Management เสร็จสมบูรณ์!** 🎉

---

**พร้อมลงมือทำหรือยังครับ?** 🚀
