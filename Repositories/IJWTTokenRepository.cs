

using Microsoft.AspNetCore.Identity;

namespace RESTAPI.Repositories
{
    public interface IJWTTokenRepository
    {
        string createToken(IdentityUser user, List<string> roles);    
    }
}
                                                                        