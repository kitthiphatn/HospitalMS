namespace HospitalMS.UI.Forms.Dashboard
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelMedicinesStat = new System.Windows.Forms.Panel();
            this.lblMedicinesLabel = new System.Windows.Forms.Label();
            this.lblMedicinesCount = new System.Windows.Forms.Label();
            this.panelAppointmentsStat = new System.Windows.Forms.Panel();
            this.lblAppointmentsLabel = new System.Windows.Forms.Label();
            this.lblAppointmentsCount = new System.Windows.Forms.Label();
            this.panelDoctorsStat = new System.Windows.Forms.Panel();
            this.lblDoctorsLabel = new System.Windows.Forms.Label();
            this.lblDoctorsCount = new System.Windows.Forms.Label();
            this.panelPatientsStat = new System.Windows.Forms.Panel();
            this.lblPatientsLabel = new System.Windows.Forms.Label();
            this.lblPatientsCount = new System.Windows.Forms.Label();
            this.lblStatsTitle = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.Reports = new System.Windows.Forms.Button();
            this.Billing = new System.Windows.Forms.Button();
            this.Medicines = new System.Windows.Forms.Button();
            this.Appointments = new System.Windows.Forms.Button();
            this.Doctors = new System.Windows.Forms.Button();
            this.btnPatients = new System.Windows.Forms.Button();
            this.dgvTodayAppointments = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelMedicinesStat.SuspendLayout();
            this.panelAppointmentsStat.SuspendLayout();
            this.panelDoctorsStat.SuspendLayout();
            this.panelPatientsStat.SuspendLayout();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblWelcome);
            this.panelHeader.Controls.Add(this.btnLogout);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1184, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(342, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "🏥 Hospital Management System";
            this.lblTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(796, 21);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(192, 17);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome, System Administrator";
            this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Red;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1074, 15);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(80, 30);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelStats
            // 
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.panelMedicinesStat);
            this.panelStats.Controls.Add(this.panelAppointmentsStat);
            this.panelStats.Controls.Add(this.panelDoctorsStat);
            this.panelStats.Controls.Add(this.panelPatientsStat);
            this.panelStats.Controls.Add(this.lblStatsTitle);
            this.panelStats.Location = new System.Drawing.Point(20, 80);
            this.panelStats.Name = "panelStats";
            this.panelStats.AutoScroll = true;
            this.panelStats.Size = new System.Drawing.Size(1160, 120);
            this.panelStats.TabIndex = 3;
            this.panelStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // panelMedicinesStat
            // 
            this.panelMedicinesStat.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panelMedicinesStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMedicinesStat.Controls.Add(this.lblMedicinesLabel);
            this.panelMedicinesStat.Controls.Add(this.lblMedicinesCount);
            this.panelMedicinesStat.Location = new System.Drawing.Point(830, 35);
            this.panelMedicinesStat.Name = "panelMedicinesStat";
            this.panelMedicinesStat.Size = new System.Drawing.Size(250, 80);
            this.panelMedicinesStat.TabIndex = 8;
            // 
            // lblMedicinesLabel
            // 
            this.lblMedicinesLabel.AutoSize = true;
            this.lblMedicinesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMedicinesLabel.Location = new System.Drawing.Point(87, 52);
            this.lblMedicinesLabel.Name = "lblMedicinesLabel";
            this.lblMedicinesLabel.Size = new System.Drawing.Size(80, 20);
            this.lblMedicinesLabel.TabIndex = 1;
            this.lblMedicinesLabel.Text = "Medicines";
            // 
            // lblMedicinesCount
            // 
            this.lblMedicinesCount.AutoSize = true;
            this.lblMedicinesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMedicinesCount.Location = new System.Drawing.Point(100, 15);
            this.lblMedicinesCount.Name = "lblMedicinesCount";
            this.lblMedicinesCount.Size = new System.Drawing.Size(36, 37);
            this.lblMedicinesCount.TabIndex = 0;
            this.lblMedicinesCount.Text = "0";
            // 
            // panelAppointmentsStat
            // 
            this.panelAppointmentsStat.BackColor = System.Drawing.Color.LightCoral;
            this.panelAppointmentsStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAppointmentsStat.Controls.Add(this.lblAppointmentsLabel);
            this.panelAppointmentsStat.Controls.Add(this.lblAppointmentsCount);
            this.panelAppointmentsStat.Location = new System.Drawing.Point(560, 35);
            this.panelAppointmentsStat.Name = "panelAppointmentsStat";
            this.panelAppointmentsStat.Size = new System.Drawing.Size(250, 80);
            this.panelAppointmentsStat.TabIndex = 7;
            // 
            // lblAppointmentsLabel
            // 
            this.lblAppointmentsLabel.AutoSize = true;
            this.lblAppointmentsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAppointmentsLabel.Location = new System.Drawing.Point(71, 52);
            this.lblAppointmentsLabel.Name = "lblAppointmentsLabel";
            this.lblAppointmentsLabel.Size = new System.Drawing.Size(108, 20);
            this.lblAppointmentsLabel.TabIndex = 1;
            this.lblAppointmentsLabel.Text = "Appointments";
            // 
            // lblAppointmentsCount
            // 
            this.lblAppointmentsCount.AutoSize = true;
            this.lblAppointmentsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAppointmentsCount.Location = new System.Drawing.Point(100, 15);
            this.lblAppointmentsCount.Name = "lblAppointmentsCount";
            this.lblAppointmentsCount.Size = new System.Drawing.Size(36, 37);
            this.lblAppointmentsCount.TabIndex = 0;
            this.lblAppointmentsCount.Text = "0";
            // 
            // panelDoctorsStat
            // 
            this.panelDoctorsStat.BackColor = System.Drawing.Color.LightGreen;
            this.panelDoctorsStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDoctorsStat.Controls.Add(this.lblDoctorsLabel);
            this.panelDoctorsStat.Controls.Add(this.lblDoctorsCount);
            this.panelDoctorsStat.Location = new System.Drawing.Point(290, 35);
            this.panelDoctorsStat.Name = "panelDoctorsStat";
            this.panelDoctorsStat.Size = new System.Drawing.Size(250, 80);
            this.panelDoctorsStat.TabIndex = 6;
            // 
            // lblDoctorsLabel
            // 
            this.lblDoctorsLabel.AutoSize = true;
            this.lblDoctorsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDoctorsLabel.Location = new System.Drawing.Point(91, 52);
            this.lblDoctorsLabel.Name = "lblDoctorsLabel";
            this.lblDoctorsLabel.Size = new System.Drawing.Size(65, 20);
            this.lblDoctorsLabel.TabIndex = 1;
            this.lblDoctorsLabel.Text = "Doctors";
            // 
            // lblDoctorsCount
            // 
            this.lblDoctorsCount.AutoSize = true;
            this.lblDoctorsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDoctorsCount.Location = new System.Drawing.Point(100, 15);
            this.lblDoctorsCount.Name = "lblDoctorsCount";
            this.lblDoctorsCount.Size = new System.Drawing.Size(36, 37);
            this.lblDoctorsCount.TabIndex = 0;
            this.lblDoctorsCount.Text = "0";
            // 
            // panelPatientsStat
            // 
            this.panelPatientsStat.BackColor = System.Drawing.Color.LightBlue;
            this.panelPatientsStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPatientsStat.Controls.Add(this.lblPatientsLabel);
            this.panelPatientsStat.Controls.Add(this.lblPatientsCount);
            this.panelPatientsStat.Location = new System.Drawing.Point(20, 35);
            this.panelPatientsStat.Name = "panelPatientsStat";
            this.panelPatientsStat.Size = new System.Drawing.Size(250, 80);
            this.panelPatientsStat.TabIndex = 5;
            // 
            // lblPatientsLabel
            // 
            this.lblPatientsLabel.AutoSize = true;
            this.lblPatientsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPatientsLabel.Location = new System.Drawing.Point(90, 50);
            this.lblPatientsLabel.Name = "lblPatientsLabel";
            this.lblPatientsLabel.Size = new System.Drawing.Size(67, 20);
            this.lblPatientsLabel.TabIndex = 1;
            this.lblPatientsLabel.Text = "Patients";
            // 
            // lblPatientsCount
            // 
            this.lblPatientsCount.AutoSize = true;
            this.lblPatientsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPatientsCount.Location = new System.Drawing.Point(100, 15);
            this.lblPatientsCount.Name = "lblPatientsCount";
            this.lblPatientsCount.Size = new System.Drawing.Size(36, 37);
            this.lblPatientsCount.TabIndex = 0;
            this.lblPatientsCount.Text = "0";
            // 
            // lblStatsTitle
            // 
            this.lblStatsTitle.AutoSize = true;
            this.lblStatsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatsTitle.Location = new System.Drawing.Point(3, 0);
            this.lblStatsTitle.Name = "lblStatsTitle";
            this.lblStatsTitle.Size = new System.Drawing.Size(164, 21);
            this.lblStatsTitle.TabIndex = 4;
            this.lblStatsTitle.Text = "📊 System Statistics";
            // 
            // panelMenu
            // 
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.Reports);
            this.panelMenu.Controls.Add(this.Billing);
            this.panelMenu.Controls.Add(this.Medicines);
            this.panelMenu.Controls.Add(this.Appointments);
            this.panelMenu.Controls.Add(this.Doctors);
            this.panelMenu.Controls.Add(this.btnPatients);
            this.panelMenu.Location = new System.Drawing.Point(20, 220);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.AutoScroll = true;
            this.panelMenu.Size = new System.Drawing.Size(1160, 150);
            this.panelMenu.TabIndex = 4;
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // Reports
            // 
            this.Reports.BackColor = System.Drawing.Color.DodgerBlue;
            this.Reports.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Reports.ForeColor = System.Drawing.Color.White;
            this.Reports.Location = new System.Drawing.Point(870, 40);
            this.Reports.Name = "Reports";
            this.Reports.Size = new System.Drawing.Size(150, 60);
            this.Reports.TabIndex = 5;
            this.Reports.Text = "📊 Reports";
            this.Reports.UseVisualStyleBackColor = false;
            this.Reports.Click += new System.EventHandler(this.btnDailyRevenue_Click);
            // 
            // Billing
            // 
            this.Billing.BackColor = System.Drawing.Color.DodgerBlue;
            this.Billing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Billing.ForeColor = System.Drawing.Color.White;
            this.Billing.Location = new System.Drawing.Point(700, 40);
            this.Billing.Name = "Billing";
            this.Billing.Size = new System.Drawing.Size(150, 60);
            this.Billing.TabIndex = 4;
            this.Billing.Text = "💰 Billing";
            this.Billing.UseVisualStyleBackColor = false;
            this.Billing.Click += new System.EventHandler(this.btnBilling_Click);
            // 
            // Medicines
            // 
            this.Medicines.BackColor = System.Drawing.Color.DodgerBlue;
            this.Medicines.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Medicines.ForeColor = System.Drawing.Color.White;
            this.Medicines.Location = new System.Drawing.Point(530, 40);
            this.Medicines.Name = "Medicines";
            this.Medicines.Size = new System.Drawing.Size(150, 60);
            this.Medicines.TabIndex = 3;
            this.Medicines.Text = "💊 Medicines";
            this.Medicines.UseVisualStyleBackColor = false;
            this.Medicines.Click += new System.EventHandler(this.btnMedicines_Click);
            // 
            // Appointments
            // 
            this.Appointments.BackColor = System.Drawing.Color.DodgerBlue;
            this.Appointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Appointments.ForeColor = System.Drawing.Color.White;
            this.Appointments.Location = new System.Drawing.Point(360, 40);
            this.Appointments.Name = "Appointments";
            this.Appointments.Size = new System.Drawing.Size(150, 60);
            this.Appointments.TabIndex = 2;
            this.Appointments.Text = "👥 Appointments";
            this.Appointments.UseVisualStyleBackColor = false;
            this.Appointments.Click += new System.EventHandler(this.btnAppointments_Click);
            // 
            // Doctors
            // 
            this.Doctors.BackColor = System.Drawing.Color.DodgerBlue;
            this.Doctors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Doctors.ForeColor = System.Drawing.Color.White;
            this.Doctors.Location = new System.Drawing.Point(190, 40);
            this.Doctors.Name = "Doctors";
            this.Doctors.Size = new System.Drawing.Size(150, 60);
            this.Doctors.TabIndex = 1;
            this.Doctors.Text = "👥 Doctors";
            this.Doctors.UseVisualStyleBackColor = false;
            this.Doctors.Click += new System.EventHandler(this.btnDoctors_Click);
            // 
            // btnPatients
            // 
            this.btnPatients.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPatients.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPatients.ForeColor = System.Drawing.Color.White;
            this.btnPatients.Location = new System.Drawing.Point(20, 40);
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.Size = new System.Drawing.Size(150, 60);
            this.btnPatients.TabIndex = 0;
            this.btnPatients.Text = "👥 Patients";
            this.btnPatients.UseVisualStyleBackColor = false;
            this.btnPatients.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // dgvTodayAppointments
            // 
            this.dgvTodayAppointments.AllowUserToAddRows = false;
            this.dgvTodayAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTodayAppointments.Location = new System.Drawing.Point(20, 420);
            this.dgvTodayAppointments.Name = "dgvTodayAppointments";
            this.dgvTodayAppointments.ReadOnly = true;
            this.dgvTodayAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTodayAppointments.Size = new System.Drawing.Size(1160, 220);
            this.dgvTodayAppointments.TabIndex = 5;
            this.dgvTodayAppointments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTodayAppointments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTodayAppointments_CellContentClick);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.dgvTodayAppointments);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hospital Management System - Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.panelMedicinesStat.ResumeLayout(false);
            this.panelMedicinesStat.PerformLayout();
            this.panelAppointmentsStat.ResumeLayout(false);
            this.panelAppointmentsStat.PerformLayout();
            this.panelDoctorsStat.ResumeLayout(false);
            this.panelDoctorsStat.PerformLayout();
            this.panelPatientsStat.ResumeLayout(false);
            this.panelPatientsStat.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayAppointments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Panel panelPatientsStat;
        private System.Windows.Forms.Label lblPatientsCount;
        private System.Windows.Forms.Label lblPatientsLabel;
        private System.Windows.Forms.Panel panelMedicinesStat;
        private System.Windows.Forms.Label lblMedicinesLabel;
        private System.Windows.Forms.Label lblMedicinesCount;
        private System.Windows.Forms.Panel panelAppointmentsStat;
        private System.Windows.Forms.Label lblAppointmentsLabel;
        private System.Windows.Forms.Label lblAppointmentsCount;
        private System.Windows.Forms.Panel panelDoctorsStat;
        private System.Windows.Forms.Label lblDoctorsLabel;
        private System.Windows.Forms.Label lblDoctorsCount;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button Reports;
        private System.Windows.Forms.Button Billing;
        private System.Windows.Forms.Button Medicines;
        private System.Windows.Forms.Button Appointments;
        private System.Windows.Forms.Button Doctors;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.DataGridView dgvTodayAppointments;
    }
}