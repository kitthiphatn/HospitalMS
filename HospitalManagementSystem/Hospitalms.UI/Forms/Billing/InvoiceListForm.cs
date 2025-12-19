using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Billing
{
    public partial class InvoiceListForm : Form
    {
        public InvoiceListForm()
        {
            InitializeComponent();
        }

        private void InvoiceListForm_Load(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void LoadInvoices(string searchTerm = "", string statusFilter = "All")
        {
            try
            {
                string query = @"
                    SELECT 
                        i.InvoiceID,
                        i.InvoiceNumber,
                        p.FirstName + ' ' + p.LastName AS PatientName,
                        i.InvoiceDate,
                        i.TotalAmount,
                        i.PaidAmount,
                        (i.TotalAmount - i.PaidAmount) AS Balance,
                        i.Status
                    FROM Invoices i
                    INNER JOIN Patients p ON i.PatientID = p.PatientID
                    WHERE i.IsActive = 1";

                // เพิ่มเงื่อนไขค้นหา
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += @" AND (i.InvoiceNumber LIKE @Search 
                                OR p.FirstName LIKE @Search 
                                OR p.LastName LIKE @Search)";
                }

                // เพิ่มเงื่อนไขกรองสถานะ
                if (statusFilter != "All")
                {
                    query += " AND i.Status = @Status";
                }

                query += " ORDER BY i.InvoiceDate DESC";

                SqlParameter[] parameters = null;
                
                if (!string.IsNullOrWhiteSpace(searchTerm) && statusFilter != "All")
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Search", "%" + searchTerm + "%"),
                        new SqlParameter("@Status", statusFilter)
                    };
                }
                else if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Search", "%" + searchTerm + "%")
                    };
                }
                else if (statusFilter != "All")
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Status", statusFilter)
                    };
                }

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                dgvInvoices.DataSource = dt;

                // ตั้งค่าคอลัมน์
                ConfigureDataGridView();

                // แสดงสถานะด้วยสี
                ColorCodeRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoices: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvInvoices.Columns.Count > 0)
            {
                dgvInvoices.Columns["InvoiceID"].Visible = false;
                dgvInvoices.Columns["InvoiceNumber"].HeaderText = "Invoice No.";
                dgvInvoices.Columns["InvoiceNumber"].Width = 150;
                dgvInvoices.Columns["PatientName"].HeaderText = "Patient";
                dgvInvoices.Columns["InvoiceDate"].HeaderText = "Date";
                dgvInvoices.Columns["InvoiceDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvInvoices.Columns["TotalAmount"].HeaderText = "Total";
                dgvInvoices.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
                dgvInvoices.Columns["PaidAmount"].HeaderText = "Paid";
                dgvInvoices.Columns["PaidAmount"].DefaultCellStyle.Format = "N2";
                dgvInvoices.Columns["Balance"].HeaderText = "Balance";
                dgvInvoices.Columns["Balance"].DefaultCellStyle.Format = "N2";
                dgvInvoices.Columns["Status"].HeaderText = "Status";
            }
        }

        private void ColorCodeRows()
        {
            foreach (DataGridViewRow row in dgvInvoices.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString();
                
                switch (status)
                {
                    case "Unpaid":
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        break;
                    case "Partial":
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        break;
                    case "Paid":
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                    case "Cancelled":
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        break;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadInvoices(txtSearch.Text, cboStatusFilter.SelectedItem.ToString());
        }

        private void cboStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInvoices(txtSearch.Text, cboStatusFilter.SelectedItem.ToString());
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            InvoiceFormDialog dialog = new InvoiceFormDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadInvoices();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int invoiceId = Convert.ToInt32(dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value);
            
            InvoiceFormDialog dialog = new InvoiceFormDialog(invoiceId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadInvoices();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string status = dgvInvoices.SelectedRows[0].Cells["Status"].Value.ToString();
            
            if (status == "Paid")
            {
                MessageBox.Show("Cannot delete a paid invoice.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this invoice?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int invoiceId = Convert.ToInt32(dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value);

                    string query = "UPDATE Invoices SET IsActive = 0 WHERE InvoiceID = @InvoiceID";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@InvoiceID", invoiceId)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);

                    MessageBox.Show("Invoice deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadInvoices();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting invoice: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice to preview.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int invoiceId = Convert.ToInt32(dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value);
            
            InvoicePreviewForm previewForm = new InvoicePreviewForm(invoiceId);
            previewForm.ShowDialog();
        }

        private void btnRecordPayment_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice to record payment.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string status = dgvInvoices.SelectedRows[0].Cells["Status"].Value.ToString();
            
            if (status == "Paid")
            {
                MessageBox.Show("This invoice is already fully paid.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int invoiceId = Convert.ToInt32(dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value);
            
            PaymentFormDialog dialog = new PaymentFormDialog(invoiceId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadInvoices();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboStatusFilter.SelectedIndex = 0;
            LoadInvoices();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
