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
using HospitalMS.DAL;
using HospitalMS.UI.Forms.Dashboard;

namespace HospitalMS.UI.Forms.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ากรอกข้อมูลครบหรือยัง
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("กรุณากรอก Username", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("กรุณากรอก Password", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                // ตรวจสอบ Login กับฐานข้อมูล
                string query = @"SELECT u.UserID, u.Username, u.FullName, r.RoleName 
                        FROM Users u 
                        INNER JOIN Roles r ON u.RoleID = r.RoleID 
                        WHERE u.Username = @Username 
                        AND u.PasswordHash = @Password 
                        AND u.IsActive = 1";

                SqlParameter[] parameters = {
            new SqlParameter("@Username", txtUsername.Text.Trim()),
            new SqlParameter("@Password", txtPassword.Text)
        };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    // Login สำเร็จ
                    DataRow user = dt.Rows[0];
                    string username = user["Username"].ToString();
                    string fullName = user["FullName"].ToString();
                    string roleName = user["RoleName"].ToString();

                    // ซ่อน Login Form
                    this.Hide();

                    // เปิด Dashboard
                    DashboardForm dashboard = new DashboardForm(username, fullName, roleName);
                    dashboard.FormClosed += (s, args) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    // Login ไม่สำเร็จ
                    MessageBox.Show("Invalid Username or Password!", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
