namespace Hospitalms.UI.Forms.Doctors
{
    partial class DoctorFormDialog
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
            this.txtDoctorCode = new System.Windows.Forms.TextBox();
            this.DoctorCode = new System.Windows.Forms.Label();
            this.Fristname = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.cboSpecialization = new System.Windows.Forms.ComboBox();
            this.Specialization = new System.Windows.Forms.Label();
            this.LicenseNumber = new System.Windows.Forms.Label();
            this.txtLicenseNumber = new System.Windows.Forms.TextBox();
            this.Phone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDoctorCode
            // 
            this.txtDoctorCode.BackColor = System.Drawing.Color.White;
            this.txtDoctorCode.Location = new System.Drawing.Point(101, 31);
            this.txtDoctorCode.Name = "txtDoctorCode";
            this.txtDoctorCode.Size = new System.Drawing.Size(250, 20);
            this.txtDoctorCode.TabIndex = 0;
            // 
            // DoctorCode
            // 
            this.DoctorCode.AutoSize = true;
            this.DoctorCode.Location = new System.Drawing.Point(25, 34);
            this.DoctorCode.Name = "DoctorCode";
            this.DoctorCode.Size = new System.Drawing.Size(70, 13);
            this.DoctorCode.TabIndex = 1;
            this.DoctorCode.Text = "DoctorCode :";
            this.DoctorCode.Click += new System.EventHandler(this.DoctorCode_Click);
            // 
            // Fristname
            // 
            this.Fristname.AutoSize = true;
            this.Fristname.Location = new System.Drawing.Point(25, 74);
            this.Fristname.Name = "Fristname";
            this.Fristname.Size = new System.Drawing.Size(58, 13);
            this.Fristname.TabIndex = 2;
            this.Fristname.Text = "Fristname :";
            // 
            // LastName
            // 
            this.LastName.AutoSize = true;
            this.LastName.Location = new System.Drawing.Point(261, 77);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(59, 13);
            this.LastName.TabIndex = 3;
            this.LastName.Text = "Lastname :";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.White;
            this.txtFirstName.Location = new System.Drawing.Point(101, 70);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(150, 20);
            this.txtFirstName.TabIndex = 4;
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.White;
            this.txtLastName.Location = new System.Drawing.Point(326, 74);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(150, 20);
            this.txtLastName.TabIndex = 5;
            // 
            // cboSpecialization
            // 
            this.cboSpecialization.FormattingEnabled = true;
            this.cboSpecialization.Location = new System.Drawing.Point(101, 111);
            this.cboSpecialization.Name = "cboSpecialization";
            this.cboSpecialization.Size = new System.Drawing.Size(150, 21);
            this.cboSpecialization.TabIndex = 6;
            // 
            // Specialization
            // 
            this.Specialization.AutoSize = true;
            this.Specialization.Location = new System.Drawing.Point(17, 114);
            this.Specialization.Name = "Specialization";
            this.Specialization.Size = new System.Drawing.Size(78, 13);
            this.Specialization.TabIndex = 7;
            this.Specialization.Text = "Specialization :";
            // 
            // LicenseNumber
            // 
            this.LicenseNumber.AutoSize = true;
            this.LicenseNumber.Location = new System.Drawing.Point(12, 153);
            this.LicenseNumber.Name = "LicenseNumber";
            this.LicenseNumber.Size = new System.Drawing.Size(87, 13);
            this.LicenseNumber.TabIndex = 8;
            this.LicenseNumber.Text = "License Number:";
            this.LicenseNumber.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtLicenseNumber
            // 
            this.txtLicenseNumber.Location = new System.Drawing.Point(101, 150);
            this.txtLicenseNumber.Name = "txtLicenseNumber";
            this.txtLicenseNumber.Size = new System.Drawing.Size(150, 20);
            this.txtLicenseNumber.TabIndex = 9;
            // 
            // Phone
            // 
            this.Phone.AutoSize = true;
            this.Phone.Location = new System.Drawing.Point(261, 116);
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(44, 13);
            this.Phone.TabIndex = 10;
            this.Phone.Text = "Phone :";
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(326, 114);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 20);
            this.txtPhone.TabIndex = 11;
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(261, 153);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(38, 13);
            this.Email.TabIndex = 12;
            this.Email.Text = "Email :";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(326, 151);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 20);
            this.txtEmail.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(101, 223);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 60);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(289, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 60);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DoctorFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.Phone);
            this.Controls.Add(this.txtLicenseNumber);
            this.Controls.Add(this.LicenseNumber);
            this.Controls.Add(this.Specialization);
            this.Controls.Add(this.cboSpecialization);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.Fristname);
            this.Controls.Add(this.DoctorCode);
            this.Controls.Add(this.txtDoctorCode);
            this.Name = "DoctorFormDialog";
            this.Text = "DoctorFormDialog";
            this.Load += new System.EventHandler(this.DoctorFormDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDoctorCode;
        private System.Windows.Forms.Label DoctorCode;
        private System.Windows.Forms.Label Fristname;
        private System.Windows.Forms.Label LastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.ComboBox cboSpecialization;
        private System.Windows.Forms.Label Specialization;
        private System.Windows.Forms.Label LicenseNumber;
        private System.Windows.Forms.TextBox txtLicenseNumber;
        private System.Windows.Forms.Label Phone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}