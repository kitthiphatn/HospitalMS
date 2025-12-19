namespace HospitalMS.UI.Forms.Reports
{
    partial class DailyRevenueReportForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtpReportDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.pnlOutstanding = new System.Windows.Forms.Panel();
            this.lblOutstanding = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlInvoiceCount = new System.Windows.Forms.Panel();
            this.lblInvoiceCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlOther = new System.Windows.Forms.Panel();
            this.lblOther = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblCard = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlCash = new System.Windows.Forms.Panel();
            this.lblCash = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlTotalRevenue = new System.Windows.Forms.Panel();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.pnlDetailsHeader = new System.Windows.Forms.Panel();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlOutstanding.SuspendLayout();
            this.pnlInvoiceCount.SuspendLayout();
            this.pnlOther.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlCash.SuspendLayout();
            this.pnlTotalRevenue.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.pnlDetailsHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.btnYesterday);
            this.pnlHeader.Controls.Add(this.btnToday);
            this.pnlHeader.Controls.Add(this.dtpReportDate);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.lblReportTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1100, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYesterday.BackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesterday.Location = new System.Drawing.Point(890, 25);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(90, 30);
            this.btnYesterday.TabIndex = 4;
            this.btnYesterday.Text = "Yesterday";
            this.btnYesterday.UseVisualStyleBackColor = false;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnToday
            // 
            this.btnToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToday.BackColor = System.Drawing.Color.White;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Location = new System.Drawing.Point(800, 25);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(80, 30);
            this.btnToday.TabIndex = 3;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpReportDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReportDate.Location = new System.Drawing.Point(650, 27);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new System.Drawing.Size(130, 25);
            this.dtpReportDate.TabIndex = 2;
            this.dtpReportDate.ValueChanged += new System.EventHandler(this.dtpReportDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(600, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date:";
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblReportTitle.ForeColor = System.Drawing.Color.White;
            this.lblReportTitle.Location = new System.Drawing.Point(20, 22);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(243, 32);
            this.lblReportTitle.TabIndex = 0;
            this.lblReportTitle.Text = "Daily Revenue Report";
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.pnlOutstanding);
            this.pnlSummary.Controls.Add(this.pnlInvoiceCount);
            this.pnlSummary.Controls.Add(this.pnlOther);
            this.pnlSummary.Controls.Add(this.pnlCard);
            this.pnlSummary.Controls.Add(this.pnlCash);
            this.pnlSummary.Controls.Add(this.pnlTotalRevenue);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 80);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.pnlSummary.Size = new System.Drawing.Size(1200, 180);
            this.pnlSummary.TabIndex = 1;
            // 
            // pnlOutstanding
            // 
            this.pnlOutstanding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.pnlOutstanding.Controls.Add(this.lblOutstanding);
            this.pnlOutstanding.Controls.Add(this.label12);
            this.pnlOutstanding.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlOutstanding.Location = new System.Drawing.Point(1000, 20);
            this.pnlOutstanding.Name = "pnlOutstanding";
            this.pnlOutstanding.Padding = new System.Windows.Forms.Padding(15);
            this.pnlOutstanding.Size = new System.Drawing.Size(180, 150);
            this.pnlOutstanding.TabIndex = 5;
            // 
            // lblOutstanding
            // 
            this.lblOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutstanding.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblOutstanding.ForeColor = System.Drawing.Color.White;
            this.lblOutstanding.Location = new System.Drawing.Point(15, 50);
            this.lblOutstanding.Name = "lblOutstanding";
            this.lblOutstanding.Size = new System.Drawing.Size(150, 85);
            this.lblOutstanding.TabIndex = 1;
            this.lblOutstanding.Text = "฿0.00";
            this.lblOutstanding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(15, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 35);
            this.label12.TabIndex = 0;
            this.label12.Text = "Outstanding";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlInvoiceCount
            // 
            this.pnlInvoiceCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.pnlInvoiceCount.Controls.Add(this.lblInvoiceCount);
            this.pnlInvoiceCount.Controls.Add(this.label10);
            this.pnlInvoiceCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInvoiceCount.Location = new System.Drawing.Point(804, 20);
            this.pnlInvoiceCount.Name = "pnlInvoiceCount";
            this.pnlInvoiceCount.Padding = new System.Windows.Forms.Padding(15);
            this.pnlInvoiceCount.Size = new System.Drawing.Size(196, 150);
            this.pnlInvoiceCount.TabIndex = 4;
            // 
            // lblInvoiceCount
            // 
            this.lblInvoiceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvoiceCount.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceCount.ForeColor = System.Drawing.Color.White;
            this.lblInvoiceCount.Location = new System.Drawing.Point(15, 50);
            this.lblInvoiceCount.Name = "lblInvoiceCount";
            this.lblInvoiceCount.Size = new System.Drawing.Size(166, 85);
            this.lblInvoiceCount.TabIndex = 1;
            this.lblInvoiceCount.Text = "0";
            this.lblInvoiceCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(15, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(166, 35);
            this.label10.TabIndex = 0;
            this.label10.Text = "Invoices";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlOther
            // 
            this.pnlOther.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlOther.Controls.Add(this.lblOther);
            this.pnlOther.Controls.Add(this.label8);
            this.pnlOther.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlOther.Location = new System.Drawing.Point(608, 20);
            this.pnlOther.Name = "pnlOther";
            this.pnlOther.Padding = new System.Windows.Forms.Padding(15);
            this.pnlOther.Size = new System.Drawing.Size(196, 150);
            this.pnlOther.TabIndex = 3;
            // 
            // lblOther
            // 
            this.lblOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOther.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblOther.ForeColor = System.Drawing.Color.White;
            this.lblOther.Location = new System.Drawing.Point(15, 50);
            this.lblOther.Name = "lblOther";
            this.lblOther.Size = new System.Drawing.Size(166, 85);
            this.lblOther.TabIndex = 1;
            this.lblOther.Text = "฿0.00";
            this.lblOther.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(15, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 35);
            this.label8.TabIndex = 0;
            this.label8.Text = "Other Methods";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.pnlCard.Controls.Add(this.lblCard);
            this.pnlCard.Controls.Add(this.label6);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCard.Location = new System.Drawing.Point(412, 20);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCard.Size = new System.Drawing.Size(196, 150);
            this.pnlCard.TabIndex = 2;
            // 
            // lblCard
            // 
            this.lblCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCard.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblCard.ForeColor = System.Drawing.Color.White;
            this.lblCard.Location = new System.Drawing.Point(15, 50);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(166, 85);
            this.lblCard.TabIndex = 1;
            this.lblCard.Text = "฿0.00";
            this.lblCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(15, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 35);
            this.label6.TabIndex = 0;
            this.label6.Text = "Card Payments";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCash
            // 
            this.pnlCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.pnlCash.Controls.Add(this.lblCash);
            this.pnlCash.Controls.Add(this.label4);
            this.pnlCash.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCash.Location = new System.Drawing.Point(216, 20);
            this.pnlCash.Name = "pnlCash";
            this.pnlCash.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCash.Size = new System.Drawing.Size(196, 150);
            this.pnlCash.TabIndex = 1;
            // 
            // lblCash
            // 
            this.lblCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCash.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblCash.ForeColor = System.Drawing.Color.White;
            this.lblCash.Location = new System.Drawing.Point(15, 50);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(166, 85);
            this.lblCash.TabIndex = 1;
            this.lblCash.Text = "฿0.00";
            this.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 35);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cash Payments";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotalRevenue
            // 
            this.pnlTotalRevenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTotalRevenue.Controls.Add(this.lblTotalRevenue);
            this.pnlTotalRevenue.Controls.Add(this.label2);
            this.pnlTotalRevenue.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTotalRevenue.Location = new System.Drawing.Point(20, 20);
            this.pnlTotalRevenue.Name = "pnlTotalRevenue";
            this.pnlTotalRevenue.Padding = new System.Windows.Forms.Padding(15);
            this.pnlTotalRevenue.Size = new System.Drawing.Size(196, 150);
            this.pnlTotalRevenue.TabIndex = 0;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.White;
            this.lblTotalRevenue.Location = new System.Drawing.Point(15, 50);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(166, 85);
            this.lblTotalRevenue.TabIndex = 1;
            this.lblTotalRevenue.Text = "฿0.00";
            this.lblTotalRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Revenue";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.dgvTransactions);
            this.pnlDetails.Controls.Add(this.pnlDetailsHeader);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 260);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.pnlDetails.Size = new System.Drawing.Size(1200, 390);
            this.pnlDetails.TabIndex = 2;
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.Location = new System.Drawing.Point(20, 50);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(1160, 340);
            this.dgvTransactions.TabIndex = 1;
            // 
            // pnlDetailsHeader
            // 
            this.pnlDetailsHeader.Controls.Add(this.lblRecordCount);
            this.pnlDetailsHeader.Controls.Add(this.label3);
            this.pnlDetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailsHeader.Location = new System.Drawing.Point(20, 0);
            this.pnlDetailsHeader.Name = "pnlDetailsHeader";
            this.pnlDetailsHeader.Size = new System.Drawing.Size(1160, 50);
            this.pnlDetailsHeader.TabIndex = 0;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRecordCount.ForeColor = System.Drawing.Color.Gray;
            this.lblRecordCount.Location = new System.Drawing.Point(960, 15);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(200, 20);
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.Text = "Total: 0 transactions";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(0, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Transaction Details";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnPrint);
            this.pnlFooter.Controls.Add(this.btnExportExcel);
            this.pnlFooter.Controls.Add(this.btnRefresh);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 650);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlFooter.Size = new System.Drawing.Size(1200, 50);
            this.pnlFooter.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(240, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "🖨️ Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(120, 10);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(110, 30);
            this.btnExportExcel.TabIndex = 1;
            this.btnExportExcel.Text = "📊 Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(20, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DailyRevenueReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1216, 739);
            this.Name = "DailyRevenueReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Revenue Report";
            this.Load += new System.EventHandler(this.DailyRevenueReportForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlOutstanding.ResumeLayout(false);
            this.pnlInvoiceCount.ResumeLayout(false);
            this.pnlOther.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCash.ResumeLayout(false);
            this.pnlTotalRevenue.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.pnlDetailsHeader.ResumeLayout(false);
            this.pnlDetailsHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblReportTitle;
        private System.Windows.Forms.DateTimePicker dtpReportDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Panel pnlTotalRevenue;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCash;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlOther;
        private System.Windows.Forms.Label lblOther;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlInvoiceCount;
        private System.Windows.Forms.Label lblInvoiceCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlOutstanding;
        private System.Windows.Forms.Label lblOutstanding;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Panel pnlDetailsHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Button btnClose;
    }
}
