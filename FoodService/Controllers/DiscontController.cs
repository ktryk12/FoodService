using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
using FoodService.Modellayer;
using Microsoft.AspNetCore.Authorization;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
      

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountById(int id)
        {
            var discount = await _discountService.GetDiscountByIdAsync(id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _discountService.GetAllDiscountsAsync();
            return Ok(discounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountDto discountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Assuming the service expects a DTO, no need to map to the domain entity.
                // var discount = _mapper.Map<Discount>(discountDto);

                var id = await _discountService.CreateDiscountAsync(discountDto);
                return CreatedAtAction(nameof(GetDiscountById), new { id }, discountDto);
            }
            catch (Exception ex)
            {
                // Log the exception here
                // ...

                // Return an appropriate error response, for example:
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, [FromBody] Discount discountToUpdate)
        {
            if (id != discountToUpdate.Id) return BadRequest();

            // Call the ToDto method directly without generic type arguments
            var discountDtoToUpdate = DiscountConverter.ToDto(discountToUpdate);
            var success = await _discountService.UpdateDiscountByIdAsync(discountDtoToUpdate);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var success = await _discountService.DeleteDiscountByIdAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
