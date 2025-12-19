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

        #region Medical Records Tab

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
                MessageBox.Show("Error loading medical records: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            MedicalRecordFormDialog dialog = new MedicalRecordFormDialog(_patientId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadMedicalRecords();
            }
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            if (dgvMedicalRecords.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int recordId = Convert.ToInt32(dgvMedicalRecords.SelectedRows[0].Cells["RecordID"].Value);
            MedicalRecordFormDialog dialog = new MedicalRecordFormDialog(_patientId, recordId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadMedicalRecords();
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            if (dgvMedicalRecords.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this medical record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int recordId = Convert.ToInt32(dgvMedicalRecords.SelectedRows[0].Cells["RecordID"].Value);

                    string query = "UPDATE MedicalRecords SET IsActive = 0 WHERE RecordID = @RecordID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@RecordID", recordId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Medical record deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMedicalRecords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Chronic Diseases Tab

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
                    dgvChronicDiseases.Columns["DiseaseName"].HeaderText = "Disease Name";
                    dgvChronicDiseases.Columns["DiagnosedDate"].HeaderText = "Diagnosed Date";
                    dgvChronicDiseases.Columns["Severity"].HeaderText = "Severity";
                    dgvChronicDiseases.Columns["Status"].HeaderText = "Status";
                    dgvChronicDiseases.Columns["Notes"].HeaderText = "Notes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chronic diseases: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDisease_Click(object sender, EventArgs e)
        {
            ChronicDiseaseFormDialog dialog = new ChronicDiseaseFormDialog(_patientId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadChronicDiseases();
            }
        }

        private void btnEditDisease_Click(object sender, EventArgs e)
        {
            if (dgvChronicDiseases.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a disease to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int diseaseId = Convert.ToInt32(dgvChronicDiseases.SelectedRows[0].Cells["ChronicDiseaseID"].Value);
            ChronicDiseaseFormDialog dialog = new ChronicDiseaseFormDialog(_patientId, diseaseId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadChronicDiseases();
            }
        }

        private void btnDeleteDisease_Click(object sender, EventArgs e)
        {
            if (dgvChronicDiseases.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a disease to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this chronic disease record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int diseaseId = Convert.ToInt32(dgvChronicDiseases.SelectedRows[0].Cells["ChronicDiseaseID"].Value);

                    string query = "UPDATE ChronicDiseases SET IsActive = 0 WHERE ChronicDiseaseID = @DiseaseID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@DiseaseID", diseaseId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Chronic disease record deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChronicDiseases();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting disease: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Allergies Tab

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
                    dgvAllergies.Columns["AllergyType"].HeaderText = "Type";
                    dgvAllergies.Columns["AllergyName"].HeaderText = "Allergy";
                    dgvAllergies.Columns["Reaction"].HeaderText = "Reaction";
                    dgvAllergies.Columns["Severity"].HeaderText = "Severity";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading allergies: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAllergy_Click(object sender, EventArgs e)
        {
            AllergyFormDialog dialog = new AllergyFormDialog(_patientId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadAllergies();
            }
        }

        private void btnEditAllergy_Click(object sender, EventArgs e)
        {
            if (dgvAllergies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an allergy to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int allergyId = Convert.ToInt32(dgvAllergies.SelectedRows[0].Cells["AllergyID"].Value);
            AllergyFormDialog dialog = new AllergyFormDialog(_patientId, allergyId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadAllergies();
            }
        }

        private void btnDeleteAllergy_Click(object sender, EventArgs e)
        {
            if (dgvAllergies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an allergy to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this allergy record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int allergyId = Convert.ToInt32(dgvAllergies.SelectedRows[0].Cells["AllergyID"].Value);

                    string query = "UPDATE Allergies SET IsActive = 0 WHERE AllergyID = @AllergyID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@AllergyID", allergyId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Allergy record deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllergies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting allergy: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void dgvMedicalRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }

        private void dgvChronicDiseases_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }

        private void dgvAllergies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }
    }
}
