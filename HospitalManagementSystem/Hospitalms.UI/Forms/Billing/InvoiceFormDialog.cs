using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalMS.DAL;

namespace Hospitalms.UI.Forms.Billing
{
    public partial class InvoiceFormDialog : Form
    {
        private int? _invoiceId = null;
        private bool _isEditMode = false;
        private DataTable _itemsTable;

        public InvoiceFormDialog(int? invoiceId = null)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            _isEditMode = invoiceId.HasValue;
        }

        private void InvoiceFormDialog_Load(object sender, EventArgs e)
        {
            InitializeItemsDataTable();
            LoadPatients();
            LoadAppointments();
            
            if (_isEditMode)
            {
                lblTitle.Text = "Edit Invoice";
                LoadInvoiceData();
            }
            else
            {
                lblTitle.Text = "Create New Invoice";
                dtpInvoiceDate.Value = DateTime.Now;
                dtpDueDate.Value = DateTime.Now.AddDays(7);
            }
        }

        private void InitializeItemsDataTable()
        {
            _itemsTable = new DataTable();
            _itemsTable.Columns.Add("ItemType", typeof(string));
            _itemsTable.Columns.Add("Description", typeof(string));
            _itemsTable.Columns.Add("Quantity", typeof(int));
            _itemsTable.Columns.Add("UnitPrice", typeof(decimal));
            _itemsTable.Columns.Add("Discount%", typeof(decimal));
            _itemsTable.Columns.Add("Amount", typeof(decimal));
            
            dgvItems.DataSource = _itemsTable;
            
            // ตั้งค่าคอลัมน์
            if (dgvItems.Columns.Count > 0)
            {
                dgvItems.Columns["Amount"].DefaultCellStyle.Format = "N2";
                dgvItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            }
        }

