using System.ComponentModel.DataAnnotations;

namespace CityHospitalManagementSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string MedicalHistory { get; set; }
    }
}