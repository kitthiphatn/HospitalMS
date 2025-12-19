namespace HospitalMS.DAL.Models
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int? AppointmentID { get; set; }
        public System.DateTime VisitDate { get; set; }
        public string ChiefComplaint { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
        public int DoctorID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
