using HospitalMS.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospitalms.UI.Forms.Patients
{
    public partial class PatientFormDialog : Form
    {
        private int? _patientId = null;
        private bool _isEditMode = false;

        // Constructor สำหรับ Add
        public PatientFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "Add Patient";
        }

        // Constructor สำหรับ Edit
        public PatientFormDialog(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            _isEditMode = true;
            this.Text = "Edit Patient";
        }

        private void PatientFormDialog_Load(object sender, EventArgs e)
        {
            // ตั้งค่า ComboBoxes
            cboGender.Items.AddRange(new string[] { "Male", "Female" });
            cboBloodGroup.Items.AddRange(new string[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" });

            if (_isEditMode && _patientId.HasValue)
            {
                LoadPatientData(_patientId.Value);
            }
            else
            {
                // Generate Patient Code
                txtPatientCode.Text = GeneratePatientCode();
            }
        }

        private string GeneratePatientCode()
        {
            try
            {
                string query = "SELECT TOP 1 PatientCode FROM Patients ORDER BY PatientID DESC";
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != null)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(1)) + 1;
                    return $"P{number:D7}";
                }
                else
                {
                    return "P0000001";
                }
            }
            catch
            {
                return "P0000001";
            }
        }

        private void LoadPatientData(int patientId)
        {
            try
            {
                string query = @"SELECT * FROM Patients WHERE PatientID = @PatientID";
                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patientId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtPatientCode.Text = row["PatientCode"].ToString();
                    txtPatientCode.ReadOnly = true;
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    dtpDateOfBirth.Value = Convert.ToDateTime(row["DateOfBirth"]);
                    cboGender.SelectedItem = row["Gender"].ToString();
                    cboBloodGroup.SelectedItem = row["BloodGroup"].ToString();
                    txtPhone.Text = row["Phone"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtAddress.Text = row["Address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Gender.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
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
                if (_isEditMode && _patientId.HasValue)
                {
                    // Update
                    string query = @"UPDATE Patients SET 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        DateOfBirth = @DateOfBirth,
                        Gender = @Gender,
                        BloodGroup = @BloodGroup,
                        Phone = @Phone,
                        Email = @Email,
                        Address = @Address,
                        ModifiedDate = GETDATE()
                        WHERE PatientID = @PatientID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", _patientId.Value),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@DateOfBirth", dtpDateOfBirth.Value),
                        new SqlParameter("@Gender", cboGender.SelectedItem.ToString()),
                        new SqlParameter("@BloodGroup", cboBloodGroup.SelectedItem?.ToString() ?? (object)DBNull.Value),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim()),
                        new SqlParameter("@Address", txtAddress.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Patient updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert
                    string query = @"INSERT INTO Patients 
                        (PatientCode, FirstName, LastName, DateOfBirth, Gender, BloodGroup, 
                         Phone, Email, Address, IsActive, CreatedDate)
                        VALUES 
                        (@PatientCode, @FirstName, @LastName, @DateOfBirth, @Gender, @BloodGroup,
                         @Phone, @Email, @Address, 1, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientCode", txtPatientCode.Text.Trim()),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@DateOfBirth", dtpDateOfBirth.Value),
                        new SqlParameter("@Gender", cboGender.SelectedItem.ToString()),
                        new SqlParameter("@BloodGroup", cboBloodGroup.SelectedItem?.ToString() ?? (object)DBNull.Value),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim()),
                        new SqlParameter("@Address", txtAddress.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Patient added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving patient: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblLastName_Click(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void cboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // ว่างไว้
        }
    }
}
