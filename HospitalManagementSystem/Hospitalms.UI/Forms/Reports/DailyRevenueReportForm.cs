using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace HospitalMS.UI.Forms.Reports
{
    public partial class DailyRevenueReportForm : Form
    {
        private DataTable _summaryData;
        private DataTable _detailsData;

        public DailyRevenueReportForm()
        {
            InitializeComponent();
        }

        private void DailyRevenueReportForm_Load(object sender, EventArgs e)
        {
            // Set default date to today
            dtpReportDate.Value = DateTime.Today;
            
            // Load today's report
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Get selected date
                DateTime reportDate = dtpReportDate.Value.Date;

                // Execute stored procedure
                string query = "EXEC sp_GetDailyRevenue @ReportDate";
                SqlParameter[] parameters = {
                    new SqlParameter("@ReportDate", reportDate)
                };

                DataSet ds = DatabaseHelper.ExecuteDataSet(query, parameters);

                if (ds.Tables.Count >= 2)
                {
                    _summaryData = ds.Tables[0];
                    _detailsData = ds.Tables[1];

                    DisplaySummary();
                    DisplayDetails();
                }
                else
                {
                    MessageBox.Show("No data returned from database.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DisplaySummary()
        {
            if (_summaryData == null || _summaryData.Rows.Count == 0)
            {
                // Clear all summary labels
                lblTotalRevenue.Text = "฿0.00";
                lblCash.Text = "฿0.00";
                lblCard.Text = "฿0.00";
                lblOther.Text = "฿0.00";
                lblInvoiceCount.Text = "0";
                lblOutstanding.Text = "฿0.00";
                return;
            }

            DataRow summary = _summaryData.Rows[0];

            // Update summary cards
            lblTotalRevenue.Text = $"฿{Convert.ToDecimal(summary["TotalPaid"]):N2}";
            lblCash.Text = $"฿{Convert.ToDecimal(summary["CashPayments"]):N2}";
            lblCard.Text = $"฿{Convert.ToDecimal(summary["CardPayments"]):N2}";
            
            // Calculate other payments (Transfer + Insurance + Other)
            decimal otherPayments = Convert.ToDecimal(summary["TransferPayments"]) +
                                   Convert.ToDecimal(summary["InsurancePayments"]) +
                                   Convert.ToDecimal(summary["OtherPayments"]);
            lblOther.Text = $"฿{otherPayments:N2}";
            
            lblInvoiceCount.Text = summary["TotalInvoices"].ToString();
            lblOutstanding.Text = $"฿{Convert.ToDecimal(summary["TotalOutstanding"]):N2}";

            // Update title with date
            lblReportTitle.Text = $"Daily Revenue Report - {dtpReportDate.Value:dd/MM/yyyy}";
        }

        private void DisplayDetails()
        {
            dgvTransactions.DataSource = _detailsData;

            if (dgvTransactions.Columns.Count > 0)
            {
                // Hide ID column
                dgvTransactions.Columns["InvoiceID"].Visible = false;

                // Format columns
                dgvTransactions.Columns["InvoiceNumber"].HeaderText = "Invoice #";
                dgvTransactions.Columns["InvoiceNumber"].Width = 120;

                dgvTransactions.Columns["PatientName"].HeaderText = "Patient";
                dgvTransactions.Columns["PatientName"].Width = 180;

                dgvTransactions.Columns["InvoiceDate"].HeaderText = "Date";
                dgvTransactions.Columns["InvoiceDate"].Width = 100;
                dgvTransactions.Columns["InvoiceDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvTransactions.Columns["TotalAmount"].HeaderText = "Total";
                dgvTransactions.Columns["TotalAmount"].Width = 100;
                dgvTransactions.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
                dgvTransactions.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvTransactions.Columns["PaidAmount"].HeaderText = "Paid";
                dgvTransactions.Columns["PaidAmount"].Width = 100;
                dgvTransactions.Columns["PaidAmount"].DefaultCellStyle.Format = "N2";
                dgvTransactions.Columns["PaidAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvTransactions.Columns["Balance"].HeaderText = "Balance";
                dgvTransactions.Columns["Balance"].Width = 100;
                dgvTransactions.Columns["Balance"].DefaultCellStyle.Format = "N2";
                dgvTransactions.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvTransactions.Columns["Status"].HeaderText = "Status";
                dgvTransactions.Columns["Status"].Width = 100;

                dgvTransactions.Columns["PaymentMethods"].HeaderText = "Payment Methods";
                dgvTransactions.Columns["PaymentMethods"].Width = 250;

                dgvTransactions.Columns["LastPaymentDate"].HeaderText = "Last Payment";
                dgvTransactions.Columns["LastPaymentDate"].Width = 120;
                dgvTransactions.Columns["LastPaymentDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            // Update record count
            lblRecordCount.Text = $"Total: {_detailsData.Rows.Count} transactions";
        }

        private void dtpReportDate_ValueChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpReportDate.Value = DateTime.Today;
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            dtpReportDate.Value = DateTime.Today.AddDays(-1);
        }

        private void btnThisWeek_Click(object sender, EventArgs e)
        {
            // Show message for future implementation
            MessageBox.Show("Weekly report will be implemented in the next version.", "Coming Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // TODO: Implement Excel export
            MessageBox.Show("Excel export will be implemented in the next version.", "Coming Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // TODO: Implement print functionality
            MessageBox.Show("Print functionality will be implemented in the next version.", "Coming Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
