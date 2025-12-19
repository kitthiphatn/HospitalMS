# Billing Management Module - Learning Guide

## Overview
บทเรียนนี้จะสอนวิธีสร้าง **Billing Management Module** สำหรับระบบโรงพยาบาล ประกอบด้วย 3 ฟอร์มหลัก:
1. **InvoiceListForm** - แสดงรายการใบแจ้งหนี้ทั้งหมด
2. **InvoiceFormDialog** - สร้าง/แก้ไขใบแจ้งหนี้
3. **PaymentFormDialog** - บันทึกการชำระเงิน

---

## Prerequisites (สิ่งที่ต้องเตรียมก่อน)

### 1. รัน SQL Script
ก่อนเริ่มสร้าง Forms ต้องรัน SQL script เพื่อสร้างตารางในฐานข้อมูลก่อน:

**ไฟล์:** `Database\13_Create_Billing_Tables.sql`

**วิธีรัน:**
1. เปิด SQL Server Management Studio (SSMS)
2. เปิดไฟล์ `13_Create_Billing_Tables.sql`
3. กด Execute (F5)

**ตารางที่จะถูกสร้าง:**
- `Invoices` - ใบแจ้งหนี้
- `InvoiceItems` - รายการในใบแจ้งหนี้
- `Payments` - การชำระเงิน
- `MedicalCertificates` - ใบรับรองแพทย์

### 2. Model Classes
ตรวจสอบว่ามี Model Classes ครบแล้ว:
- `Invoice.cs`
- `InvoiceItem.cs`
- `Payment.cs`
- `MedicalCertificate.cs`

อยู่ใน: `HospitalMS.DAL\Models`

---

## Part 1: InvoiceListForm (หน้าแสดงรายการใบแจ้งหนี้)

### 1.1 สร้าง Form ใหม่

**ขั้นตอน:**
1. คลิกขวาที่ `Hospitalms.UI\Forms`
2. Add → New Folder → ตั้งชื่อ `Billing`
3. คลิกขวาที่ folder `Billing`
4. Add → Form (Windows Forms)
5. ตั้งชื่อ: `InvoiceListForm.cs`

### 1.2 ออกแบบ UI (Designer)

**เปิด Designer:**
- ดับเบิลคลิกที่ `InvoiceListForm.cs` ใน Solution Explorer

**เพิ่ม Controls:**

#### Panel สำหรับค้นหา (panelSearch)
```
Properties:
- Name: panelSearch
- Dock: Top
- Height: 60
- BackColor: WhiteSmoke
```

> [!NOTE]
> **ลำดับการสร้าง Panel:** เมื่อใช้ `Dock: Top` หลาย Panels จะเรียงซ้อนกันจากบนลงล่าง
> ตามลำดับที่สร้าง Panel แรกจะอยู่บนสุด Panel ที่สองจะอยู่ถัดลงมา
> 
> **ลำดับที่ถูกต้อง:**
> 1. สร้าง `panelSearch` ก่อน (จะอยู่บนสุด)
> 2. สร้าง `panelActions` ทีหลัง (จะอยู่ถัดลงมา)
> 3. สร้าง `dgvInvoices` สุดท้าย (Dock: Fill จะเติมเต็มพื้นที่ที่เหลือ)

**เพิ่ม Controls ใน panelSearch:**
1. **Label (lblSearch)**
   - Text: "Search:"
   - Location: 20, 20

2. **TextBox (txtSearch)**
   - Name: txtSearch
   - Location: 80, 18
   - Size: 300, 20

3. **Button (btnSearch)**
   - Name: btnSearch
   - Text: "🔍 Search"
   - Location: 400, 15
   - Size: 100, 30
   - BackColor: DodgerBlue
   - ForeColor: White

4. **ComboBox (cboStatusFilter)**
   - Name: cboStatusFilter
   - Location: 520, 18
   - Size: 150, 20
   - Items: "All", "Unpaid", "Partial", "Paid", "Cancelled"
   - SelectedIndex: 0

