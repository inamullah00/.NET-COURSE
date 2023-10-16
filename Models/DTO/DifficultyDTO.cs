using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.DTO
{
    public class DifficultyDTO
    {

     
        [Required]
        public string Name { get; set; }

    }
}
