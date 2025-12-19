namespace HospitalMS.DAL.Models
{
    public class Allergy
    {
        public int AllergyID { get; set; }
        public int PatientID { get; set; }
        public string AllergyType { get; set; }
        public string AllergyName { get; set; }
        public string Reaction { get; set; }
        public string Severity { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