#### Panel สำหรับปุ่มต่างๆ (panelActions)
```
Properties:
- Name: panelActions
- Dock: Top
- Height: 60
- BackColor: White
```

> [!TIP]
> Panel นี้จะอยู่ถัดจาก `panelSearch` ลงมา เพราะสร้างทีหลัง

**เพิ่ม Buttons:**
1. **btnAddInvoice**
   - Text: "+ New Invoice"
   - Location: 20, 12
   - Size: 150, 35
   - BackColor: #2ECC71 (Green)
   - ForeColor: White

2. **btnEdit**
   - Text: "✏️ Edit"
   - Location: 190, 12
   - Size: 100, 35
   - BackColor: Orange
   - ForeColor: White

3. **btnDelete**
   - Text: "🗑️ Delete"
   - Location: 310, 12
   - Size: 100, 35
   - BackColor: Red
   - ForeColor: White

4. **btnPreview**
   - Text: "📄 Preview"
   - Location: 430, 12
   - Size: 100, 35
   - BackColor: #3498DB (Blue)
   - ForeColor: White

5. **btnRecordPayment**
   - Text: "💰 Record Payment"
   - Location: 550, 12
   - Size: 150, 35
   - BackColor: #9B59B6 (Purple)
   - ForeColor: White

6. **btnRefresh**
   - Text: "🔄 Refresh"
   - Location: 720, 12
   - Size: 100, 35
   - BackColor: Gray
   - ForeColor: White

#### DataGridView (dgvInvoices)
```
Properties:
- Name: dgvInvoices
- Dock: Fill
- AllowUserToAddRows: False
- AllowUserToDeleteRows: False
- ReadOnly: True
- SelectionMode: FullRowSelect
- MultiSelect: False
- AutoSizeColumnsMode: Fill
- BackgroundColor: White
```

### 1.3 เขียน Code Logic

**เปิด Code Editor:**
- กด F7 หรือคลิกขวา → View Code

**Code สำหรับ InvoiceListForm.cs:**

```csharp
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
            // TODO: เปิด InvoiceFormDialog
            MessageBox.Show("Add Invoice - Coming soon!", "Info");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // TODO: เปิด InvoiceFormDialog แบบ Edit
            MessageBox.Show("Edit Invoice - Coming soon!", "Info");
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

            // TODO: เปิด InvoicePreviewForm
            MessageBox.Show("Preview Invoice - Coming soon!", "Info");
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

            // TODO: เปิด PaymentFormDialog
            MessageBox.Show("Record Payment - Coming soon!", "Info");
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
```

### 1.4 เชื่อม Event Handlers

**ใน Designer:**
1. เลือก `btnSearch` → Properties → Events (⚡) → Click → เลือก `btnSearch_Click`
2. เลือก `cboStatusFilter` → Events → SelectedIndexChanged → เลือก `cboStatusFilter_SelectedIndexChanged`
3. เลือก `btnAddInvoice` → Events → Click → เลือก `btnAddInvoice_Click`
4. เลือก `btnEdit` → Events → Click → เลือก `btnEdit_Click`
5. เลือก `btnDelete` → Events → Click → เลือก `btnDelete_Click`
6. เลือก `btnPreview` → Events → Click → เลือก `btnPreview_Click`
7. เลือก `btnRecordPayment` → Events → Click → เลือก `btnRecordPayment_Click`
8. เลือก `btnRefresh` → Events → Click → เลือก `btnRefresh_Click`
9. เลือก `txtSearch` → Events → KeyPress → เลือก `txtSearch_KeyPress`
10. เลือก Form → Events → Load → เลือก `InvoiceListForm_Load`

---

## Part 2: InvoiceFormDialog (หน้าสร้าง/แก้ไขใบแจ้งหนี้)

### 2.1 สร้าง Form Dialog

**ขั้นตอน:**
1. คลิกขวาที่ folder `Billing`
2. Add → Form (Windows Forms)
3. ตั้งชื่อ: `InvoiceFormDialog.cs`

