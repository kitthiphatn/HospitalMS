using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;
using Hospitalms.UI.Forms.MedicalRecords;

namespace Hospitalms.UI.Forms.Patients
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

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }

        private void btnMedicalHistory_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to view medical history.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["PatientID"].Value);
                string patientName = dgvPatients.SelectedRows[0].Cells["FullName"].Value.ToString();

                PatientMedicalHistoryForm form = new PatientMedicalHistoryForm(patientId, patientName);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening medical history: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PatientListForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
