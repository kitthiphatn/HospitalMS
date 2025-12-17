using System;
using System.Windows.Forms;
using HospitalMS.DAL;
using HospitalMS.UI.Forms.Login; // Add this using directive

namespace HospitalMS.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // ทดสอบการเชื่อมต่อฐานข้อมูล
            if (DatabaseHelper.TestConnection())
            {
                MessageBox.Show("เชื่อมต่อฐานข้อมูลสำเร็จ!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("เชื่อมต่อฐานข้อมูลไม่สำเร็จ!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // เปิด LoginForm
            Application.Run(new LoginForm());
        }
    }
}