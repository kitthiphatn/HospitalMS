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
                        MedicineName,
                        Category,
                        Manufacturer,
                        UnitPrice,
                        StockQuantity,
                        ReorderLevel
                    FROM Medicines
                    WHERE IsActive = 1";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (MedicineName LIKE @Search 
                                OR Manufacturer LIKE @Search)";
                }

                if (category != "All")
                {
                    query += " AND Category = @Category";
                }

                query += " ORDER BY MedicineID";

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
                    dgvMedicines.Columns["MedicineName"].HeaderText = "Medicine Name";
                    dgvMedicines.Columns["MedicineName"].Width = 200;
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

            string medicineName = dgvMedicines.SelectedRows[0].Cells["MedicineName"].Value.ToString();
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
