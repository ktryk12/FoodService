using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientOrderlineController : ControllerBase
    {
        private readonly IIngredientOrderlineService _ingredientOrderlineService;

        public IngredientOrderlineController(IIngredientOrderlineService ingredientOrderlineService)
        {
            _ingredientOrderlineService = ingredientOrderlineService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIngredientOrderline([FromBody] IngredientOrderlineDto ingredientOrderlineDto)
        {
            var result = await _ingredientOrderlineService.CreateIngredientOrderlineAsync(ingredientOrderlineDto);
            if (result.HasValue)
                return CreatedAtAction(nameof(GetIngredientOrderlineById), new { id = result.Value }, ingredientOrderlineDto);
            return BadRequest("Failed to create ingredient orderline.");
        }

        [HttpGet("{orderlineId}/{ingredientId}")]
        public async Task<IActionResult> GetIngredientOrderlineById(int orderlineId, int ingredientId)
        {
            var idDto = new IngredientOrderlineDto { OrderlineId = orderlineId, IngredientId = ingredientId };
            var result = await _ingredientOrderlineService.GetIngredientOrderlineByIdAsync(idDto);
            if (result != null)
                return Ok(result);
            return NotFound($"IngredientOrderline with OrderlineId {orderlineId} and IngredientId {ingredientId} not found.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateIngredientOrderline([FromBody] IngredientOrderlineDto ingredientOrderlineDto)
        {
            var success = await _ingredientOrderlineService.UpdateIngredientOrderlineAsync(ingredientOrderlineDto);
            if (success)
                return Ok();
            return BadRequest("Failed to update ingredient orderline.");
        }

        [HttpDelete("{orderlineId}/{ingredientId}")]
        public async Task<IActionResult> DeleteIngredientOrderline(int orderlineId, int ingredientId)
        {
            var success = await _ingredientOrderlineService.DeleteIngredientOrderlineAsync(orderlineId, ingredientId);
            if (success)
                return Ok();
            return NotFound($"IngredientOrderline with OrderlineId {orderlineId} and IngredientId {ingredientId} not found.");
        }

        [HttpPost("removeIngredients")]
        public async Task<IActionResult> RemoveIngredientsFromOrderline([FromBody] IngredientOrderlineDto ingredientOrderlinesDto)
        {
            await _ingredientOrderlineService.RemoveIngredientsFromOrderline(ingredientOrderlinesDto);
            return Ok();
        }

        [HttpPost("addIngredients")]
        public async Task<IActionResult> AddIngredientsToOrderline([FromBody] IngredientOrderlineDto ingredientOrderlinesDto)
        {
            await _ingredientOrderlineService.AddIngredientsToOrderline(ingredientOrderlinesDto);
            return Ok();
        }
    }
}
