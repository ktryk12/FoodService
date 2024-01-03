using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FoodService.DTOs;
using FoodService.BusinessLogic;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Tillad ikke-autoriserede brugere at logge ind

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminUserDto adminUserDto)
        {
            if (!await _adminService.ValidateAdminCredentials(adminUserDto))
            {
                return Unauthorized();
            }

            var token = await _adminService.GenerateJwtToken(adminUserDto.Username);
            return Ok(new { token });
        }

        // Tillad ikke-autoriserede brugere at registrere en ny admin
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdminUserDto adminUserDto)
        {
            if (string.IsNullOrWhiteSpace(adminUserDto.Username) || string.IsNullOrWhiteSpace(adminUserDto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            try
            {
                var result = await _adminService.CreateAdminUserAsync(adminUserDto);
                if (result != null)
                {
                    return CreatedAtAction(nameof(Login), new { username = result.Username });
                }
            }
            catch (InvalidOperationException ex)
            {
                // Log the exception if necessary
                return BadRequest(ex.Message);
            }

            return BadRequest("User registration failed.");
        }

        // Yderligere beskyttede admin-funktioner...
    }
}
