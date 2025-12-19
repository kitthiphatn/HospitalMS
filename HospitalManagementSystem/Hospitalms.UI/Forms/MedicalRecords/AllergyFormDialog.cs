using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.MedicalRecords
{
    public partial class AllergyFormDialog : Form
    {
        private int _patientId;
        private int? _allergyId;
        private bool _isEditMode;

        public AllergyFormDialog(int patientId, int? allergyId = null)
        {
            InitializeComponent();
            _patientId = patientId;
            _allergyId = allergyId;
            _isEditMode = allergyId.HasValue;
        }

        private void AllergyFormDialog_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                this.Text = "Edit Allergy";
                LoadAllergyData();
            }
            else
            {
                this.Text = "Add Allergy";
                cboAllergyType.SelectedIndex = 0;
                cboSeverity.SelectedIndex = 0;
            }
        }

        private void LoadAllergyData()
        {
            try
            {
                string query = @"
                    SELECT 
                        AllergyType,
                        AllergyName,
                        Reaction,
                        Severity
                    FROM Allergies
                    WHERE AllergyID = @AllergyID";

                SqlParameter[] parameters = {
                    new SqlParameter("@AllergyID", _allergyId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cboAllergyType.SelectedItem = row["AllergyType"].ToString();
                    txtAllergyName.Text = row["AllergyName"].ToString();
                    txtReaction.Text = row["Reaction"].ToString();
                    cboSeverity.SelectedItem = row["Severity"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading allergy data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cboAllergyType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select allergy type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboAllergyType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAllergyName.Text))
            {
                MessageBox.Show("Please enter allergy name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAllergyName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReaction.Text))
            {
                MessageBox.Show("Please enter reaction.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaction.Focus();
                return false;
            }

            if (cboSeverity.SelectedIndex == -1)
            {
                MessageBox.Show("Please select severity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSeverity.Focus();
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
                    // Update existing allergy
                    string query = @"
                        UPDATE Allergies SET
                            AllergyType = @AllergyType,
                            AllergyName = @AllergyName,
                            Reaction = @Reaction,
                            Severity = @Severity
                        WHERE AllergyID = @AllergyID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@AllergyType", cboAllergyType.SelectedItem.ToString()),
                        new SqlParameter("@AllergyName", txtAllergyName.Text.Trim()),
                        new SqlParameter("@Reaction", txtReaction.Text.Trim()),
                        new SqlParameter("@Severity", cboSeverity.SelectedItem.ToString()),
                        new SqlParameter("@AllergyID", _allergyId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Allergy updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert new allergy
                    string query = @"
                        INSERT INTO Allergies 
                        (PatientID, AllergyType, AllergyName, Reaction, Severity, IsActive)
                        VALUES 
                        (@PatientID, @AllergyType, @AllergyName, @Reaction, @Severity, 1)";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", _patientId),
                        new SqlParameter("@AllergyType", cboAllergyType.SelectedItem.ToString()),
                        new SqlParameter("@AllergyName", txtAllergyName.Text.Trim()),
                        new SqlParameter("@Reaction", txtReaction.Text.Trim()),
                        new SqlParameter("@Severity", cboSeverity.SelectedItem.ToString())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Allergy added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving allergy: " + ex.Message, "Error",
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
