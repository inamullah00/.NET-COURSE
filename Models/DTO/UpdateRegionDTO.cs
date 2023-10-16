using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code Length Must be 3 Character ")]
        [MaxLength(3, ErrorMessage = "Code Length Must be 3 Character ")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be Maximum 100 Character Long")]

        [MinLength(4, ErrorMessage = "Name must be Minimum 4 Character Long")]
        public string Name { get; set; }

        //This is Optional Mean Nullable
        public string? RegionImageUrl { get; set; }

    }
}
