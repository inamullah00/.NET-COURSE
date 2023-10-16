using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.Domain
{
    public class Walk
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



//-- Navigation Property for the relationship ES se Entity Framework ko pata chalta hai k walk ka relation hai region se or difficulty se
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

    }
}
