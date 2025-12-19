using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Billing
{
    public partial class PaymentFormDialog : Form
    {
        private int _invoiceId;
        private decimal _balance;

        public PaymentFormDialog(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
        }

        private void PaymentFormDialog_Load(object sender, EventArgs e)
        {
            LoadPaymentMethods();
            LoadInvoiceData();
            dtpPaymentDate.Value = DateTime.Now;
        }

        private void LoadPaymentMethods()
        {
            cboPaymentMethod.Items.Clear();
            cboPaymentMethod.Items.Add("Cash");
            cboPaymentMethod.Items.Add("Credit Card");
            cboPaymentMethod.Items.Add("Debit Card");
            cboPaymentMethod.Items.Add("Bank Transfer");
            cboPaymentMethod.Items.Add("PromptPay");
            cboPaymentMethod.Items.Add("Social Security");
            cboPaymentMethod.Items.Add("Health Insurance");
            cboPaymentMethod.SelectedIndex = 0;
        }

        private void LoadInvoiceData()
        {
            try
            {
                string query = @"SELECT i.InvoiceNumber, 
                               p.FirstName + ' ' + p.LastName AS PatientName,
                               i.TotalAmount, i.PaidAmount,
                               (i.TotalAmount - i.PaidAmount) AS Balance
                               FROM Invoices i
                               INNER JOIN Patients p ON i.PatientID = p.PatientID
                               WHERE i.InvoiceID = @InvoiceID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtInvoiceNumber.Text = row["InvoiceNumber"].ToString();
                    txtPatientName.Text = row["PatientName"].ToString();
                    txtTotalAmount.Text = Convert.ToDecimal(row["TotalAmount"]).ToString("N2");
                    txtPaidAmount.Text = Convert.ToDecimal(row["PaidAmount"]).ToString("N2");
                    
                    _balance = Convert.ToDecimal(row["Balance"]);
                    txtBalance.Text = _balance.ToString("N2");
                    
                    // Set default payment amount to balance
                    nudAmount.Value = _balance;
                    nudAmount.Maximum = _balance;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide all optional fields first
            lblApprovalCode.Visible = false;
            txtApprovalCode.Visible = false;
            lblInsuranceProvider.Visible = false;
            txtInsuranceProvider.Visible = false;
            lblInsuranceClaim.Visible = false;
            txtInsuranceClaimNumber.Visible = false;
            lblSocialSecurity.Visible = false;
            txtSocialSecurityNumber.Visible = false;

            string method = cboPaymentMethod.SelectedItem.ToString();

            switch (method)
            {
                case "Credit Card":
                case "Debit Card":
                    lblApprovalCode.Visible = true;
                    txtApprovalCode.Visible = true;
                    break;

                case "Health Insurance":
                    lblInsuranceProvider.Visible = true;
                    txtInsuranceProvider.Visible = true;
                    lblInsuranceClaim.Visible = true;
                    txtInsuranceClaimNumber.Visible = true;
                    break;

                case "Social Security":
                    lblSocialSecurity.Visible = true;
                    txtSocialSecurityNumber.Visible = true;
                    break;
            }
        }

        private bool ValidateInput()
        {
            if (nudAmount.Value <= 0)
            {
                MessageBox.Show("Please enter payment amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudAmount.Focus();
                return false;
            }

            if (nudAmount.Value > _balance)
            {
                MessageBox.Show("Payment amount cannot exceed balance.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudAmount.Focus();
                return false;
            }

            string method = cboPaymentMethod.SelectedItem.ToString();

            if ((method == "Credit Card" || method == "Debit Card") && 
                string.IsNullOrWhiteSpace(txtApprovalCode.Text))
            {
                MessageBox.Show("Please enter approval code for card payment.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApprovalCode.Focus();
                return false;
            }

            if (method == "Health Insurance" && 
                string.IsNullOrWhiteSpace(txtInsuranceProvider.Text))
            {
                MessageBox.Show("Please enter insurance provider.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInsuranceProvider.Focus();
                return false;
            }

            if (method == "Social Security" && 
                string.IsNullOrWhiteSpace(txtSocialSecurityNumber.Text))
            {
                MessageBox.Show("Please enter social security number.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSocialSecurityNumber.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                decimal paymentAmount = nudAmount.Value;
                string paymentMethod = cboPaymentMethod.SelectedItem.ToString();

                // Insert Payment
                string query = @"INSERT INTO Payments 
                               (InvoiceID, PaymentDate, PaymentMethod, Amount, ReferenceNumber,
                                InsuranceProvider, InsuranceClaimNumber, SocialSecurityNumber, 
                                ApprovalCode, Notes)
                               VALUES 
                               (@InvoiceID, @PaymentDate, @PaymentMethod, @Amount, @ReferenceNumber,
                                @InsuranceProvider, @InsuranceClaimNumber, @SocialSecurityNumber,
                                @ApprovalCode, @Notes)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId),
                    new SqlParameter("@PaymentDate", dtpPaymentDate.Value),
                    new SqlParameter("@PaymentMethod", paymentMethod),
                    new SqlParameter("@Amount", paymentAmount),
                    new SqlParameter("@ReferenceNumber", 
                        string.IsNullOrWhiteSpace(txtReferenceNumber.Text) ? (object)DBNull.Value : txtReferenceNumber.Text),
                    new SqlParameter("@InsuranceProvider", 
                        string.IsNullOrWhiteSpace(txtInsuranceProvider.Text) ? (object)DBNull.Value : txtInsuranceProvider.Text),
                    new SqlParameter("@InsuranceClaimNumber", 
                        string.IsNullOrWhiteSpace(txtInsuranceClaimNumber.Text) ? (object)DBNull.Value : txtInsuranceClaimNumber.Text),
                    new SqlParameter("@SocialSecurityNumber", 
                        string.IsNullOrWhiteSpace(txtSocialSecurityNumber.Text) ? (object)DBNull.Value : txtSocialSecurityNumber.Text),
                    new SqlParameter("@ApprovalCode", 
                        string.IsNullOrWhiteSpace(txtApprovalCode.Text) ? (object)DBNull.Value : txtApprovalCode.Text),
                    new SqlParameter("@Notes", 
                        string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Update Invoice PaidAmount and Status
                decimal newPaidAmount = decimal.Parse(txtPaidAmount.Text) + paymentAmount;
                decimal totalAmount = decimal.Parse(txtTotalAmount.Text);
                string newStatus = "Partial";

                if (newPaidAmount >= totalAmount)
                {
                    newStatus = "Paid";
                }

                query = @"UPDATE Invoices SET 
                         PaidAmount = @PaidAmount,
                         Status = @Status,
                         ModifiedDate = GETDATE()
                         WHERE InvoiceID = @InvoiceID";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId),
                    new SqlParameter("@PaidAmount", newPaidAmount),
                    new SqlParameter("@Status", newStatus)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Payment recorded successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error recording payment: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
