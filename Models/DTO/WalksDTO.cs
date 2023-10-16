using System.ComponentModel.DataAnnotations;
using RESTAPI.Models.Domain;

namespace RESTAPI.Models.DTO
{
    public class WalksDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        [Required]
        public string WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }


        //Navigation Property

        public DifficultyDTO Difficulty { get; set; }
        public RegionsDTO Region { get; set; }
    }
}
