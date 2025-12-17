using System;

namespace HospitalMS.DAL.Models
{
    /// <summary>
    /// Appointment Model - เก็บข้อมูลการนัดหมาย
    /// </summary>
    public class Appointment
    {
        // Primary Key
        public int AppointmentID { get; set; }

        // Foreign Keys
        public int PatientID { get; set; }
        public int DoctorID { get; set; }

        // Appointment Information
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Completed, Cancelled
        public string Reason { get; set; }
        public string Notes { get; set; }

        // Metadata
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Navigation Properties (for display purposes)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        // Computed Properties
        public DateTime AppointmentDateTime
        {
            get
            {
                return AppointmentDate.Date.Add(AppointmentTime);
            }
        }

        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "Pending": return "รอยืนยัน";
                    case "Confirmed": return "ยืนยันแล้ว";
                    case "Completed": return "เสร็จสิ้น";
                    case "Cancelled": return "ยกเลิก";
                    default: return Status;
                }
            }
        }

        // Constructor
        public Appointment()
        {
            Status = "Pending";
            CreatedDate = DateTime.Now;
            AppointmentDate = DateTime.Today;
            AppointmentTime = new TimeSpan(9, 0, 0); // Default 9:00 AM
        }
    }
}
