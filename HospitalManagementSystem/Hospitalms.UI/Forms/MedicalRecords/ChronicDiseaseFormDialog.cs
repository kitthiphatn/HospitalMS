using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.MedicalRecords
{
    public partial class ChronicDiseaseFormDialog : Form
    {
        private int _patientId;
        private int? _diseaseId;
        private bool _isEditMode;

        public ChronicDiseaseFormDialog(int patientId, int? diseaseId = null)
        {
            InitializeComponent();
            _patientId = patientId;
            _diseaseId = diseaseId;
            _isEditMode = diseaseId.HasValue;
        }

        private void ChronicDiseaseFormDialog_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                this.Text = "Edit Chronic Disease";
                LoadDiseaseData();
            }
            else
            {
                this.Text = "Add Chronic Disease";
                dtpDiagnosedDate.Value = DateTime.Now;
                cboSeverity.SelectedIndex = 0;
                cboStatus.SelectedIndex = 0;
            }
        }

        private void LoadDiseaseData()
        {
            try
            {
                string query = @"
                    SELECT 
                        DiseaseName,
                        DiagnosedDate,
                        Severity,
                        Status,
                        Notes
                    FROM ChronicDiseases
                    WHERE ChronicDiseaseID = @DiseaseID";

                SqlParameter[] parameters = {
                    new SqlParameter("@DiseaseID", _diseaseId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtDiseaseName.Text = row["DiseaseName"].ToString();
                    dtpDiagnosedDate.Value = Convert.ToDateTime(row["DiagnosedDate"]);
                    cboSeverity.SelectedItem = row["Severity"].ToString();
                    cboStatus.SelectedItem = row["Status"].ToString();
                    txtNotes.Text = row["Notes"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading disease data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtDiseaseName.Text))
            {
                MessageBox.Show("Please enter disease name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiseaseName.Focus();
                return false;
            }

            if (cboSeverity.SelectedIndex == -1)
            {
                MessageBox.Show("Please select severity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSeverity.Focus();
                return false;
            }

            if (cboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStatus.Focus();
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
                if (_isEditMode)
                {
                    // Update existing disease
                    string query = @"
                        UPDATE ChronicDiseases SET
                            DiseaseName = @DiseaseName,
                            DiagnosedDate = @DiagnosedDate,
                            Severity = @Severity,
                            Status = @Status,
                            Notes = @Notes
                        WHERE ChronicDiseaseID = @DiseaseID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@DiseaseName", txtDiseaseName.Text.Trim()),
                        new SqlParameter("@DiagnosedDate", dtpDiagnosedDate.Value),
                        new SqlParameter("@Severity", cboSeverity.SelectedItem.ToString()),
                        new SqlParameter("@Status", cboStatus.SelectedItem.ToString()),
                        new SqlParameter("@Notes", txtNotes.Text.Trim()),
                        new SqlParameter("@DiseaseID", _diseaseId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Chronic disease updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert new disease
                    string query = @"
                        INSERT INTO ChronicDiseases 
                        (PatientID, DiseaseName, DiagnosedDate, Severity, Status, Notes, IsActive)
                        VALUES 
                        (@PatientID, @DiseaseName, @DiagnosedDate, @Severity, @Status, @Notes, 1)";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", _patientId),
                        new SqlParameter("@DiseaseName", txtDiseaseName.Text.Trim()),
                        new SqlParameter("@DiagnosedDate", dtpDiagnosedDate.Value),
                        new SqlParameter("@Severity", cboSeverity.SelectedItem.ToString()),
                        new SqlParameter("@Status", cboStatus.SelectedItem.ToString()),
                        new SqlParameter("@Notes", txtNotes.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Chronic disease added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving disease: " + ex.Message, "Error",
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
