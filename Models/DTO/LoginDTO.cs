using System.ComponentModel.DataAnnotations;

namespace RESTAPI.Models.DTO
{
    public class LoginDTO
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
