using System;

namespace HospitalMS.DAL.Models
{
    /// <summary>
    /// Patient Model - เก็บข้อมูลผู้ป่วย
    /// </summary>
    public class Patient
    {
        // Primary Key
        public int PatientID { get; set; }

        // Patient Information
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } // Male, Female, Other
        public string BloodGroup { get; set; } // A+, B+, O+, AB+, A-, B-, O-, AB-

        // Contact Information
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Emergency Contact
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }

        // Medical Information
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }

        // Metadata
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Computed Properties
        public string FullName => $"{FirstName} {LastName}";
        
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        // Constructor
        public Patient()
        {
            IsActive = true;
            RegistrationDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
    }
}
