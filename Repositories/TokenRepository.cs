using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace RESTAPI.Repositories
{
    public class TokenRepository : IJWTTokenRepository
    {
        private readonly IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public string createToken(IdentityUser user, List<string> roles)
        {
            // Create Claims

                        //this claim is created for email and now we create one for roles
                        //but not for password because it not recommend to add password inside claim
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));

                         foreach (var role in roles)
                        {
                            claims.Add(new Claim (ClaimTypes.Role, role));
                        }

            // Create a Semmetric Key        this line get the key from the appsetting file AND Encode the key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"])); 
   
             // create signin Credential

            var signCredential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            // Create JWT Token

            var token = new JwtSecurityToken(

                _configuration["jwt:Issuer"],
                _configuration["jwt:Audience"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:signCredential

                );

            // Serlize the Token JWT to String
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
