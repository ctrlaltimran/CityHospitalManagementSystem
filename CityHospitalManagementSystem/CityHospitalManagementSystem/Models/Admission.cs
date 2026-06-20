using System.ComponentModel.DataAnnotations;

namespace CityHospitalManagementSystem.Models
{
    public class Admission
    {
        public int AdmissionId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int WardId { get; set; }

        public int BedId { get; set; }

        [Required]
        public string AdmissionType { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public string Status { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public Ward Ward { get; set; }

        public Bed Bed { get; set; }
    }
}