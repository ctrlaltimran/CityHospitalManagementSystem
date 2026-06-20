using System.ComponentModel.DataAnnotations;

namespace CityHospitalManagementSystem.Models
{
    public class Treatment
    {
        public int TreatmentId { get; set; }

        public int AdmissionId { get; set; }

        public DateTime TreatmentDate { get; set; }

        [Required]
        public string TreatmentDetails { get; set; } = string.Empty;

        public string? MedicineGiven { get; set; }

        public string? Notes { get; set; }

        public Admission? Admission { get; set; }
    }
}