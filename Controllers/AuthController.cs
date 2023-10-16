using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models.DTO;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJWTTokenRepository _jwtTokenRepository;
        public AuthController(UserManager<IdentityUser> userManager , IJWTTokenRepository jwtTokenRepo) {
            _userManager = userManager;
            _jwtTokenRepository = jwtTokenRepo;
        }


        
        //Register 
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerData)
        {

            // First we take the Data from registerData
            var userIdentity = new IdentityUser
            {

                UserName = registerData.userName,
                Email = registerData.userName
            };

            // Then we create or register the User 
            var registerdResult = await _userManager.CreateAsync(userIdentity, registerData.Password);


            // then we check the user register or not successfully or not and then assign roles 
            if (registerdResult.Succeeded)
            {
                    //Data come from request Body 
                if (registerData.roles!= null && registerData.roles.Any())
                {

                    // Assign role to the registerd user

                    registerdResult = await _userManager.AddToRolesAsync(userIdentity, registerData.roles);

                    if (registerdResult.Succeeded)
                    {
                        return Ok("User Registerd Successfully!");
                        
                    }
                    else
                    {
                        return BadRequest("Failed to assign roles to the user.");
                    }

                }

            }

            return BadRequest(registerdResult);
        }

        //Login 

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO LoginDTOData)
        {
            // Check if the user exists by email

            var user = await _userManager.FindByEmailAsync(LoginDTOData.userName);

             if(user != null) {

                // Check if the password matches

                var checkPassword = await _userManager.CheckPasswordAsync(user, LoginDTOData.Password);

                if(checkPassword)
                {
                    // in the login action we have not roles for user so we can get role by the help of manager class
                    // Get the user's roles
                    var getUserRole = await _userManager.GetRolesAsync(user);


                    // Generate a token
                    var token =  _jwtTokenRepository.createToken(user, getUserRole.ToList());

                    if(token != null)
                    {
                        // we can send the token directly but it's not the recomended way so therefore we need to create DTO for response
                        // convert the token to DTO
                        var responseTokenDTO = new TokenResponseDTO
                        {
                            jwtToken = token,
                            Roles = getUserRole.ToList()
                        };
                        return Ok(responseTokenDTO);
                    }
                }
            }
            return BadRequest("Email or Password is Incorrect!");
        }




    }
}
