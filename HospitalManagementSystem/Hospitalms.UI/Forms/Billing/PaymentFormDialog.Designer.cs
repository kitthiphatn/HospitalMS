namespace Hospitalms.UI.Forms.Billing
{
    partial class PaymentFormDialog
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
            this.grpInvoiceInfo = new System.Windows.Forms.GroupBox();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPaymentDetails = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSocialSecurityNumber = new System.Windows.Forms.TextBox();
            this.lblSocialSecurity = new System.Windows.Forms.Label();
            this.txtInsuranceClaimNumber = new System.Windows.Forms.TextBox();
            this.lblInsuranceClaim = new System.Windows.Forms.Label();
            this.txtInsuranceProvider = new System.Windows.Forms.TextBox();
            this.lblInsuranceProvider = new System.Windows.Forms.Label();
            this.txtApprovalCode = new System.Windows.Forms.TextBox();
            this.lblApprovalCode = new System.Windows.Forms.Label();
            this.txtReferenceNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.grpInvoiceInfo.SuspendLayout();
            this.grpPaymentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(191, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Record Payment";
            // 
            // grpInvoiceInfo
            // 
            this.grpInvoiceInfo.Controls.Add(this.txtBalance);
            this.grpInvoiceInfo.Controls.Add(this.txtPaidAmount);
            this.grpInvoiceInfo.Controls.Add(this.txtTotalAmount);
            this.grpInvoiceInfo.Controls.Add(this.txtPatientName);
            this.grpInvoiceInfo.Controls.Add(this.txtInvoiceNumber);
            this.grpInvoiceInfo.Controls.Add(this.label5);
            this.grpInvoiceInfo.Controls.Add(this.label4);
            this.grpInvoiceInfo.Controls.Add(this.label3);
            this.grpInvoiceInfo.Controls.Add(this.label2);
            this.grpInvoiceInfo.Controls.Add(this.label1);
            this.grpInvoiceInfo.Location = new System.Drawing.Point(20, 90);
            this.grpInvoiceInfo.Name = "grpInvoiceInfo";
            this.grpInvoiceInfo.Size = new System.Drawing.Size(560, 120);
            this.grpInvoiceInfo.TabIndex = 1;
            this.grpInvoiceInfo.TabStop = false;
            this.grpInvoiceInfo.Text = "Invoice Information";
            // 
            // txtBalance
            // 
            this.txtBalance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtBalance.Location = new System.Drawing.Point(400, 80);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(130, 23);
            this.txtBalance.TabIndex = 9;
            this.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(400, 50);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.ReadOnly = true;
            this.txtPaidAmount.Size = new System.Drawing.Size(130, 20);
            this.txtPaidAmount.TabIndex = 8;
            this.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(400, 20);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(130, 20);
            this.txtTotalAmount.TabIndex = 7;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(120, 50);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(200, 20);
            this.txtPatientName.TabIndex = 6;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(120, 20);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.ReadOnly = true;
            this.txtInvoiceNumber.Size = new System.Drawing.Size(200, 20);
            this.txtInvoiceNumber.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(330, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Balance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Paid:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Patient:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice No.:";
            // 
            // grpPaymentDetails
            // 
            this.grpPaymentDetails.Controls.Add(this.txtNotes);
            this.grpPaymentDetails.Controls.Add(this.label13);
            this.grpPaymentDetails.Controls.Add(this.txtSocialSecurityNumber);
            this.grpPaymentDetails.Controls.Add(this.lblSocialSecurity);
            this.grpPaymentDetails.Controls.Add(this.txtInsuranceClaimNumber);
            this.grpPaymentDetails.Controls.Add(this.lblInsuranceClaim);
            this.grpPaymentDetails.Controls.Add(this.txtInsuranceProvider);
            this.grpPaymentDetails.Controls.Add(this.lblInsuranceProvider);
            this.grpPaymentDetails.Controls.Add(this.txtApprovalCode);
            this.grpPaymentDetails.Controls.Add(this.lblApprovalCode);
            this.grpPaymentDetails.Controls.Add(this.txtReferenceNumber);
            this.grpPaymentDetails.Controls.Add(this.label9);
            this.grpPaymentDetails.Controls.Add(this.nudAmount);
            this.grpPaymentDetails.Controls.Add(this.label8);
            this.grpPaymentDetails.Controls.Add(this.cboPaymentMethod);
            this.grpPaymentDetails.Controls.Add(this.label7);
            this.grpPaymentDetails.Controls.Add(this.dtpPaymentDate);
            this.grpPaymentDetails.Controls.Add(this.label6);
            this.grpPaymentDetails.Location = new System.Drawing.Point(20, 220);
            this.grpPaymentDetails.Name = "grpPaymentDetails";
            this.grpPaymentDetails.Size = new System.Drawing.Size(560, 310);
            this.grpPaymentDetails.TabIndex = 2;
            this.grpPaymentDetails.TabStop = false;
            this.grpPaymentDetails.Text = "Payment Details";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(150, 260);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(380, 40);
            this.txtNotes.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 263);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Notes:";
            // 
            // txtSocialSecurityNumber
            // 
            this.txtSocialSecurityNumber.Location = new System.Drawing.Point(150, 230);
            this.txtSocialSecurityNumber.Name = "txtSocialSecurityNumber";
            this.txtSocialSecurityNumber.Size = new System.Drawing.Size(200, 20);
            this.txtSocialSecurityNumber.TabIndex = 15;
            this.txtSocialSecurityNumber.Visible = false;
            // 
            // lblSocialSecurity
            // 
            this.lblSocialSecurity.AutoSize = true;
            this.lblSocialSecurity.Location = new System.Drawing.Point(20, 233);
            this.lblSocialSecurity.Name = "lblSocialSecurity";
            this.lblSocialSecurity.Size = new System.Drawing.Size(101, 13);
            this.lblSocialSecurity.TabIndex = 14;
            this.lblSocialSecurity.Text = "Social Security No.:";
            this.lblSocialSecurity.Visible = false;
            // 
            // txtInsuranceClaimNumber
            // 
            this.txtInsuranceClaimNumber.Location = new System.Drawing.Point(150, 200);
            this.txtInsuranceClaimNumber.Name = "txtInsuranceClaimNumber";
            this.txtInsuranceClaimNumber.Size = new System.Drawing.Size(200, 20);
            this.txtInsuranceClaimNumber.TabIndex = 13;
            this.txtInsuranceClaimNumber.Visible = false;
            // 
            // lblInsuranceClaim
            // 
            this.lblInsuranceClaim.AutoSize = true;
            this.lblInsuranceClaim.Location = new System.Drawing.Point(20, 203);
            this.lblInsuranceClaim.Name = "lblInsuranceClaim";
            this.lblInsuranceClaim.Size = new System.Drawing.Size(55, 13);
            this.lblInsuranceClaim.TabIndex = 12;
            this.lblInsuranceClaim.Text = "Claim No.:";
            this.lblInsuranceClaim.Visible = false;
            // 
            // txtInsuranceProvider
            // 
            this.txtInsuranceProvider.Location = new System.Drawing.Point(150, 170);
            this.txtInsuranceProvider.Name = "txtInsuranceProvider";
            this.txtInsuranceProvider.Size = new System.Drawing.Size(300, 20);
            this.txtInsuranceProvider.TabIndex = 11;
            this.txtInsuranceProvider.Visible = false;
            // 
            // lblInsuranceProvider
            // 
            this.lblInsuranceProvider.AutoSize = true;
            this.lblInsuranceProvider.Location = new System.Drawing.Point(20, 173);
            this.lblInsuranceProvider.Name = "lblInsuranceProvider";
            this.lblInsuranceProvider.Size = new System.Drawing.Size(101, 13);
            this.lblInsuranceProvider.TabIndex = 10;
            this.lblInsuranceProvider.Text = "Insurance Provider:";
            this.lblInsuranceProvider.Visible = false;
            // 
            // txtApprovalCode
            // 
            this.txtApprovalCode.Location = new System.Drawing.Point(150, 140);
            this.txtApprovalCode.Name = "txtApprovalCode";
            this.txtApprovalCode.Size = new System.Drawing.Size(200, 20);
            this.txtApprovalCode.TabIndex = 9;
            this.txtApprovalCode.Visible = false;
            // 
            // lblApprovalCode
            // 
            this.lblApprovalCode.AutoSize = true;
            this.lblApprovalCode.Location = new System.Drawing.Point(20, 143);
            this.lblApprovalCode.Name = "lblApprovalCode";
            this.lblApprovalCode.Size = new System.Drawing.Size(80, 13);
            this.lblApprovalCode.TabIndex = 8;
            this.lblApprovalCode.Text = "Approval Code:";
            this.lblApprovalCode.Visible = false;
            // 
            // txtReferenceNumber
            // 
            this.txtReferenceNumber.Location = new System.Drawing.Point(150, 110);
            this.txtReferenceNumber.Name = "txtReferenceNumber";
            this.txtReferenceNumber.Size = new System.Drawing.Size(250, 20);
            this.txtReferenceNumber.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Reference No.:";
            // 
            // nudAmount
            // 
            this.nudAmount.DecimalPlaces = 2;
            this.nudAmount.Location = new System.Drawing.Point(150, 80);
            this.nudAmount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(150, 20);
            this.nudAmount.TabIndex = 5;
            this.nudAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAmount.ThousandsSeparator = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Amount:";
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Location = new System.Drawing.Point(150, 50);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(250, 21);
            this.cboPaymentMethod.TabIndex = 3;
            this.cboPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cboPaymentMethod_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Payment Method:";
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPaymentDate.Location = new System.Drawing.Point(150, 20);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(150, 20);
            this.dtpPaymentDate.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Payment Date:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 540);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(600, 60);
            this.panelButtons.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(330, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💰 Record Payment";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PaymentFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.grpPaymentDetails);
            this.Controls.Add(this.grpInvoiceInfo);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentFormDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Record Payment";
            this.Load += new System.EventHandler(this.PaymentFormDialog_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.grpInvoiceInfo.ResumeLayout(false);
            this.grpInvoiceInfo.PerformLayout();
            this.grpPaymentDetails.ResumeLayout(false);
            this.grpPaymentDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpInvoiceInfo;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpPaymentDetails;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSocialSecurityNumber;
        private System.Windows.Forms.Label lblSocialSecurity;
        private System.Windows.Forms.TextBox txtInsuranceClaimNumber;
        private System.Windows.Forms.Label lblInsuranceClaim;
        private System.Windows.Forms.TextBox txtInsuranceProvider;
        private System.Windows.Forms.Label lblInsuranceProvider;
        private System.Windows.Forms.TextBox txtApprovalCode;
        private System.Windows.Forms.Label lblApprovalCode;
        private System.Windows.Forms.TextBox txtReferenceNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