### 2.2 ออกแบบ UI

**Form Properties:**
```
- FormBorderStyle: FixedDialog
- StartPosition: CenterScreen
- MaximizeBox: False
- MinimizeBox: False
- Size: 900, 700
- Text: "Invoice Form"
```

**เพิ่ม Controls:**

#### Panel ด้านบน (panelHeader)
```
- Dock: Top
- Height: 80
- BackColor: #2980B9
```

**Label (lblTitle):**
- Text: "Create/Edit Invoice"
- Font: Segoe UI, 18pt, Bold
- ForeColor: White
- Location: 20, 20

#### GroupBox (grpPatientInfo) - ข้อมูลผู้ป่วย
```
- Location: 20, 100
- Size: 850, 100
- Text: "Patient Information"
```

**Controls ใน grpPatientInfo:**
1. **Label:** "Patient:" (20, 30)
2. **ComboBox (cboPatient):** (100, 28), Size: 300, 21
3. **Label:** "Appointment:" (450, 30)
4. **ComboBox (cboAppointment):** (560, 28), Size: 250, 21

5. **Label:** "Invoice Date:" (20, 60)
6. **DateTimePicker (dtpInvoiceDate):** (120, 58), Size: 150, 21
7. **Label:** "Due Date:" (300, 60)
8. **DateTimePicker (dtpDueDate):** (380, 58), Size: 150, 21

#### GroupBox (grpItems) - รายการ
```
- Location: 20, 210
- Size: 850, 300
- Text: "Invoice Items"
```

**Panel (panelItemButtons):**
- Dock: Top
- Height: 50

**Buttons:**
1. **btnAddItem:** "+ Add Item" (10, 10), Green
2. **btnEditItem:** "Edit Item" (120, 10), Orange
3. **btnDeleteItem:** "Delete Item" (220, 10), Red

**DataGridView (dgvItems):**
- Dock: Fill
- Columns: ItemType, Description, Quantity, UnitPrice, Discount%, Amount

#### Panel (panelSummary) - สรุปยอด
```
- Location: 600, 520
- Size: 270, 150
- BackColor: WhiteSmoke
```

**Labels และ TextBoxes:**
1. SubTotal
2. Tax (7%)
3. Discount
4. **Total Amount** (Bold, Larger)

#### Panel (panelButtons) - ปุ่มบันทึก/ยกเลิก
```
- Dock: Bottom
- Height: 60
```

**Buttons:**
1. **btnSave:** "💾 Save" (Green)
2. **btnCancel:** "❌ Cancel" (Gray)

### 2.3 Code Logic (ตัวอย่างโครงสร้าง)

```csharp
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
            LoadInvoiceData();
        }
        else
        {
            GenerateInvoiceNumber();
        }
    }

    private void InitializeItemsDataTable()
    {
        _itemsTable = new DataTable();
        _itemsTable.Columns.Add("ItemType", typeof(string));
        _itemsTable.Columns.Add("Description", typeof(string));
        _itemsTable.Columns.Add("Quantity", typeof(int));
        _itemsTable.Columns.Add("UnitPrice", typeof(decimal));
        _itemsTable.Columns.Add("DiscountPercent", typeof(decimal));
        _itemsTable.Columns.Add("Amount", typeof(decimal));
        
        dgvItems.DataSource = _itemsTable;
    }

    private void btnAddItem_Click(object sender, EventArgs e)
    {
        // เปิด Dialog เพื่อเพิ่มรายการ
        // คำนวณ Amount = Quantity * UnitPrice * (1 - Discount/100)
        // เพิ่มลงใน _itemsTable
        // CalculateTotals();
    }

    private void CalculateTotals()
    {
        decimal subTotal = 0;
        foreach (DataRow row in _itemsTable.Rows)
        {
            subTotal += Convert.ToDecimal(row["Amount"]);
        }

        decimal tax = subTotal * 0.07m;
        decimal discount = 0; // จาก textbox
        decimal total = subTotal + tax - discount;

        // แสดงผลใน labels
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        // Validate
        // Save Invoice
        // Save InvoiceItems
        // Close dialog
    }
}
```

