using System.ComponentModel.DataAnnotations;

namespace CityHospitalManagementSystem.Models
{
    public class Ward
    {
        public int WardId { get; set; }

        [Required]
        public string WardName { get; set; }

        [Required]
        public int TotalBeds { get; set; }
    }
}