namespace Hospitalms.UI.Forms.MedicalRecords
{
    partial class AllergyFormDialog
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
            this.lblAllergyType = new System.Windows.Forms.Label();
            this.cboAllergyType = new System.Windows.Forms.ComboBox();
            this.lblAllergyName = new System.Windows.Forms.Label();
            this.txtAllergyName = new System.Windows.Forms.TextBox();
            this.lblReaction = new System.Windows.Forms.Label();
            this.txtReaction = new System.Windows.Forms.TextBox();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.cboSeverity = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAllergyType
            // 
            this.lblAllergyType.AutoSize = true;
            this.lblAllergyType.Location = new System.Drawing.Point(30, 30);
            this.lblAllergyType.Name = "lblAllergyType";
            this.lblAllergyType.Size = new System.Drawing.Size(71, 13);
            this.lblAllergyType.TabIndex = 0;
            this.lblAllergyType.Text = "Allergy Type:";
            // 
            // cboAllergyType
            // 
            this.cboAllergyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAllergyType.FormattingEnabled = true;
            this.cboAllergyType.Items.AddRange(new object[] {
            "Drug",
            "Food",
            "Environmental",
            "Other"});
            this.cboAllergyType.Location = new System.Drawing.Point(150, 27);
            this.cboAllergyType.Name = "cboAllergyType";
            this.cboAllergyType.Size = new System.Drawing.Size(350, 21);
            this.cboAllergyType.TabIndex = 1;
            // 
            // lblAllergyName
            // 
            this.lblAllergyName.AutoSize = true;
            this.lblAllergyName.Location = new System.Drawing.Point(30, 65);
            this.lblAllergyName.Name = "lblAllergyName";
            this.lblAllergyName.Size = new System.Drawing.Size(76, 13);
            this.lblAllergyName.TabIndex = 2;
            this.lblAllergyName.Text = "Allergy Name:";
            // 
            // txtAllergyName
            // 
            this.txtAllergyName.Location = new System.Drawing.Point(150, 62);
            this.txtAllergyName.Name = "txtAllergyName";
            this.txtAllergyName.Size = new System.Drawing.Size(350, 20);
            this.txtAllergyName.TabIndex = 3;
            // 
            // lblReaction
            // 
            this.lblReaction.AutoSize = true;
            this.lblReaction.Location = new System.Drawing.Point(30, 100);
            this.lblReaction.Name = "lblReaction";
            this.lblReaction.Size = new System.Drawing.Size(54, 13);
            this.lblReaction.TabIndex = 4;
            this.lblReaction.Text = "Reaction:";
            // 
            // txtReaction
            // 
            this.txtReaction.Location = new System.Drawing.Point(150, 97);
            this.txtReaction.Multiline = true;
            this.txtReaction.Name = "txtReaction";
            this.txtReaction.Size = new System.Drawing.Size(350, 60);
            this.txtReaction.TabIndex = 5;
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(30, 170);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(48, 13);
            this.lblSeverity.TabIndex = 6;
            this.lblSeverity.Text = "Severity:";
            // 
            // cboSeverity
            // 
            this.cboSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeverity.FormattingEnabled = true;
            this.cboSeverity.Items.AddRange(new object[] {
            "Mild",
            "Moderate",
            "Severe",
            "Life-threatening"});
            this.cboSeverity.Location = new System.Drawing.Point(150, 167);
            this.cboSeverity.Name = "cboSeverity";
            this.cboSeverity.Size = new System.Drawing.Size(350, 21);
            this.cboSeverity.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(290, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(400, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AllergyFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 271);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboSeverity);
            this.Controls.Add(this.lblSeverity);
            this.Controls.Add(this.txtReaction);
            this.Controls.Add(this.lblReaction);
            this.Controls.Add(this.txtAllergyName);
            this.Controls.Add(this.lblAllergyName);
            this.Controls.Add(this.cboAllergyType);
            this.Controls.Add(this.lblAllergyType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AllergyFormDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allergy";
            this.Load += new System.EventHandler(this.AllergyFormDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAllergyType;
        private System.Windows.Forms.ComboBox cboAllergyType;
        private System.Windows.Forms.Label lblAllergyName;
        private System.Windows.Forms.TextBox txtAllergyName;
        private System.Windows.Forms.Label lblReaction;
        private System.Windows.Forms.TextBox txtReaction;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.ComboBox cboSeverity;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