---

## Part 3: PaymentFormDialog (หน้าบันทึกการชำระเงิน)

### 3.1 สร้าง Form Dialog

**ขั้นตอน:**
1. คลิกขวาที่ folder `Billing`
2. Add → Form (Windows Forms)
3. ตั้งชื่อ: `PaymentFormDialog.cs`

### 3.2 ออกแบบ UI

**Form Properties:**
```
- FormBorderStyle: FixedDialog
- StartPosition: CenterScreen
- Size: 600, 550
- Text: "Record Payment"
```

**Controls:**

#### GroupBox (grpInvoiceInfo)
- แสดงข้อมูลใบแจ้งหนี้
- Invoice Number, Patient, Total Amount, Paid Amount, Balance

#### GroupBox (grpPaymentDetails)
**Controls:**
1. **DateTimePicker (dtpPaymentDate):** วันที่ชำระ
2. **ComboBox (cboPaymentMethod):** วิธีการชำระ
   - Items: Cash, Credit Card, Debit Card, Bank Transfer, PromptPay, Social Security, Health Insurance

3. **NumericUpDown (nudAmount):** จำนวนเงิน
4. **TextBox (txtReferenceNumber):** เลขที่อ้างอิง
5. **TextBox (txtApprovalCode):** รหัสอนุมัติ (สำหรับบัตร)
6. **TextBox (txtInsuranceProvider):** บริษัทประกัน
7. **TextBox (txtInsuranceClaimNumber):** เลขที่เคลม
8. **TextBox (txtSocialSecurityNumber):** เลขประกันสังคม
9. **TextBox (txtNotes):** หมายเหตุ

**Logic:**
- แสดง/ซ่อน fields ตาม Payment Method ที่เลือก
- Validate จำนวนเงินไม่เกิน Balance
- บันทึกลง Payments table
- อัปเดต PaidAmount และ Status ใน Invoices table

---

## Testing

### 1. ทดสอบ InvoiceListForm
- [ ] แสดงรายการใบแจ้งหนี้ทั้งหมด
- [ ] ค้นหาด้วยเลขที่ใบแจ้งหนี้
- [ ] กรองตามสถานะ
- [ ] สีแสดงสถานะถูกต้อง
- [ ] ลบใบแจ้งหนี้ได้

### 2. ทดสอบ InvoiceFormDialog
- [ ] สร้างใบแจ้งหนี้ใหม่
- [ ] เพิ่มรายการได้
- [ ] คำนวณยอดรวมถูกต้อง
- [ ] บันทึกข้อมูลสำเร็จ

### 3. ทดสอบ PaymentFormDialog
- [ ] บันทึกการชำระเงินได้
- [ ] อัปเดตสถานะใบแจ้งหนี้
- [ ] แสดง/ซ่อน fields ตาม Payment Method

---

## Next Steps

1. สร้าง InvoicePreviewForm
2. เพิ่ม Export to PDF
3. เพิ่ม Print Duplicate Receipt
4. สร้าง MedicalCertificateDialog
5. Integration กับ Dashboard

---

## Tips & Best Practices

> [!TIP]
> - ใช้ Transaction เมื่อบันทึก Invoice + Items พร้อมกัน
> - Validate ข้อมูลก่อนบันทึกทุกครั้ง
> - แสดง Loading indicator สำหรับการโหลดข้อมูล
> - ใช้ Try-Catch เพื่อจัดการ Error

> [!IMPORTANT]
> - ใบแจ้งหนี้ที่ชำระแล้วห้ามแก้ไข
> - ตรวจสอบยอดเงินที่ชำระไม่เกินยอดคงเหลือ
> - Invoice Number ต้อง unique

---

**สิ้นสุดบทเรียน Billing Management Module**

ถ้ามีคำถามหรือต้องการความช่วยเหลือเพิ่มเติม สามารถถามได้เลยครับ! 😊
