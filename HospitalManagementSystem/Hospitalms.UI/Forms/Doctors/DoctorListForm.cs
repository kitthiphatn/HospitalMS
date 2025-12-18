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
                    dgvDoctors.Columns["DoctorCode"].Width = 100;
                    dgvDoctors.Columns["FullName"].HeaderText = "Doctor Name";
                    dgvDoctors.Columns["FullName"].Width = 200;
                    dgvDoctors.Columns["Specialization"].HeaderText = "Specialization";
                    dgvDoctors.Columns["Specialization"].Width = 150;
                    dgvDoctors.Columns["LicenseNumber"].HeaderText = "License No.";
                    dgvDoctors.Columns["LicenseNumber"].Width = 120;
                    dgvDoctors.Columns["Phone"].HeaderText = "Phone";
                    dgvDoctors.Columns["Phone"].Width = 120;
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
                    MessageBox.Show("Doctor deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDoctors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dgvDoctors_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
