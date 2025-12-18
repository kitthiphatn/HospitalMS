using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Doctors
{
    public partial class DoctorFormDialog : Form
    {
        private int? _doctorId = null;
        private bool _isEditMode = false;

        public DoctorFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "Add Doctor";
        }

        public DoctorFormDialog(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;
            _isEditMode = true;
            this.Text = "Edit Doctor";
        }

        private void DoctorFormDialog_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Specialization
            cboSpecialization.Items.AddRange(new string[] { 
                "Cardiology", "Pediatrics", "Orthopedics", 
                "Neurology", "Dermatology", "General Practice" 
            });

            if (_isEditMode && _doctorId.HasValue)
            {
                LoadDoctorData(_doctorId.Value);
            }
            else
            {
                txtDoctorCode.Text = GenerateDoctorCode();
            }
        }

        private string GenerateDoctorCode()
        {
            try
            {
                string query = "SELECT TOP 1 DoctorCode FROM Doctors ORDER BY DoctorID DESC";
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != null)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(1)) + 1;
                    return $"D{number:D7}";
                }
                else
                {
                    return "D0000001";
                }
            }
            catch
            {
                return "D0000001";
            }
        }

        private void LoadDoctorData(int doctorId)
        {
            try
            {
                string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";
                SqlParameter[] parameters = {
                    new SqlParameter("@DoctorID", doctorId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtDoctorCode.Text = row["DoctorCode"].ToString();
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    cboSpecialization.SelectedItem = row["Specialization"].ToString();
                    txtLicenseNumber.Text = row["LicenseNumber"].ToString();
                    txtPhone.Text = row["Phone"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctor data: " + ex.Message, "Error",
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

            if (cboSpecialization.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Specialization.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSpecialization.Focus();
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
                if (_isEditMode && _doctorId.HasValue)
                {
                    // Update
                    string query = @"UPDATE Doctors SET 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Specialization = @Specialization,
                        LicenseNumber = @LicenseNumber,
                        Phone = @Phone,
                        Email = @Email,
                        ModifiedDate = GETDATE()
                        WHERE DoctorID = @DoctorID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@DoctorID", _doctorId.Value),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@Specialization", cboSpecialization.SelectedItem.ToString()),
                        new SqlParameter("@LicenseNumber", txtLicenseNumber.Text.Trim()),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Doctor updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Insert
                    string query = @"INSERT INTO Doctors 
                        (DoctorCode, FirstName, LastName, Specialization, LicenseNumber, 
                         Phone, Email, IsActive, CreatedDate)
                        VALUES 
                        (@DoctorCode, @FirstName, @LastName, @Specialization, @LicenseNumber,
                         @Phone, @Email, 1, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@DoctorCode", txtDoctorCode.Text.Trim()),
                        new SqlParameter("@FirstName", txtFirstName.Text.Trim()),
                        new SqlParameter("@LastName", txtLastName.Text.Trim()),
                        new SqlParameter("@Specialization", cboSpecialization.SelectedItem.ToString()),
                        new SqlParameter("@LicenseNumber", txtLicenseNumber.Text.Trim()),
                        new SqlParameter("@Phone", txtPhone.Text.Trim()),
                        new SqlParameter("@Email", txtEmail.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Doctor added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving doctor: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DoctorCode_Click(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // ว่างไว้
        }
    }
}
