using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.MedicalRecords
{
    public partial class MedicalRecordFormDialog : Form
    {
        private int _patientId;
        private int? _recordId;
        private bool _isEditMode;

        public MedicalRecordFormDialog(int patientId, int? recordId = null)
        {
            InitializeComponent();
            _patientId = patientId;
            _recordId = recordId;
            _isEditMode = recordId.HasValue;
        }

        private void MedicalRecordFormDialog_Load(object sender, EventArgs e)
        {
            LoadDoctors();

            if (_isEditMode)
            {
                this.Text = "Edit Medical Record";
                LoadRecordData();
            }
            else
            {
                this.Text = "Add Medical Record";
                dtpVisitDate.Value = DateTime.Now;
            }
        }

        private void LoadDoctors()
        {
            try
            {
                string query = @"
                    SELECT 
                        DoctorID,
                        FirstName + ' ' + LastName + ' (' + Specialization + ')' AS DoctorName
                    FROM Doctors
                    WHERE IsActive = 1
                    ORDER BY FirstName, LastName";

                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                cboDoctor.DataSource = dt;
                cboDoctor.DisplayMember = "DoctorName";
                cboDoctor.ValueMember = "DoctorID";
                cboDoctor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctors: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecordData()
        {
            try
            {
                string query = @"
                    SELECT 
                        VisitDate,
                        DoctorID,
                        ChiefComplaint,
                        Diagnosis,
                        Prescription,
                        Notes
                    FROM MedicalRecords
                    WHERE RecordID = @RecordID";

                SqlParameter[] parameters = {
                    new SqlParameter("@RecordID", _recordId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    dtpVisitDate.Value = Convert.ToDateTime(row["VisitDate"]);
                    cboDoctor.SelectedValue = row["DoctorID"];
                    txtChiefComplaint.Text = row["ChiefComplaint"].ToString();
                    txtDiagnosis.Text = row["Diagnosis"].ToString();
                    txtPrescription.Text = row["Prescription"].ToString();
                    txtNotes.Text = row["Notes"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading record data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cboDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a doctor.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDoctor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChiefComplaint.Text))
            {
                MessageBox.Show("Please enter chief complaint.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChiefComplaint.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Please enter diagnosis.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiagnosis.Focus();
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
                    // Update existing record
                    string query = @"
                        UPDATE MedicalRecords SET
                            VisitDate = @VisitDate,
                            DoctorID = @DoctorID,
                            ChiefComplaint = @ChiefComplaint,
                            Diagnosis = @Diagnosis,
                            Prescription = @Prescription,
                            Notes = @Notes
                        WHERE RecordID = @RecordID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@VisitDate", dtpVisitDate.Value),
                        new SqlParameter("@DoctorID", cboDoctor.SelectedValue),
                        new SqlParameter("@ChiefComplaint", txtChiefComplaint.Text.Trim()),
                        new SqlParameter("@Diagnosis", txtDiagnosis.Text.Trim()),
                        new SqlParameter("@Prescription", txtPrescription.Text.Trim()),
                        new SqlParameter("@Notes", txtNotes.Text.Trim()),
                        new SqlParameter("@RecordID", _recordId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Medical record updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert new record
                    string query = @"
                        INSERT INTO MedicalRecords 
                        (PatientID, VisitDate, DoctorID, ChiefComplaint, Diagnosis, Prescription, Notes, IsActive)
                        VALUES 
                        (@PatientID, @VisitDate, @DoctorID, @ChiefComplaint, @Diagnosis, @Prescription, @Notes, 1)";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", _patientId),
                        new SqlParameter("@VisitDate", dtpVisitDate.Value),
                        new SqlParameter("@DoctorID", cboDoctor.SelectedValue),
                        new SqlParameter("@ChiefComplaint", txtChiefComplaint.Text.Trim()),
                        new SqlParameter("@Diagnosis", txtDiagnosis.Text.Trim()),
                        new SqlParameter("@Prescription", txtPrescription.Text.Trim()),
                        new SqlParameter("@Notes", txtNotes.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Medical record added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error",
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
