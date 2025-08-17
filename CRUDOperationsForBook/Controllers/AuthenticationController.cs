using CRUDOperationsForBook.DTOs;
using CRUDOperationsForBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDOperationsForBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<AppUser> userManager;

        public AuthenticationController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser appUser = new AppUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };

            var existingEmail = await userManager.FindByEmailAsync(registerDTO.Email);
            var existingUserName = await userManager.FindByNameAsync(registerDTO.UserName);
            if (existingEmail != null)
                return BadRequest("Email already exists");
            if (existingUserName != null)
                return BadRequest("Username already exists");

            var result = await userManager.CreateAsync(appUser, registerDTO.Password);
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO userFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check if the user exists
            AppUser userFromDb = await userManager.FindByNameAsync(userFromRequest.UserName);
            var isPasswordValid = await userManager.CheckPasswordAsync(userFromDb, userFromRequest.Password);
            if (userFromDb != null && isPasswordValid == true)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,userFromDb.Id),
                    new Claim(ClaimTypes.Name, userFromDb.UserName),
                    new Claim(ClaimTypes.Email, userFromDb.Email),
                    new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString()),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AFGeih987$#5925d,fkjhkdkdl,xkf&/532"));
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //Generate the token
                JwtSecurityToken myToken = new JwtSecurityToken(
                    issuer: "http://localhost:46926/",
                    audience: "http://localhost:4200/",
                    expires: DateTime.UtcNow.AddHours(2),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

                //generate the token response
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(myToken),
                    expiration = myToken.ValidTo
                });
            }
            else
            {
                return BadRequest("Invalid email or password");
            }
        }

    }
}
