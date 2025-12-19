using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;
using Hospitalms.UI.Forms.Patients;
using Hospitalms.UI.Forms.Appointments;
using Hospitalms.UI.Forms.Doctors;
using Hospitalms.UI.Forms.Medicines;  // เพิ่มบรรทัดนี้

namespace HospitalMS.UI.Forms.Dashboard
{
    public partial class DashboardForm : Form
    {
        // ประกาศตัวแปร
        private string _username;
        private string _fullName;
        private string _roleName;

        // Constructor แบบมี parameters
        public DashboardForm(string username, string fullName, string roleName)
        {
            InitializeComponent();
            _username = username;
            _fullName = fullName;
            _roleName = roleName;
        }

        // Constructor เดิม (สำหรับ Designer)
        public DashboardForm()
        {
            InitializeComponent();
            _username = "Guest";
            _fullName = "Guest User";
            _roleName = "Guest";
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // แสดงชื่อผู้ใช้
            lblWelcome.Text = $"Welcome, {_fullName} ({_roleName})";

            // ตั้งค่า Columns
            dgvTodayAppointments.Columns.Add("Time", "Time");
            dgvTodayAppointments.Columns.Add("PatientName", "Patient");
            dgvTodayAppointments.Columns.Add("DoctorName", "Doctor");
            dgvTodayAppointments.Columns.Add("Status", "Status");
            dgvTodayAppointments.Columns.Add("Reason", "Reason");

            // โหลดข้อมูล
            LoadStatistics();
            LoadTodayAppointments();
        }

        private void LoadStatistics()
        {
            try
            {
                // นับจำนวนผู้ป่วย
                string queryPatients = "SELECT COUNT(*) FROM Patients WHERE IsActive = 1";
                int patientsCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryPatients));
                lblPatientsCount.Text = patientsCount.ToString();

                // นับจำนวนหมอ
                string queryDoctors = "SELECT COUNT(*) FROM Doctors WHERE IsActive = 1";
                int doctorsCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryDoctors));
                lblDoctorsCount.Text = doctorsCount.ToString();

                // นับจำนวนนัดหมาย
                string queryAppointments = "SELECT COUNT(*) FROM Appointments";
                int appointmentsCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryAppointments));
                lblAppointmentsCount.Text = appointmentsCount.ToString();

                // นับจำนวนยา
                string queryMedicines = "SELECT COUNT(*) FROM Medicines WHERE IsActive = 1";
                int medicinesCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryMedicines));
                lblMedicinesCount.Text = medicinesCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading statistics: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTodayAppointments()
        {
            try
            {
                string query = @"
                    SELECT 
                        CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS Time,
                        p.FirstName + ' ' + p.LastName AS PatientName,
                        d.FirstName + ' ' + d.LastName AS DoctorName,
                        a.Status,
                        a.Reason
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientID = p.PatientID
                    INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                    WHERE CAST(a.AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
                    ORDER BY a.AppointmentTime";

                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                dgvTodayAppointments.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvTodayAppointments.Rows.Add(
                        row["Time"],
                        row["PatientName"],
                        row["DoctorName"],
                        row["Status"],
                        row["Reason"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // ว่างไว้
        }

        private void dgvTodayAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ว่างไว้
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            PatientListForm form = new PatientListForm();
            form.ShowDialog();
        }

        private void btnMedicines_Click(object sender, EventArgs e)
        {
            MedicineListForm form = new MedicineListForm();
            form.ShowDialog();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            Hospitalms.UI.Forms.Billing.InvoiceListForm invoiceForm = new Hospitalms.UI.Forms.Billing.InvoiceListForm();
            invoiceForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports - Coming Soon!", "Info");
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            AppointmentListForm form = new AppointmentListForm();
            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                // แสดง Login Form อีกครั้ง
                Application.Restart();
            }
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            DoctorListForm form = new DoctorListForm();
            form.ShowDialog();
        }
    }
}