using System;

namespace HospitalMS.DAL.Models
{
    public class InvoiceItem
    {
        public int ItemID { get; set; }
        public int InvoiceID { get; set; }
        public string ItemType { get; set; }  // Service/Medicine/Lab/Other
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }  // ส่วนลดต่อรายการ (%)
        public decimal Amount { get; set; }  // Calculated: Quantity * UnitPrice * (1 - DiscountPercent/100)
        public int? MedicineID { get; set; }
        public bool IsActive { get; set; }
    }
}
