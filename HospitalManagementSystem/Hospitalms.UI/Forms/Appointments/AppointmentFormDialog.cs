using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Appointments
{
    public partial class AppointmentFormDialog : Form
    {
        private int? _appointmentId = null;
        private bool _isEditMode = false;

        public AppointmentFormDialog()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Text = "New Appointment";
        }

        public AppointmentFormDialog(int appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _isEditMode = true;
            this.Text = "Edit Appointment";
        }

        private void AppointmentFormDialog_Load(object sender, EventArgs e)
        {
            LoadPatients();
            LoadDoctors();

            cboStatus.Items.AddRange(new string[] { "Pending", "Confirmed", "Completed", "Cancelled" });
            cboStatus.SelectedIndex = 0;

            if (_isEditMode && _appointmentId.HasValue)
            {
                LoadAppointmentData(_appointmentId.Value);
            }
        }

        private void LoadPatients()
        {
            try
            {
                string query = "SELECT PatientID, FirstName + ' ' + LastName AS FullName FROM Patients WHERE IsActive = 1 ORDER BY FirstName";
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                cboPatient.DisplayMember = "FullName";
                cboPatient.ValueMember = "PatientID";
                cboPatient.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message);
            }
        }

        private void LoadDoctors()
        {
            try
            {
                string query = "SELECT DoctorID, FirstName + ' ' + LastName AS FullName FROM Doctors WHERE IsActive = 1 ORDER BY FirstName";
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                cboDoctor.DisplayMember = "FullName";
                cboDoctor.ValueMember = "DoctorID";
                cboDoctor.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctors: " + ex.Message);
            }
        }

        private void LoadAppointmentData(int appointmentId)
        {
            try
            {
                string query = "SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID";
                SqlParameter[] parameters = { new SqlParameter("@AppointmentID", appointmentId) };
                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cboPatient.SelectedValue = row["PatientID"];
                    cboDoctor.SelectedValue = row["DoctorID"];
                    dtpDate.Value = Convert.ToDateTime(row["AppointmentDate"]);
                    dtpTime.Value = DateTime.Today.Add((TimeSpan)row["AppointmentTime"]);
                    cboStatus.SelectedItem = row["Status"].ToString();
                    txtReason.Text = row["Reason"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboPatient.SelectedIndex == -1 || cboDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Patient and Doctor.");
                return;
            }

            try
            {
                if (_isEditMode)
                {
                    string query = @"UPDATE Appointments SET 
                        PatientID = @PatientID,
                        DoctorID = @DoctorID,
                        AppointmentDate = @Date,
                        AppointmentTime = @Time,
                        Status = @Status,
                        Reason = @Reason
                        WHERE AppointmentID = @AppointmentID";

                    SqlParameter[] parameters = {
                        new SqlParameter("@AppointmentID", _appointmentId.Value),
                        new SqlParameter("@PatientID", cboPatient.SelectedValue),
                        new SqlParameter("@DoctorID", cboDoctor.SelectedValue),
                        new SqlParameter("@Date", dtpDate.Value.Date),
                        new SqlParameter("@Time", dtpTime.Value.TimeOfDay),
                        new SqlParameter("@Status", cboStatus.SelectedItem.ToString()),
                        new SqlParameter("@Reason", txtReason.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Appointment updated!");
                }
                else
                {
                    string query = @"INSERT INTO Appointments 
                        (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, CreatedDate)
                        VALUES (@PatientID, @DoctorID, @Date, @Time, @Status, @Reason, GETDATE())";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PatientID", cboPatient.SelectedValue),
                        new SqlParameter("@DoctorID", cboDoctor.SelectedValue),
                        new SqlParameter("@Date", dtpDate.Value.Date),
                        new SqlParameter("@Time", dtpTime.Value.TimeOfDay),
                        new SqlParameter("@Status", cboStatus.SelectedItem.ToString()),
                        new SqlParameter("@Reason", txtReason.Text.Trim())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Appointment created!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cboPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void cboDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void dtpTime_ValueChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // ว่างไว้
        }
    }
}
