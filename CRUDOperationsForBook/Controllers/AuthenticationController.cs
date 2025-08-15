using CRUDOperationsForBook.DTOs;
using CRUDOperationsForBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser appUser = new AppUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };
            var existingUser = await userManager.FindByNameAsync(registerDTO.UserName);
            if (existingUser != null)
                return BadRequest("Username already exists");

            var result = await userManager.CreateAsync(appUser, registerDTO.Password);
            if(result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }
        }


        //[HttpPost]
        //public IActionResult Login(LoginDTO loginDTO)
        //{
            
        //}
    }
}
