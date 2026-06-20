using System.ComponentModel.DataAnnotations;

namespace CityHospitalManagementSystem.Models
{
    public class Bed
    {
        public int BedId { get; set; }

        [Required]
        public string BedNumber { get; set; }

        public int WardId { get; set; }

        public bool IsOccupied { get; set; }

        public Ward Ward { get; set; }
    }
}