using System;

namespace HospitalMS.DAL.Models
{
    public class MedicalCertificate
    {
        public int CertificateID { get; set; }
        public string CertificateNumber { get; set; }  // MC-YYYYMMDD-XXXX
        public int PatientID { get; set; }
        public int? RecordID { get; set; }
        public DateTime IssueDate { get; set; }
        public string Diagnosis { get; set; }
        public string MedicalAdvice { get; set; }
        public int? SickLeaveDays { get; set; }
        public DateTime? SickLeaveFrom { get; set; }
        public DateTime? SickLeaveTo { get; set; }
        public int DoctorID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
