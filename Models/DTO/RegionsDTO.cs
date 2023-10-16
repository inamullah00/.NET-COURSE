using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.DTO
{
    public class RegionsDTO
    {

   
            [Required]
            public Guid Id { get; set; }
            [Required]
            public string Code { get; set; }
            [Required]
            public string Name { get; set; }

            public string? RegionImageUrl { get; set; }

        }
    }




