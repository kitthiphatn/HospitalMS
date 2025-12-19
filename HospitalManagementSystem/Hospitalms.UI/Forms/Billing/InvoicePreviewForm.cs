using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Billing
{
    public partial class InvoicePreviewForm : Form
    {
        private int _invoiceId;
        private PrintDocument _printDocument;
        private DataTable _invoiceData;
        private DataTable _itemsData;
        private DataTable _paymentsData;

        public InvoicePreviewForm(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void InvoicePreviewForm_Load(object sender, EventArgs e)
        {
            LoadInvoiceData();
            DisplayInvoice();
        }

        private void LoadInvoiceData()
        {
            try
            {
                // Load Invoice Header
                string query = @"SELECT 
                               i.InvoiceNumber,
                               i.InvoiceDate,
                               i.DueDate,
                               p.FirstName + ' ' + p.LastName AS PatientName,
                               p.Address,
                               p.Phone,
                               p.Email,
                               i.SubTotal,
                               i.TaxAmount,
                               i.DiscountAmount,
                               i.TotalAmount,
                               i.PaidAmount,
                               (i.TotalAmount - i.PaidAmount) AS Balance,
                               i.Status
                               FROM Invoices i
                               INNER JOIN Patients p ON i.PatientID = p.PatientID
                               WHERE i.InvoiceID = @InvoiceID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId)
                };

                _invoiceData = DatabaseHelper.ExecuteDataTable(query, parameters);

                // Load Invoice Items
                query = @"SELECT 
                         ItemType,
                         ItemDescription AS Description,
                         Quantity,
                         UnitPrice,
                         DiscountPercent AS [Discount %],
                         Amount
                         FROM InvoiceItems
                         WHERE InvoiceID = @InvoiceID AND IsActive = 1";

                SqlParameter[] itemsParams = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId)
                };

                _itemsData = DatabaseHelper.ExecuteDataTable(query, itemsParams);

                // Load Payments
                query = @"SELECT 
                         PaymentDate,
                         PaymentMethod,
                         Amount,
                         ReferenceNumber
                         FROM Payments
                         WHERE InvoiceID = @InvoiceID
                         ORDER BY PaymentDate";

                SqlParameter[] paymentsParams = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId)
                };

                _paymentsData = DatabaseHelper.ExecuteDataTable(query, paymentsParams);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayInvoice()
        {
            if (_invoiceData.Rows.Count == 0) return;

            DataRow invoice = _invoiceData.Rows[0];

            // Build professional invoice display
            StringBuilder html = new StringBuilder();
            
            html.AppendLine("<html><head><style>");
            html.AppendLine("body { font-family: 'Segoe UI', Arial; margin: 40px; }");
            html.AppendLine(".header { text-align: center; border-bottom: 3px solid #3498db; padding-bottom: 20px; margin-bottom: 30px; }");
            html.AppendLine(".company-name { font-size: 28px; font-weight: bold; color: #2c3e50; }");
            html.AppendLine(".invoice-title { font-size: 24px; color: #3498db; margin-top: 10px; }");
            html.AppendLine(".info-section { margin: 20px 0; }");
            html.AppendLine(".info-row { display: flex; justify-content: space-between; margin: 10px 0; }");
            html.AppendLine(".label { font-weight: bold; color: #7f8c8d; }");
            html.AppendLine("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            html.AppendLine("th { background-color: #3498db; color: white; padding: 12px; text-align: left; }");
            html.AppendLine("td { padding: 10px; border-bottom: 1px solid #ecf0f1; }");
            html.AppendLine(".summary { float: right; width: 300px; margin-top: 20px; }");
            html.AppendLine(".summary-row { display: flex; justify-content: space-between; padding: 8px 0; }");
            html.AppendLine(".total-row { font-size: 18px; font-weight: bold; border-top: 2px solid #3498db; padding-top: 10px; }");
            html.AppendLine(".status-paid { color: #27ae60; font-weight: bold; }");
            html.AppendLine(".status-unpaid { color: #e74c3c; font-weight: bold; }");
            html.AppendLine(".status-partial { color: #f39c12; font-weight: bold; }");
            html.AppendLine("</style></head><body>");

            // Header
            html.AppendLine("<div class='header'>");
            html.AppendLine("<div class='company-name'>🏥 HOSPITAL MANAGEMENT SYSTEM</div>");
            html.AppendLine("<div class='invoice-title'>INVOICE</div>");
            html.AppendLine("</div>");

            // Invoice Info
            html.AppendLine("<div class='info-section'>");
            html.AppendLine($"<div class='info-row'><span class='label'>Invoice Number:</span><span>{invoice["InvoiceNumber"]}</span></div>");
            html.AppendLine($"<div class='info-row'><span class='label'>Invoice Date:</span><span>{Convert.ToDateTime(invoice["InvoiceDate"]):dd/MM/yyyy}</span></div>");
            html.AppendLine($"<div class='info-row'><span class='label'>Due Date:</span><span>{Convert.ToDateTime(invoice["DueDate"]):dd/MM/yyyy}</span></div>");
            
            string statusClass = invoice["Status"].ToString() == "Paid" ? "status-paid" : 
                                invoice["Status"].ToString() == "Unpaid" ? "status-unpaid" : "status-partial";
            html.AppendLine($"<div class='info-row'><span class='label'>Status:</span><span class='{statusClass}'>{invoice["Status"]}</span></div>");
            html.AppendLine("</div>");

            // Bill To
            html.AppendLine("<div class='info-section'>");
            html.AppendLine("<div class='label'>BILL TO:</div>");
            html.AppendLine($"<div>{invoice["PatientName"]}</div>");
            html.AppendLine($"<div>{invoice["Address"]}</div>");
            html.AppendLine($"<div>Phone: {invoice["Phone"]}</div>");
            if (invoice["Email"] != DBNull.Value)
                html.AppendLine($"<div>Email: {invoice["Email"]}</div>");
            html.AppendLine("</div>");

            // Items Table
            html.AppendLine("<table>");
            html.AppendLine("<thead><tr><th>Description</th><th>Type</th><th>Qty</th><th>Unit Price</th><th>Discount</th><th>Amount</th></tr></thead>");
            html.AppendLine("<tbody>");

            foreach (DataRow item in _itemsData.Rows)
            {
                html.AppendLine("<tr>");
                html.AppendLine($"<td>{item["Description"]}</td>");
                html.AppendLine($"<td>{item["ItemType"]}</td>");
                html.AppendLine($"<td>{item["Quantity"]}</td>");
                html.AppendLine($"<td>฿{Convert.ToDecimal(item["UnitPrice"]):N2}</td>");
                html.AppendLine($"<td>{Convert.ToDecimal(item["Discount %"]):N2}%</td>");
                html.AppendLine($"<td>฿{Convert.ToDecimal(item["Amount"]):N2}</td>");
                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody></table>");

            // Summary
            html.AppendLine("<div class='summary'>");
            html.AppendLine($"<div class='summary-row'><span>SubTotal:</span><span>฿{Convert.ToDecimal(invoice["SubTotal"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row'><span>Tax (7%):</span><span>฿{Convert.ToDecimal(invoice["TaxAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row'><span>Discount:</span><span>฿{Convert.ToDecimal(invoice["DiscountAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row total-row'><span>TOTAL:</span><span>฿{Convert.ToDecimal(invoice["TotalAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row'><span>Paid:</span><span>฿{Convert.ToDecimal(invoice["PaidAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row total-row'><span>BALANCE:</span><span>฿{Convert.ToDecimal(invoice["Balance"]):N2}</span></div>");
            html.AppendLine("</div>");

            html.AppendLine("<div style='clear:both; margin-top: 60px; text-align: center; color: #95a5a6;'>");
            html.AppendLine("<p>Thank you for your business!</p>");
            html.AppendLine("</div>");

            html.AppendLine("</body></html>");

            // Display in WebBrowser control (if you want to add one) or RichTextBox
            // For now, we'll use the existing labels
            lblInvoiceDetails.Text = $@"
🏥 HOSPITAL MANAGEMENT SYSTEM
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

                                    INVOICE

Invoice Number:  {invoice["InvoiceNumber"]}
Invoice Date:    {Convert.ToDateTime(invoice["InvoiceDate"]):dd/MM/yyyy}
Due Date:        {Convert.ToDateTime(invoice["DueDate"]):dd/MM/yyyy}
Status:          {invoice["Status"]}

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

BILL TO:
{invoice["PatientName"]}
{invoice["Address"]}
Phone: {invoice["Phone"]}
";
            lblInvoiceDetails.Font = new Font("Consolas", 10);

            // Display items in grid
            dgvItems.DataSource = _itemsData;
            if (dgvItems.Columns.Count > 0)
            {
                dgvItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                dgvItems.Columns["Amount"].DefaultCellStyle.Format = "N2";
                dgvItems.Columns["Discount %"].DefaultCellStyle.Format = "N2";
            }

            // Summary
            StringBuilder summaryText = new StringBuilder();
            summaryText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            summaryText.AppendLine();
            summaryText.AppendLine($"                                          SubTotal:  ฿{Convert.ToDecimal(invoice["SubTotal"]):N2}");
            summaryText.AppendLine($"                                          Tax (7%):  ฿{Convert.ToDecimal(invoice["TaxAmount"]):N2}");
            summaryText.AppendLine($"                                          Discount:  ฿{Convert.ToDecimal(invoice["DiscountAmount"]):N2}");
            summaryText.AppendLine("                                          ─────────────────────────");
            summaryText.AppendLine($"                                          TOTAL:     ฿{Convert.ToDecimal(invoice["TotalAmount"]):N2}");
            summaryText.AppendLine($"                                          Paid:      ฿{Convert.ToDecimal(invoice["PaidAmount"]):N2}");
            summaryText.AppendLine("                                          ─────────────────────────");
            summaryText.AppendLine($"                                          BALANCE:   ฿{Convert.ToDecimal(invoice["Balance"]):N2}");
            summaryText.AppendLine();
            
            // Add Payment History if exists
            if (_paymentsData != null && _paymentsData.Rows.Count > 0)
            {
                summaryText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                summaryText.AppendLine();
                summaryText.AppendLine("PAYMENT HISTORY:");
                summaryText.AppendLine();
                
                foreach (DataRow payment in _paymentsData.Rows)
                {
                    string paymentDate = Convert.ToDateTime(payment["PaymentDate"]).ToString("dd/MM/yyyy");
                    string paymentMethod = payment["PaymentMethod"].ToString();
                    string amount = Convert.ToDecimal(payment["Amount"]).ToString("N2");
                    string reference = payment["ReferenceNumber"] != DBNull.Value ? payment["ReferenceNumber"].ToString() : "-";
                    
                    summaryText.AppendLine($"  • {paymentDate} - {paymentMethod,-15} ฿{amount,10}  (Ref: {reference})");
                }
                
                summaryText.AppendLine();
            }
            
            summaryText.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            summaryText.AppendLine();
            summaryText.AppendLine("                        Thank you for your business!");
            
            lblSummary.Text = summaryText.ToString();
            lblSummary.Font = new Font("Consolas", 10);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = _printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    _printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_invoiceData.Rows.Count == 0) return;

            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 20, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font normalFont = new Font("Arial", 10);
            Font smallFont = new Font("Arial", 9);
            Brush brush = Brushes.Black;
            Brush blueBrush = new SolidBrush(Color.FromArgb(52, 152, 219));

            float y = 50;
            float leftMargin = 50;
            float rightMargin = e.PageBounds.Width - 50;

            DataRow invoice = _invoiceData.Rows[0];

            // Header
            g.DrawString("🏥 HOSPITAL MANAGEMENT SYSTEM", titleFont, blueBrush, leftMargin, y);
            y += 40;
            g.DrawString("INVOICE", headerFont, blueBrush, leftMargin, y);
            y += 40;

            // Line
            g.DrawLine(new Pen(blueBrush, 2), leftMargin, y, rightMargin, y);
            y += 20;

            // Invoice Info
            g.DrawString($"Invoice Number: {invoice["InvoiceNumber"]}", normalFont, brush, leftMargin, y);
            g.DrawString($"Date: {Convert.ToDateTime(invoice["InvoiceDate"]):dd/MM/yyyy}", normalFont, brush, rightMargin - 200, y);
            y += 20;
            g.DrawString($"Due Date: {Convert.ToDateTime(invoice["DueDate"]):dd/MM/yyyy}", normalFont, brush, leftMargin, y);
            g.DrawString($"Status: {invoice["Status"]}", normalFont, brush, rightMargin - 200, y);
            y += 40;

            // Bill To
            g.DrawString("BILL TO:", headerFont, brush, leftMargin, y);
            y += 25;
            g.DrawString(invoice["PatientName"].ToString(), normalFont, brush, leftMargin, y);
            y += 20;
            g.DrawString(invoice["Address"].ToString(), smallFont, brush, leftMargin, y);
            y += 20;
            g.DrawString($"Phone: {invoice["Phone"]}", smallFont, brush, leftMargin, y);
            y += 40;

            // Items Table Header
            float col1 = leftMargin;
            float col2 = 300;
            float col3 = 400;
            float col4 = 500;
            float col5 = 600;

            g.FillRectangle(blueBrush, col1, y, rightMargin - leftMargin, 25);
            g.DrawString("Description", headerFont, Brushes.White, col1 + 5, y + 5);
            g.DrawString("Qty", headerFont, Brushes.White, col2 + 5, y + 5);
            g.DrawString("Price", headerFont, Brushes.White, col3 + 5, y + 5);
            g.DrawString("Discount", headerFont, Brushes.White, col4 + 5, y + 5);
            g.DrawString("Amount", headerFont, Brushes.White, col5 + 5, y + 5);
            y += 30;

            // Items
            foreach (DataRow item in _itemsData.Rows)
            {
                g.DrawString(item["Description"].ToString(), normalFont, brush, col1, y);
                g.DrawString(item["Quantity"].ToString(), normalFont, brush, col2, y);
                g.DrawString(Convert.ToDecimal(item["UnitPrice"]).ToString("N2"), normalFont, brush, col3, y);
                g.DrawString(Convert.ToDecimal(item["Discount %"]).ToString("N1") + "%", normalFont, brush, col4, y);
                g.DrawString(Convert.ToDecimal(item["Amount"]).ToString("N2"), normalFont, brush, col5, y);
                y += 20;
            }

            y += 20;

            // Summary
            float summaryX = rightMargin - 250;
            g.DrawString($"SubTotal:", normalFont, brush, summaryX, y);
            g.DrawString($"฿{Convert.ToDecimal(invoice["SubTotal"]):N2}", normalFont, brush, summaryX + 150, y);
            y += 20;
            g.DrawString($"Tax (7%):", normalFont, brush, summaryX, y);
            g.DrawString($"฿{Convert.ToDecimal(invoice["TaxAmount"]):N2}", normalFont, brush, summaryX + 150, y);
            y += 20;
            g.DrawString($"Discount:", normalFont, brush, summaryX, y);
            g.DrawString($"฿{Convert.ToDecimal(invoice["DiscountAmount"]):N2}", normalFont, brush, summaryX + 150, y);
            y += 25;
            g.DrawLine(Pens.Black, summaryX, y, rightMargin, y);
            y += 5;
            g.DrawString($"TOTAL:", headerFont, brush, summaryX, y);
            g.DrawString($"฿{Convert.ToDecimal(invoice["TotalAmount"]):N2}", headerFont, brush, summaryX + 150, y);
            y += 25;
            g.DrawString($"Paid:", normalFont, brush, summaryX, y);
            g.DrawString($"฿{Convert.ToDecimal(invoice["PaidAmount"]):N2}", normalFont, brush, summaryX + 150, y);
            y += 20;
            g.DrawLine(Pens.Black, summaryX, y, rightMargin, y);
            y += 5;
            g.DrawString($"BALANCE:", headerFont, brush, summaryX, y);
            g.DrawString($"฿{Convert.ToDecimal(invoice["Balance"]):N2}", headerFont, brush, summaryX + 150, y);

            // Footer
            y = e.PageBounds.Height - 100;
            g.DrawString("Thank you for your business!", normalFont, Brushes.Gray, leftMargin + 200, y);
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
                saveDialog.DefaultExt = "html";
                saveDialog.FileName = $"Invoice_{_invoiceData.Rows[0]["InvoiceNumber"]}.html";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToHTML(saveDialog.FileName);
                    MessageBox.Show($"Invoice exported successfully to:\n{saveDialog.FileName}\n\nYou can open this file in a browser and print to PDF.", 
                        "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToHTML(string filename)
        {
            if (_invoiceData.Rows.Count == 0) return;

            DataRow invoice = _invoiceData.Rows[0];
            StringBuilder html = new StringBuilder();
            
            html.AppendLine("<!DOCTYPE html><html><head>");
            html.AppendLine("<meta charset='UTF-8'>");
            html.AppendLine("<title>Invoice " + invoice["InvoiceNumber"] + "</title>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 40px; }");
            html.AppendLine(".header { text-align: center; border-bottom: 3px solid #3498db; padding-bottom: 20px; margin-bottom: 30px; }");
            html.AppendLine(".company-name { font-size: 28px; font-weight: bold; color: #2c3e50; }");
            html.AppendLine(".invoice-title { font-size: 24px; color: #3498db; margin-top: 10px; }");
            html.AppendLine(".info-section { margin: 20px 0; }");
            html.AppendLine(".info-row { display: flex; justify-content: space-between; margin: 10px 0; }");
            html.AppendLine(".label { font-weight: bold; color: #7f8c8d; }");
            html.AppendLine("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            html.AppendLine("th { background-color: #3498db; color: white; padding: 12px; text-align: left; }");
            html.AppendLine("td { padding: 10px; border-bottom: 1px solid #ecf0f1; }");
            html.AppendLine(".summary { float: right; width: 300px; margin-top: 20px; background: #f8f9fa; padding: 20px; border-radius: 5px; }");
            html.AppendLine(".summary-row { display: flex; justify-content: space-between; padding: 8px 0; }");
            html.AppendLine(".total-row { font-size: 18px; font-weight: bold; border-top: 2px solid #3498db; padding-top: 10px; margin-top: 10px; }");
            html.AppendLine(".status-paid { color: #27ae60; font-weight: bold; }");
            html.AppendLine(".status-unpaid { color: #e74c3c; font-weight: bold; }");
            html.AppendLine(".status-partial { color: #f39c12; font-weight: bold; }");
            html.AppendLine("@media print { body { margin: 20px; } }");
            html.AppendLine("</style></head><body>");

            // Header
            html.AppendLine("<div class='header'>");
            html.AppendLine("<div class='company-name'>🏥 HOSPITAL MANAGEMENT SYSTEM</div>");
            html.AppendLine("<div class='invoice-title'>INVOICE</div>");
            html.AppendLine("</div>");

            // Invoice Info
            html.AppendLine("<div class='info-section'>");
            html.AppendLine($"<div class='info-row'><span class='label'>Invoice Number:</span><span>{invoice["InvoiceNumber"]}</span></div>");
            html.AppendLine($"<div class='info-row'><span class='label'>Invoice Date:</span><span>{Convert.ToDateTime(invoice["InvoiceDate"]):dd/MM/yyyy}</span></div>");
            html.AppendLine($"<div class='info-row'><span class='label'>Due Date:</span><span>{Convert.ToDateTime(invoice["DueDate"]):dd/MM/yyyy}</span></div>");
            
            string statusClass = invoice["Status"].ToString() == "Paid" ? "status-paid" : 
                                invoice["Status"].ToString() == "Unpaid" ? "status-unpaid" : "status-partial";
            html.AppendLine($"<div class='info-row'><span class='label'>Status:</span><span class='{statusClass}'>{invoice["Status"]}</span></div>");
            html.AppendLine("</div>");

            // Bill To
            html.AppendLine("<div class='info-section'>");
            html.AppendLine("<div class='label'>BILL TO:</div>");
            html.AppendLine($"<div style='margin-top: 10px;'><strong>{invoice["PatientName"]}</strong></div>");
            html.AppendLine($"<div>{invoice["Address"]}</div>");
            html.AppendLine($"<div>Phone: {invoice["Phone"]}</div>");
            if (invoice["Email"] != DBNull.Value)
                html.AppendLine($"<div>Email: {invoice["Email"]}</div>");
            html.AppendLine("</div>");

            // Items Table
            html.AppendLine("<table>");
            html.AppendLine("<thead><tr><th>Description</th><th>Type</th><th>Qty</th><th>Unit Price</th><th>Discount</th><th>Amount</th></tr></thead>");
            html.AppendLine("<tbody>");

            foreach (DataRow item in _itemsData.Rows)
            {
                html.AppendLine("<tr>");
                html.AppendLine($"<td>{item["Description"]}</td>");
                html.AppendLine($"<td>{item["ItemType"]}</td>");
                html.AppendLine($"<td>{item["Quantity"]}</td>");
                html.AppendLine($"<td>฿{Convert.ToDecimal(item["UnitPrice"]):N2}</td>");
                html.AppendLine($"<td>{Convert.ToDecimal(item["Discount %"]):N2}%</td>");
                html.AppendLine($"<td>฿{Convert.ToDecimal(item["Amount"]):N2}</td>");
                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody></table>");

            // Payment Methods Section (if any payments exist)
            if (_paymentsData != null && _paymentsData.Rows.Count > 0)
            {
                html.AppendLine("<div class='info-section' style='margin-top: 30px;'>");
                html.AppendLine("<div class='label' style='font-size: 14px; margin-bottom: 10px;'>PAYMENT HISTORY:</div>");
                html.AppendLine("<table style='width: 60%; margin-left: 0;'>");
                html.AppendLine("<thead><tr><th>Date</th><th>Method</th><th>Reference</th><th>Amount</th></tr></thead>");
                html.AppendLine("<tbody>");

                foreach (DataRow payment in _paymentsData.Rows)
                {
                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{Convert.ToDateTime(payment["PaymentDate"]):dd/MM/yyyy}</td>");
                    html.AppendLine($"<td>{payment["PaymentMethod"]}</td>");
                    html.AppendLine($"<td>{(payment["ReferenceNumber"] != DBNull.Value ? payment["ReferenceNumber"] : "-")}</td>");
                    html.AppendLine($"<td>฿{Convert.ToDecimal(payment["Amount"]):N2}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody></table>");
                html.AppendLine("</div>");
            }

            // Summary
            html.AppendLine("<div class='summary'>");
            html.AppendLine($"<div class='summary-row'><span>SubTotal:</span><span>฿{Convert.ToDecimal(invoice["SubTotal"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row'><span>Tax (7%):</span><span>฿{Convert.ToDecimal(invoice["TaxAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row'><span>Discount:</span><span>฿{Convert.ToDecimal(invoice["DiscountAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row total-row'><span>TOTAL:</span><span>฿{Convert.ToDecimal(invoice["TotalAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row'><span>Paid:</span><span>฿{Convert.ToDecimal(invoice["PaidAmount"]):N2}</span></div>");
            html.AppendLine($"<div class='summary-row total-row'><span>BALANCE:</span><span>฿{Convert.ToDecimal(invoice["Balance"]):N2}</span></div>");
            html.AppendLine("</div>");

            html.AppendLine("<div style='clear:both; margin-top: 80px; text-align: center; color: #95a5a6; border-top: 1px solid #ecf0f1; padding-top: 20px;'>");
            html.AppendLine("<p>Thank you for your business!</p>");
            html.AppendLine("<p style='font-size: 12px;'>This is a computer-generated invoice.</p>");
            html.AppendLine("</div>");

            html.AppendLine("</body></html>");

            File.WriteAllText(filename, html.ToString(), Encoding.UTF8);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
