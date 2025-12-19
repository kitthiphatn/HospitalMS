namespace HospitalMS.DAL.Models
{
    public class ChronicDisease
    {
        public int ChronicDiseaseID { get; set; }
        public int PatientID { get; set; }
        public string DiseaseName { get; set; }
        public System.DateTime? DiagnosedDate { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
