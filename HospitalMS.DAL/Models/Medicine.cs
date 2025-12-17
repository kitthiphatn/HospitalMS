using System;

namespace HospitalMS.DAL.Models
{
    /// <summary>
    /// Medicine Model - เก็บข้อมูลยา
    /// </summary>
    public class Medicine
    {
        // Primary Key
        public int MedicineID { get; set; }

        // Medicine Information
        public string MedicineName { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public decimal UnitPrice { get; set; }

        // Stock Information
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Additional Information
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        // Computed Properties
        public bool IsLowStock => StockQuantity <= ReorderLevel;

        public bool IsExpiringSoon
        {
            get
            {
                if (!ExpiryDate.HasValue) return false;
                return ExpiryDate.Value <= DateTime.Now.AddMonths(3);
            }
        }

        public string StockStatus
        {
            get
            {
                if (StockQuantity == 0) return "Out of Stock";
                if (IsLowStock) return "Low Stock";
                return "In Stock";
            }
        }

        // Constructor
        public Medicine()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            StockQuantity = 0;
            ReorderLevel = 10;
            UnitPrice = 0;
        }
    }
}
