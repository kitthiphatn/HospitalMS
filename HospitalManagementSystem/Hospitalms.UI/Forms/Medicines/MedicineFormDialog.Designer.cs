namespace Hospitalms.UI.Forms.Medicines
{
    partial class MedicineFormDialog
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
            this.txtMedicineCode = new System.Windows.Forms.TextBox();
            this.MedicineCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.Category = new System.Windows.Forms.Label();
            this.Manufacturer = new System.Windows.Forms.Label();
            this.Price = new System.Windows.Forms.Label();
            this.Quantity = new System.Windows.Forms.Label();
            this.Level = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.txtStockQuantity = new System.Windows.Forms.TextBox();
            this.txtReorderLevel = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMedicineCode
            // 
            this.txtMedicineCode.Location = new System.Drawing.Point(94, 40);
            this.txtMedicineCode.Name = "txtMedicineCode";
            this.txtMedicineCode.ReadOnly = true;
            this.txtMedicineCode.Size = new System.Drawing.Size(269, 20);
            this.txtMedicineCode.TabIndex = 0;
            this.txtMedicineCode.TextChanged += new System.EventHandler(this.txtMedicineCode_TextChanged);
            // 
            // MedicineCode
            // 
            this.MedicineCode.AutoSize = true;
            this.MedicineCode.Location = new System.Drawing.Point(12, 43);
            this.MedicineCode.Name = "MedicineCode";
            this.MedicineCode.Size = new System.Drawing.Size(81, 13);
            this.MedicineCode.TabIndex = 1;
            this.MedicineCode.Text = "Medicine Code:";
            // 
            // Name
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(47, 89);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name :";
            // 
            // Category
            // 
            this.Category.AutoSize = true;
            this.Category.Location = new System.Drawing.Point(38, 130);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(55, 13);
            this.Category.TabIndex = 3;
            this.Category.Text = "Category :";
            // 
            // Manufacturer
            // 
            this.Manufacturer.AutoSize = true;
            this.Manufacturer.Location = new System.Drawing.Point(12, 169);
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.Size = new System.Drawing.Size(76, 13);
            this.Manufacturer.TabIndex = 4;
            this.Manufacturer.Text = "Manufacturer :";
            // 
            // Price
            // 
            this.Price.AutoSize = true;
            this.Price.Location = new System.Drawing.Point(12, 206);
            this.Price.Name = "Price";
            this.Price.Size = new System.Drawing.Size(37, 13);
            this.Price.TabIndex = 5;
            this.Price.Text = "Price :";
            // 
            // Quantity
            // 
            this.Quantity.AutoSize = true;
            this.Quantity.Location = new System.Drawing.Point(123, 208);
            this.Quantity.Name = "Quantity";
            this.Quantity.Size = new System.Drawing.Size(52, 13);
            this.Quantity.TabIndex = 6;
            this.Quantity.Text = "Quantity :";
            // 
            // Level
            // 
            this.Level.AutoSize = true;
            this.Level.Location = new System.Drawing.Point(253, 208);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(77, 13);
            this.Level.TabIndex = 7;
            this.Level.Text = "Reorder Level:";
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Location = new System.Drawing.Point(12, 248);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(66, 13);
            this.Description.TabIndex = 8;
            this.Description.Text = "Description :";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(65, 299);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 50);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(230, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(94, 89);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(269, 20);
            this.txtName.TabIndex = 12;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(94, 130);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(269, 21);
            this.cboCategory.TabIndex = 13;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Location = new System.Drawing.Point(94, 166);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(269, 20);
            this.txtManufacturer.TabIndex = 14;
            this.txtManufacturer.TextChanged += new System.EventHandler(this.txtManufacturer_TextChanged);
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUnitPrice.Location = new System.Drawing.Point(50, 203);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(67, 22);
            this.txtUnitPrice.TabIndex = 15;
            this.txtUnitPrice.TextChanged += new System.EventHandler(this.txtUnitPrice_TextChanged);
            // 
            // txtStockQuantity
            // 
            this.txtStockQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtStockQuantity.Location = new System.Drawing.Point(181, 203);
            this.txtStockQuantity.Name = "txtStockQuantity";
            this.txtStockQuantity.Size = new System.Drawing.Size(59, 22);
            this.txtStockQuantity.TabIndex = 16;
            this.txtStockQuantity.TextChanged += new System.EventHandler(this.txtStockQuantity_TextChanged);
            // 
            // txtReorderLevel
            // 
            this.txtReorderLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtReorderLevel.Location = new System.Drawing.Point(336, 203);
            this.txtReorderLevel.Name = "txtReorderLevel";
            this.txtReorderLevel.Size = new System.Drawing.Size(36, 22);
            this.txtReorderLevel.TabIndex = 17;
            this.txtReorderLevel.TextChanged += new System.EventHandler(this.txtReorderLevel_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(85, 248);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(266, 45);
            this.txtDescription.TabIndex = 18;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // MedicineFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtReorderLevel);
            this.Controls.Add(this.txtStockQuantity);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.Level);
            this.Controls.Add(this.Quantity);
            this.Controls.Add(this.Price);
            this.Controls.Add(this.Manufacturer);
            this.Controls.Add(this.Category);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.MedicineCode);
            this.Controls.Add(this.txtMedicineCode);
            this.Name = "MedicineFormDialog";
            this.Text = "MedicineFormDialog";
            this.Load += new System.EventHandler(this.MedicineFormDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMedicineCode;
        private System.Windows.Forms.Label MedicineCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label Category;
        private System.Windows.Forms.Label Manufacturer;
        private System.Windows.Forms.Label Price;
        private System.Windows.Forms.Label Quantity;
        private System.Windows.Forms.Label Level;
        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtStockQuantity;
        private System.Windows.Forms.TextBox txtReorderLevel;
        private System.Windows.Forms.TextBox txtDescription;
    }
}