using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.DTO
{
    public class DeleteRegionDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
