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
                txtMedicineCode.Text = "Auto Generated";
                txtMedicineCode.ReadOnly = true;
                txtStockQuantity.Text = "0";
                txtReorderLevel.Text = "10";
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
                    txtMedicineCode.Text = row["MedicineID"].ToString();
                    txtName.Text = row["MedicineName"].ToString();
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
                        MedicineName = @Name,
                        Category = @Category,
                        Manufacturer = @Manufacturer,
                        UnitPrice = @UnitPrice,
                        StockQuantity = @StockQuantity,
                        ReorderLevel = @ReorderLevel,
                        Description = @Description
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
                        (MedicineName, Category, Manufacturer, UnitPrice, 
                         StockQuantity, ReorderLevel, Description, IsActive, CreatedDate)
                        VALUES 
                        (@Name, @Category, @Manufacturer, @UnitPrice,
                         @StockQuantity, @ReorderLevel, @Description, 1, GETDATE())";

                    SqlParameter[] parameters = {
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

        private void txtMedicineCode_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtManufacturer_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtStockQuantity_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtReorderLevel_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }
    }
}
