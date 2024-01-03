using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public TokenController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("standard-token")]
        public async Task<IActionResult> GetStandardToken()
        {
            try
            {
                // Du kan specificere en brugeridentitet eller andet unikt identifikator her
                string token = await _jwtTokenService.GenerateJwtToken("StandardUser");
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // Log fejlen og returner en fejlbesked
                return BadRequest(ex.Message);
            }
        }
    }
}
