using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Appointments
{
    public partial class AppointmentListForm : Form
    {
        public AppointmentListForm()
        {
            InitializeComponent();
        }

        private void AppointmentListForm_Load(object sender, EventArgs e)
        {
            // ตั้งค่า Status Filter
            cboStatusFilter.Items.AddRange(new string[] { "All", "Pending", "Confirmed", "Completed", "Cancelled" });
            cboStatusFilter.SelectedIndex = 0; // All
            LoadAppointments();
        }

        private void LoadAppointments(string searchTerm = "", string statusFilter = "All")
        {
            try
            {
                string query = @"
                    SELECT 
                        a.AppointmentID,
                        CONVERT(VARCHAR(10), a.AppointmentDate, 103) AS AppointmentDate,
                        CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS AppointmentTime,
                        p.FirstName + ' ' + p.LastName AS PatientName,
                        d.FirstName + ' ' + d.LastName AS DoctorName,
                        a.Status,
                        a.Reason
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientID = p.PatientID
                    INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                    WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (p.FirstName LIKE @Search 
                                OR p.LastName LIKE @Search 
                                OR d.FirstName LIKE @Search 
                                OR d.LastName LIKE @Search)";
                }

                if (statusFilter != "All")
                {
                    query += " AND a.Status = @Status";
                }

                query += " ORDER BY a.AppointmentDate DESC, a.AppointmentTime DESC";

                SqlParameter[] parameters = null;
                var paramList = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    paramList.Add(new SqlParameter("@Search", $"%{searchTerm}%"));
                }

                if (statusFilter != "All")
                {
                    paramList.Add(new SqlParameter("@Status", statusFilter));
                }

                parameters = paramList.Count > 0 ? paramList.ToArray() : null;

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvAppointments.DataSource = dt;

                // ตั้งค่า Columns
                if (dgvAppointments.Columns.Count > 0)
                {
                    dgvAppointments.Columns["AppointmentID"].Visible = false;
                    dgvAppointments.Columns["AppointmentDate"].HeaderText = "Date";
                    dgvAppointments.Columns["AppointmentTime"].HeaderText = "Time";
                    dgvAppointments.Columns["PatientName"].HeaderText = "Patient";
                    dgvAppointments.Columns["DoctorName"].HeaderText = "Doctor";
                    dgvAppointments.Columns["Status"].HeaderText = "Status";
                    dgvAppointments.Columns["Reason"].HeaderText = "Reason";
                }

                this.Text = $"Appointment Management ({dt.Rows.Count} appointments)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string statusFilter = cboStatusFilter.SelectedItem != null ? cboStatusFilter.SelectedItem.ToString() : "All";
            LoadAppointments(txtSearch.Text.Trim(), statusFilter);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cboStatusFilter.Items.Count > 0)
            {
                cboStatusFilter.SelectedIndex = 0;
            }
            LoadAppointments();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AppointmentFormDialog form = new AppointmentFormDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadAppointments();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);
            AppointmentFormDialog form = new AppointmentFormDialog(appointmentId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadAppointments();
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Mark this appointment as Completed?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

                    string query = "UPDATE Appointments SET Status = 'Completed' WHERE AppointmentID = @AppointmentID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@AppointmentID", appointmentId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Appointment marked as Completed!", "Success");
                    LoadAppointments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }
        }

        private void cboStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatusFilter.SelectedItem != null)
            {
                LoadAppointments(txtSearch.Text.Trim(), cboStatusFilter.SelectedItem.ToString());
            }
        }

        private void dgvAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // ว่างไว้ - ลบได้ถ้าไม่ใช้
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ว่างไว้ - ลบได้ถ้าไม่ใช้
        }
    }
}