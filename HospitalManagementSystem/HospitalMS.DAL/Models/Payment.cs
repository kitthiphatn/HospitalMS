using System;

namespace HospitalMS.DAL.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int InvoiceID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }  // Cash/Credit Card/Debit Card/Bank Transfer/PromptPay/Social Security/Health Insurance
        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; }  // Transaction ID, Transfer Number
        public string InsuranceProvider { get; set; }  // ชื่อบริษัทประกัน
        public string InsuranceClaimNumber { get; set; }  // เลขที่เคลม
        public string SocialSecurityNumber { get; set; }  // เลขประกันสังคม
        public string ApprovalCode { get; set; }  // รหัสอนุมัติบัตรเครดิต
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
