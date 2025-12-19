namespace Hospitalms.UI.Forms.MedicalRecords
{
    partial class ChronicDiseaseFormDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblDiseaseName = new System.Windows.Forms.Label();
            this.txtDiseaseName = new System.Windows.Forms.TextBox();
            this.lblDiagnosedDate = new System.Windows.Forms.Label();
            this.dtpDiagnosedDate = new System.Windows.Forms.DateTimePicker();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.cboSeverity = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDiseaseName
            // 
            this.lblDiseaseName.AutoSize = true;
            this.lblDiseaseName.Location = new System.Drawing.Point(30, 30);
            this.lblDiseaseName.Name = "lblDiseaseName";
            this.lblDiseaseName.Size = new System.Drawing.Size(79, 13);
            this.lblDiseaseName.TabIndex = 0;
            this.lblDiseaseName.Text = "Disease Name:";
            // 
            // txtDiseaseName
            // 
            this.txtDiseaseName.Location = new System.Drawing.Point(150, 27);
            this.txtDiseaseName.Name = "txtDiseaseName";
            this.txtDiseaseName.Size = new System.Drawing.Size(350, 20);
            this.txtDiseaseName.TabIndex = 1;
            // 
            // lblDiagnosedDate
            // 
            this.lblDiagnosedDate.AutoSize = true;
            this.lblDiagnosedDate.Location = new System.Drawing.Point(30, 65);
            this.lblDiagnosedDate.Name = "lblDiagnosedDate";
            this.lblDiagnosedDate.Size = new System.Drawing.Size(89, 13);
            this.lblDiagnosedDate.TabIndex = 2;
            this.lblDiagnosedDate.Text = "Diagnosed Date:";
            // 
            // dtpDiagnosedDate
            // 
            this.dtpDiagnosedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDiagnosedDate.Location = new System.Drawing.Point(150, 62);
            this.dtpDiagnosedDate.Name = "dtpDiagnosedDate";
            this.dtpDiagnosedDate.Size = new System.Drawing.Size(350, 20);
            this.dtpDiagnosedDate.TabIndex = 3;
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(30, 100);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(48, 13);
            this.lblSeverity.TabIndex = 4;
            this.lblSeverity.Text = "Severity:";
            // 
            // cboSeverity
            // 
            this.cboSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeverity.FormattingEnabled = true;
            this.cboSeverity.Items.AddRange(new object[] {
            "Mild",
            "Moderate",
            "Severe"});
            this.cboSeverity.Location = new System.Drawing.Point(150, 97);
            this.cboSeverity.Name = "cboSeverity";
            this.cboSeverity.Size = new System.Drawing.Size(350, 21);
            this.cboSeverity.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(30, 135);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Active",
            "Under Control",
            "In Remission",
            "Cured"});
            this.cboStatus.Location = new System.Drawing.Point(150, 132);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(350, 21);
            this.cboStatus.TabIndex = 7;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(30, 170);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 13);
            this.lblNotes.TabIndex = 8;
            this.lblNotes.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(150, 167);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(350, 80);
            this.txtNotes.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(290, 270);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(400, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChronicDiseaseFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboSeverity);
            this.Controls.Add(this.lblSeverity);
            this.Controls.Add(this.dtpDiagnosedDate);
            this.Controls.Add(this.lblDiagnosedDate);
            this.Controls.Add(this.txtDiseaseName);
            this.Controls.Add(this.lblDiseaseName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChronicDiseaseFormDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chronic Disease";
            this.Load += new System.EventHandler(this.ChronicDiseaseFormDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDiseaseName;
        private System.Windows.Forms.TextBox txtDiseaseName;
        private System.Windows.Forms.Label lblDiagnosedDate;
        private System.Windows.Forms.DateTimePicker dtpDiagnosedDate;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.ComboBox cboSeverity;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
