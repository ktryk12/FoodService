using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FoodService.DTOs;
using FoodService.BusinessLogic.PriceService;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpGet("calculate-price/{salesItemId}")]
        public async Task<ActionResult<decimal>> CalculatePrice(int salesItemId)
        {
            try
            {
                var price = await _pricingService.CalculatePriceAsync(salesItemId);
                return Ok(price);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("calculate-custom-price")]
        public async Task<ActionResult<decimal>> CalculateCustomPrice(int salesItemId, [FromBody] Dictionary<int, int> ingredientQuantities)
        {
            if (ingredientQuantities == null || ingredientQuantities.Count == 0)
            {
                return BadRequest("Ingredient quantities are required.");
            }

            try
            {
                var price = await _pricingService.CalculateCustomPriceAsync(salesItemId, ingredientQuantities);
                return Ok(price);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Eller BadRequest afhængig af din forretningslogik
            }
            catch (Exception ex)
            {
                // Log fejlen for internt brug
                // Overvej at returnere en mere generisk fejlbesked til klienten
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }

}
