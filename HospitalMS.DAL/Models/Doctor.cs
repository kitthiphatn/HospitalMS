using System;

namespace HospitalMS.DAL.Models
{
    /// <summary>
    /// Doctor Model - เก็บข้อมูลแพทย์
    /// </summary>
    public class Doctor
    {
        // Primary Key
        public int DoctorID { get; set; }

        // Doctor Information
        public string DoctorCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }

        // Contact Information
        public string Phone { get; set; }
        public string Email { get; set; }

        // Professional Information
        public string LicenseNumber { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsAvailable { get; set; }

        // Metadata
        public DateTime JoinDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Computed Properties
        public string FullName => $"{FirstName} {LastName}";
        
        public string DisplayName => $"Dr. {FirstName} {LastName}";

        // Constructor
        public Doctor()
        {
            IsActive = true;
            IsAvailable = true;
            JoinDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            ConsultationFee = 0;
        }
    }
}