        private void LoadPatients()
        {
            try
            {
                string query = @"SELECT PatientID, FirstName + ' ' + LastName AS FullName 
                               FROM Patients WHERE IsActive = 1 
                               ORDER BY FirstName";
                
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);
                
                cboPatient.DisplayMember = "FullName";
                cboPatient.ValueMember = "PatientID";
                cboPatient.DataSource = dt;
                cboPatient.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAppointments()
        {
            try
            {
                string query = @"SELECT AppointmentID, 
                               CONVERT(VARCHAR, AppointmentDate, 103) + ' ' + 
                               CONVERT(VARCHAR, AppointmentTime, 108) AS AppointmentInfo
                               FROM Appointments 
                               ORDER BY AppointmentDate DESC, AppointmentTime DESC";
                
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);
                
                // เพิ่ม empty row
                DataRow emptyRow = dt.NewRow();
                emptyRow["AppointmentID"] = DBNull.Value;
                emptyRow["AppointmentInfo"] = "-- No Appointment --";
                dt.Rows.InsertAt(emptyRow, 0);
                
                cboAppointment.DisplayMember = "AppointmentInfo";
                cboAppointment.ValueMember = "AppointmentID";
                cboAppointment.DataSource = dt;
                cboAppointment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceData()
        {
            try
            {
                // Load Invoice Header
                string query = @"SELECT * FROM Invoices WHERE InvoiceID = @InvoiceID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId)
                };
                
                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cboPatient.SelectedValue = row["PatientID"];
                    
                    if (row["AppointmentID"] != DBNull.Value)
                        cboAppointment.SelectedValue = row["AppointmentID"];
                    
                    dtpInvoiceDate.Value = Convert.ToDateTime(row["InvoiceDate"]);
                    
                    if (row["DueDate"] != DBNull.Value)
                        dtpDueDate.Value = Convert.ToDateTime(row["DueDate"]);
                    
                    txtDiscount.Text = row["DiscountAmount"].ToString();
                }
                
                // Load Invoice Items
                query = @"SELECT ItemType, ItemDescription AS Description, Quantity, 
                         UnitPrice, DiscountPercent AS [Discount%], Amount 
                         FROM InvoiceItems 
                         WHERE InvoiceID = @InvoiceID AND IsActive = 1";
                
                // Create new parameter array for second query
                SqlParameter[] itemsParameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", _invoiceId)
                };
                
                DataTable itemsDt = DatabaseHelper.ExecuteDataTable(query, itemsParameters);
                
                foreach (DataRow itemRow in itemsDt.Rows)
                {
                    _itemsTable.ImportRow(itemRow);
                }
                
                CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // สร้าง simple input dialog
            using (Form inputForm = new Form())
            {
                inputForm.Text = "Add Item";
                inputForm.Size = new System.Drawing.Size(400, 300);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                Label lblType = new Label() { Left = 20, Top = 20, Text = "Type:", Width = 100 };
                ComboBox cboType = new ComboBox() { Left = 130, Top = 18, Width = 200 };
                cboType.Items.AddRange(new string[] { "Service", "Medicine", "Lab", "Other" });
                cboType.SelectedIndex = 0;

                Label lblDesc = new Label() { Left = 20, Top = 50, Text = "Description:", Width = 100 };
                TextBox txtDesc = new TextBox() { Left = 130, Top = 48, Width = 200 };

                Label lblQty = new Label() { Left = 20, Top = 80, Text = "Quantity:", Width = 100 };
                NumericUpDown nudQty = new NumericUpDown() { Left = 130, Top = 78, Width = 200, Minimum = 1, Value = 1 };

                Label lblPrice = new Label() { Left = 20, Top = 110, Text = "Unit Price:", Width = 100 };
                TextBox txtPrice = new TextBox() { Left = 130, Top = 108, Width = 200, Text = "0.00" };

                Label lblDiscount = new Label() { Left = 20, Top = 140, Text = "Discount %:", Width = 100 };
                NumericUpDown nudDiscount = new NumericUpDown() { Left = 130, Top = 138, Width = 200, Minimum = 0, Maximum = 100, Value = 0, DecimalPlaces = 2 };

                Button btnOK = new Button() { Text = "Add", Left = 130, Width = 90, Top = 200, DialogResult = DialogResult.OK };
                Button btnCancelInput = new Button() { Text = "Cancel", Left = 240, Width = 90, Top = 200, DialogResult = DialogResult.Cancel };

                inputForm.Controls.Add(lblType);
                inputForm.Controls.Add(cboType);
                inputForm.Controls.Add(lblDesc);
                inputForm.Controls.Add(txtDesc);
                inputForm.Controls.Add(lblQty);
                inputForm.Controls.Add(nudQty);
                inputForm.Controls.Add(lblPrice);
                inputForm.Controls.Add(txtPrice);
                inputForm.Controls.Add(lblDiscount);
                inputForm.Controls.Add(nudDiscount);
                inputForm.Controls.Add(btnOK);
                inputForm.Controls.Add(btnCancelInput);

                inputForm.AcceptButton = btnOK;
                inputForm.CancelButton = btnCancelInput;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(txtDesc.Text))
                    {
                        MessageBox.Show("Please enter description.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    decimal unitPrice = 0;
                    if (!decimal.TryParse(txtPrice.Text, out unitPrice) || unitPrice < 0)
                    {
                        MessageBox.Show("Please enter valid unit price.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int quantity = (int)nudQty.Value;
                    decimal discountPercent = nudDiscount.Value;
                    decimal amount = quantity * unitPrice * (1 - discountPercent / 100);

                    DataRow newRow = _itemsTable.NewRow();
                    newRow["ItemType"] = cboType.SelectedItem.ToString();
                    newRow["Description"] = txtDesc.Text;
                    newRow["Quantity"] = quantity;
                    newRow["UnitPrice"] = unitPrice;
                    newRow["Discount%"] = discountPercent;
                    newRow["Amount"] = amount;

                    _itemsTable.Rows.Add(newRow);
                    CalculateTotals();
                }
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // TODO: Implement edit item dialog (similar to Add)
            MessageBox.Show("Edit Item - To be implemented", "Info");
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this item?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int rowIndex = dgvItems.SelectedRows[0].Index;
                _itemsTable.Rows[rowIndex].Delete();
                CalculateTotals();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            decimal subTotal = 0;
            
            foreach (DataRow row in _itemsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    subTotal += Convert.ToDecimal(row["Amount"]);
                }
            }

            decimal tax = subTotal * 0.07m;
            
            decimal discount = 0;
            decimal.TryParse(txtDiscount.Text, out discount);
            
            decimal total = subTotal + tax - discount;

            txtSubTotal.Text = subTotal.ToString("N2");
            txtTax.Text = tax.ToString("N2");
            txtTotal.Text = total.ToString("N2");
        }

        private bool ValidateInput()
        {
            if (cboPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPatient.Focus();
                return false;
            }

            if (_itemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                int patientId = Convert.ToInt32(cboPatient.SelectedValue);
                int? appointmentId = cboAppointment.SelectedValue == DBNull.Value ? 
                    (int?)null : Convert.ToInt32(cboAppointment.SelectedValue);
                
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal tax = decimal.Parse(txtTax.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);
                decimal total = decimal.Parse(txtTotal.Text);

                if (_isEditMode)
                {
                    // Update Invoice
                    string query = @"UPDATE Invoices SET 
                                   PatientID = @PatientID,
                                   AppointmentID = @AppointmentID,
                                   InvoiceDate = @InvoiceDate,
                                   DueDate = @DueDate,
                                   SubTotal = @SubTotal,
                                   TaxAmount = @TaxAmount,
                                   DiscountAmount = @DiscountAmount,
                                   TotalAmount = @TotalAmount,
                                   ModifiedDate = GETDATE()
                                   WHERE InvoiceID = @InvoiceID";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@InvoiceID", _invoiceId),
                        new SqlParameter("@PatientID", patientId),
                        new SqlParameter("@AppointmentID", (object)appointmentId ?? DBNull.Value),
                        new SqlParameter("@InvoiceDate", dtpInvoiceDate.Value),
                        new SqlParameter("@DueDate", dtpDueDate.Value),
                        new SqlParameter("@SubTotal", subTotal),
                        new SqlParameter("@TaxAmount", tax),
                        new SqlParameter("@DiscountAmount", discount),
                        new SqlParameter("@TotalAmount", total)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);

                    // Delete old items
                    query = "DELETE FROM InvoiceItems WHERE InvoiceID = @InvoiceID";
                    DatabaseHelper.ExecuteNonQuery(query, new SqlParameter[] { new SqlParameter("@InvoiceID", _invoiceId) });

                    // Insert new items
                    SaveInvoiceItems(_invoiceId.Value);

                    MessageBox.Show("Invoice updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Generate Invoice Number
                    string invoiceNumber = GenerateInvoiceNumber();

                    // Insert Invoice
                    string query = @"INSERT INTO Invoices 
                                   (InvoiceNumber, PatientID, AppointmentID, InvoiceDate, DueDate, 
                                    SubTotal, TaxAmount, DiscountAmount, TotalAmount, Status)
                                   VALUES 
                                   (@InvoiceNumber, @PatientID, @AppointmentID, @InvoiceDate, @DueDate,
                                    @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, 'Unpaid');
                                   SELECT SCOPE_IDENTITY();";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@InvoiceNumber", invoiceNumber),
                        new SqlParameter("@PatientID", patientId),
                        new SqlParameter("@AppointmentID", (object)appointmentId ?? DBNull.Value),
                        new SqlParameter("@InvoiceDate", dtpInvoiceDate.Value),
                        new SqlParameter("@DueDate", dtpDueDate.Value),
                        new SqlParameter("@SubTotal", subTotal),
                        new SqlParameter("@TaxAmount", tax),
                        new SqlParameter("@DiscountAmount", discount),
                        new SqlParameter("@TotalAmount", total)
                    };

                    object result = DatabaseHelper.ExecuteScalar(query, parameters);
                    int newInvoiceId = Convert.ToInt32(result);

                    // Insert Items
                    SaveInvoiceItems(newInvoiceId);

                    MessageBox.Show("Invoice created successfully!\nInvoice Number: " + invoiceNumber, 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving invoice: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveInvoiceItems(int invoiceId)
        {
            foreach (DataRow row in _itemsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    string query = @"INSERT INTO InvoiceItems 
                                   (InvoiceID, ItemType, ItemDescription, Quantity, UnitPrice, DiscountPercent, Amount)
                                   VALUES 
                                   (@InvoiceID, @ItemType, @Description, @Quantity, @UnitPrice, @DiscountPercent, @Amount)";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@InvoiceID", invoiceId),
                        new SqlParameter("@ItemType", row["ItemType"]),
                        new SqlParameter("@Description", row["Description"]),
                        new SqlParameter("@Quantity", row["Quantity"]),
                        new SqlParameter("@UnitPrice", row["UnitPrice"]),
                        new SqlParameter("@DiscountPercent", row["Discount%"]),
                        new SqlParameter("@Amount", row["Amount"])
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }
            }
        }

        private string GenerateInvoiceNumber()
        {
            string datePrefix = DateTime.Now.ToString("yyyyMMdd");
            string query = @"SELECT COUNT(*) FROM Invoices 
                           WHERE InvoiceNumber LIKE 'INV-" + datePrefix + "-%'";
            
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
            int nextNumber = count + 1;
            
            return $"INV-{datePrefix}-{nextNumber:D4}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
