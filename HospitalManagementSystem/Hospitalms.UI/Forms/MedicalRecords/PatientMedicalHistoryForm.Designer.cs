namespace Hospitalms.UI.Forms.MedicalRecords
{
    partial class PatientMedicalHistoryForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMedicalRecords = new System.Windows.Forms.TabPage();
            this.panelRecordsButtons = new System.Windows.Forms.Panel();
            this.btnDeleteRecord = new System.Windows.Forms.Button();
            this.btnEditRecord = new System.Windows.Forms.Button();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.dgvMedicalRecords = new System.Windows.Forms.DataGridView();
            this.tabChronicDiseases = new System.Windows.Forms.TabPage();
            this.panelDiseasesButtons = new System.Windows.Forms.Panel();
            this.btnDeleteDisease = new System.Windows.Forms.Button();
            this.btnEditDisease = new System.Windows.Forms.Button();
            this.btnAddDisease = new System.Windows.Forms.Button();
            this.dgvChronicDiseases = new System.Windows.Forms.DataGridView();
            this.tabAllergies = new System.Windows.Forms.TabPage();
            this.panelAllergiesButtons = new System.Windows.Forms.Panel();
            this.btnDeleteAllergy = new System.Windows.Forms.Button();
            this.btnEditAllergy = new System.Windows.Forms.Button();
            this.btnAddAllergy = new System.Windows.Forms.Button();
            this.dgvAllergies = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMedicalRecords.SuspendLayout();
            this.panelRecordsButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).BeginInit();
            this.tabChronicDiseases.SuspendLayout();
            this.panelDiseasesButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChronicDiseases)).BeginInit();
            this.tabAllergies.SuspendLayout();
            this.panelAllergiesButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllergies)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelTop.Controls.Add(this.lblPatientName);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 80);
            this.panelTop.TabIndex = 0;
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.White;
            this.lblPatientName.Location = new System.Drawing.Point(20, 45);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(110, 21);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "Patient Name:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(307, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Patient Medical History";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabMedicalRecords);
            this.tabControl.Controls.Add(this.tabChronicDiseases);
            this.tabControl.Controls.Add(this.tabAllergies);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 80);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 520);
            this.tabControl.TabIndex = 1;
            // 
            // tabMedicalRecords
            // 
            this.tabMedicalRecords.Controls.Add(this.panelRecordsButtons);
            this.tabMedicalRecords.Controls.Add(this.dgvMedicalRecords);
            this.tabMedicalRecords.Location = new System.Drawing.Point(4, 26);
            this.tabMedicalRecords.Name = "tabMedicalRecords";
            this.tabMedicalRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabMedicalRecords.Size = new System.Drawing.Size(992, 490);
            this.tabMedicalRecords.TabIndex = 0;
            this.tabMedicalRecords.Text = "Medical Records";
            this.tabMedicalRecords.UseVisualStyleBackColor = true;
            // 
            // panelRecordsButtons
            // 
            this.panelRecordsButtons.Controls.Add(this.btnDeleteRecord);
            this.panelRecordsButtons.Controls.Add(this.btnEditRecord);
            this.panelRecordsButtons.Controls.Add(this.btnAddRecord);
            this.panelRecordsButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecordsButtons.Location = new System.Drawing.Point(3, 3);
            this.panelRecordsButtons.Name = "panelRecordsButtons";
            this.panelRecordsButtons.Size = new System.Drawing.Size(986, 50);
            this.panelRecordsButtons.TabIndex = 1;
            // 
            // btnDeleteRecord
            // 
            this.btnDeleteRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteRecord.ForeColor = System.Drawing.Color.White;
            this.btnDeleteRecord.Location = new System.Drawing.Point(220, 10);
            this.btnDeleteRecord.Name = "btnDeleteRecord";
            this.btnDeleteRecord.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteRecord.TabIndex = 2;
            this.btnDeleteRecord.Text = "Delete";
            this.btnDeleteRecord.UseVisualStyleBackColor = false;
            this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            // 
            // btnEditRecord
            // 
            this.btnEditRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnEditRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditRecord.ForeColor = System.Drawing.Color.White;
            this.btnEditRecord.Location = new System.Drawing.Point(115, 10);
            this.btnEditRecord.Name = "btnEditRecord";
            this.btnEditRecord.Size = new System.Drawing.Size(100, 35);
            this.btnEditRecord.TabIndex = 1;
            this.btnEditRecord.Text = "Edit";
            this.btnEditRecord.UseVisualStyleBackColor = false;
            this.btnEditRecord.Click += new System.EventHandler(this.btnEditRecord_Click);
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRecord.ForeColor = System.Drawing.Color.White;
            this.btnAddRecord.Location = new System.Drawing.Point(10, 10);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(100, 35);
            this.btnAddRecord.TabIndex = 0;
            this.btnAddRecord.Text = "Add New";
            this.btnAddRecord.UseVisualStyleBackColor = false;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // dgvMedicalRecords
            // 
            this.dgvMedicalRecords.AllowUserToAddRows = false;
            this.dgvMedicalRecords.AllowUserToDeleteRows = false;
            this.dgvMedicalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMedicalRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicalRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicalRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicalRecords.Location = new System.Drawing.Point(3, 56);
            this.dgvMedicalRecords.MultiSelect = false;
            this.dgvMedicalRecords.Name = "dgvMedicalRecords";
            this.dgvMedicalRecords.ReadOnly = true;
            this.dgvMedicalRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicalRecords.Size = new System.Drawing.Size(986, 431);
            this.dgvMedicalRecords.TabIndex = 0;
            // 
            // tabChronicDiseases
            // 
            this.tabChronicDiseases.Controls.Add(this.panelDiseasesButtons);
            this.tabChronicDiseases.Controls.Add(this.dgvChronicDiseases);
            this.tabChronicDiseases.Location = new System.Drawing.Point(4, 26);
            this.tabChronicDiseases.Name = "tabChronicDiseases";
            this.tabChronicDiseases.Padding = new System.Windows.Forms.Padding(3);
            this.tabChronicDiseases.Size = new System.Drawing.Size(992, 490);
            this.tabChronicDiseases.TabIndex = 1;
            this.tabChronicDiseases.Text = "Chronic Diseases";
            this.tabChronicDiseases.UseVisualStyleBackColor = true;
            // 
            // panelDiseasesButtons
            // 
            this.panelDiseasesButtons.Controls.Add(this.btnDeleteDisease);
            this.panelDiseasesButtons.Controls.Add(this.btnEditDisease);
            this.panelDiseasesButtons.Controls.Add(this.btnAddDisease);
            this.panelDiseasesButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDiseasesButtons.Location = new System.Drawing.Point(3, 3);
            this.panelDiseasesButtons.Name = "panelDiseasesButtons";
            this.panelDiseasesButtons.Size = new System.Drawing.Size(986, 50);
            this.panelDiseasesButtons.TabIndex = 1;
            // 
            // btnDeleteDisease
            // 
            this.btnDeleteDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteDisease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDisease.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDisease.Location = new System.Drawing.Point(220, 10);
            this.btnDeleteDisease.Name = "btnDeleteDisease";
            this.btnDeleteDisease.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteDisease.TabIndex = 2;
            this.btnDeleteDisease.Text = "Delete";
            this.btnDeleteDisease.UseVisualStyleBackColor = false;
            this.btnDeleteDisease.Click += new System.EventHandler(this.btnDeleteDisease_Click);
            // 
            // btnEditDisease
            // 
            this.btnEditDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnEditDisease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditDisease.ForeColor = System.Drawing.Color.White;
            this.btnEditDisease.Location = new System.Drawing.Point(115, 10);
            this.btnEditDisease.Name = "btnEditDisease";
            this.btnEditDisease.Size = new System.Drawing.Size(100, 35);
            this.btnEditDisease.TabIndex = 1;
            this.btnEditDisease.Text = "Edit";
            this.btnEditDisease.UseVisualStyleBackColor = false;
            this.btnEditDisease.Click += new System.EventHandler(this.btnEditDisease_Click);
            // 
            // btnAddDisease
            // 
            this.btnAddDisease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddDisease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDisease.ForeColor = System.Drawing.Color.White;
            this.btnAddDisease.Location = new System.Drawing.Point(10, 10);
            this.btnAddDisease.Name = "btnAddDisease";
            this.btnAddDisease.Size = new System.Drawing.Size(100, 35);
            this.btnAddDisease.TabIndex = 0;
            this.btnAddDisease.Text = "Add New";
            this.btnAddDisease.UseVisualStyleBackColor = false;
            this.btnAddDisease.Click += new System.EventHandler(this.btnAddDisease_Click);
            // 
            // dgvChronicDiseases
            // 
            this.dgvChronicDiseases.AllowUserToAddRows = false;
            this.dgvChronicDiseases.AllowUserToDeleteRows = false;
            this.dgvChronicDiseases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChronicDiseases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChronicDiseases.BackgroundColor = System.Drawing.Color.White;
            this.dgvChronicDiseases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChronicDiseases.Location = new System.Drawing.Point(3, 56);
            this.dgvChronicDiseases.MultiSelect = false;
            this.dgvChronicDiseases.Name = "dgvChronicDiseases";
            this.dgvChronicDiseases.ReadOnly = true;
            this.dgvChronicDiseases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChronicDiseases.Size = new System.Drawing.Size(986, 431);
            this.dgvChronicDiseases.TabIndex = 0;
            // 
            // tabAllergies
            // 
            this.tabAllergies.Controls.Add(this.panelAllergiesButtons);
            this.tabAllergies.Controls.Add(this.dgvAllergies);
            this.tabAllergies.Location = new System.Drawing.Point(4, 26);
            this.tabAllergies.Name = "tabAllergies";
            this.tabAllergies.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllergies.Size = new System.Drawing.Size(992, 490);
            this.tabAllergies.TabIndex = 2;
            this.tabAllergies.Text = "Allergies";
            this.tabAllergies.UseVisualStyleBackColor = true;
            // 
            // panelAllergiesButtons
            // 
            this.panelAllergiesButtons.Controls.Add(this.btnDeleteAllergy);
            this.panelAllergiesButtons.Controls.Add(this.btnEditAllergy);
            this.panelAllergiesButtons.Controls.Add(this.btnAddAllergy);
            this.panelAllergiesButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAllergiesButtons.Location = new System.Drawing.Point(3, 3);
            this.panelAllergiesButtons.Name = "panelAllergiesButtons";
            this.panelAllergiesButtons.Size = new System.Drawing.Size(986, 50);
            this.panelAllergiesButtons.TabIndex = 1;
            // 
            // btnDeleteAllergy
            // 
            this.btnDeleteAllergy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteAllergy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllergy.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAllergy.Location = new System.Drawing.Point(220, 10);
            this.btnDeleteAllergy.Name = "btnDeleteAllergy";
            this.btnDeleteAllergy.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteAllergy.TabIndex = 2;
            this.btnDeleteAllergy.Text = "Delete";
            this.btnDeleteAllergy.UseVisualStyleBackColor = false;
            this.btnDeleteAllergy.Click += new System.EventHandler(this.btnDeleteAllergy_Click);
            // 
            // btnEditAllergy
            // 
            this.btnEditAllergy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnEditAllergy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditAllergy.ForeColor = System.Drawing.Color.White;
            this.btnEditAllergy.Location = new System.Drawing.Point(115, 10);
            this.btnEditAllergy.Name = "btnEditAllergy";
            this.btnEditAllergy.Size = new System.Drawing.Size(100, 35);
            this.btnEditAllergy.TabIndex = 1;
            this.btnEditAllergy.Text = "Edit";
            this.btnEditAllergy.UseVisualStyleBackColor = false;
            this.btnEditAllergy.Click += new System.EventHandler(this.btnEditAllergy_Click);
            // 
            // btnAddAllergy
            // 
            this.btnAddAllergy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddAllergy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAllergy.ForeColor = System.Drawing.Color.White;
            this.btnAddAllergy.Location = new System.Drawing.Point(10, 10);
            this.btnAddAllergy.Name = "btnAddAllergy";
            this.btnAddAllergy.Size = new System.Drawing.Size(100, 35);
            this.btnAddAllergy.TabIndex = 0;
            this.btnAddAllergy.Text = "Add New";
            this.btnAddAllergy.UseVisualStyleBackColor = false;
            this.btnAddAllergy.Click += new System.EventHandler(this.btnAddAllergy_Click);
            // 
            // dgvAllergies
            // 
            this.dgvAllergies.AllowUserToAddRows = false;
            this.dgvAllergies.AllowUserToDeleteRows = false;
            this.dgvAllergies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAllergies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllergies.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllergies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllergies.Location = new System.Drawing.Point(3, 56);
            this.dgvAllergies.MultiSelect = false;
            this.dgvAllergies.Name = "dgvAllergies";
            this.dgvAllergies.ReadOnly = true;
            this.dgvAllergies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllergies.Size = new System.Drawing.Size(986, 431);
            this.dgvAllergies.TabIndex = 0;
            // 
            // PatientMedicalHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Name = "PatientMedicalHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Medical History";
            this.Load += new System.EventHandler(this.PatientMedicalHistoryForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabMedicalRecords.ResumeLayout(false);
            this.panelRecordsButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).EndInit();
            this.tabChronicDiseases.ResumeLayout(false);
            this.panelDiseasesButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChronicDiseases)).EndInit();
            this.tabAllergies.ResumeLayout(false);
            this.panelAllergiesButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllergies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMedicalRecords;
        private System.Windows.Forms.TabPage tabChronicDiseases;
        private System.Windows.Forms.TabPage tabAllergies;
        private System.Windows.Forms.DataGridView dgvMedicalRecords;
        private System.Windows.Forms.Panel panelRecordsButtons;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.Button btnEditRecord;
        private System.Windows.Forms.Button btnDeleteRecord;
        private System.Windows.Forms.DataGridView dgvChronicDiseases;
        private System.Windows.Forms.Panel panelDiseasesButtons;
        private System.Windows.Forms.Button btnAddDisease;
        private System.Windows.Forms.Button btnEditDisease;
        private System.Windows.Forms.Button btnDeleteDisease;
        private System.Windows.Forms.DataGridView dgvAllergies;
        private System.Windows.Forms.Panel panelAllergiesButtons;
        private System.Windows.Forms.Button btnAddAllergy;
        private System.Windows.Forms.Button btnEditAllergy;
        private System.Windows.Forms.Button btnDeleteAllergy;
    }
}
