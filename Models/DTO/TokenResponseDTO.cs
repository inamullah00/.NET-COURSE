namespace RESTAPI.Models.DTO
{
    public class TokenResponseDTO
    {
        public string jwtToken { get; set; }
        public List<string> Roles { get; set; }
    }
}
