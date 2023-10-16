using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.DTO
{
    public class UpdateWalkDTO
    {

        [Required]
        [MaxLength(100, ErrorMessage = "Name Could be Maximum 100 Character Long")]
        [MinLength(4, ErrorMessage = "Name Could be Minimum 4 Character Long")]
        public string Name { get; set; }
        [Required]
        [MaxLength(2000, ErrorMessage = "Description Could be Maximum 2000 Character Long")]
        [MinLength(7, ErrorMessage = "Description Could be Minimum 7 Character Long")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50, ErrorMessage = "kilometer Could be in Range 0 to 50")]
        public double LengthInKm { get; set; }
        [Required]
        public string WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

    }
}
