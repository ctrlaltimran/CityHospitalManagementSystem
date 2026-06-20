using System.ComponentModel.DataAnnotations;

namespace CityHospitalManagementSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        public string AssignedWard { get; set; }

        public bool IsAvailable { get; set; }

        public string ConsultationSchedule { get; set; }
    }
}