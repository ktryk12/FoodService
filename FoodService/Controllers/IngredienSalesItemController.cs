using Microsoft.AspNetCore.Mvc;

using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientSalesItemController : ControllerBase
    {
        private readonly IIngredientSalesItemService _ingredientSalesItemService;

        public IngredientSalesItemController(IIngredientSalesItemService ingredientSalesItemService)
        {
            _ingredientSalesItemService = ingredientSalesItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredientToSalesItem([FromBody] IngredientSalesItemDto ingredientSalesItemDto)
        {
            var success = await _ingredientSalesItemService.AddIngredientToSalesItemAsync(ingredientSalesItemDto);
            if (success)
                return Ok("Ingredient added to sales item successfully.");
            return BadRequest("Failed to add ingredient to sales item.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredientSalesItem([FromBody] IngredientSalesItemDto ingredientSalesItemDto)
        {
            var success = await _ingredientSalesItemService.UpdateIngredientSalesItemAsync(ingredientSalesItemDto);
            if (success)
                return Ok("Ingredient sales item updated successfully.");
            return BadRequest("Failed to update ingredient sales item.");
        }

        [HttpDelete("{salesItemId}/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredientFromSalesItem(int salesItemId, int ingredientId)
        {
            var success = await _ingredientSalesItemService.RemoveIngredientFromSalesItemAsync(salesItemId, ingredientId);
            if (success)
                return Ok("Ingredient removed from sales item successfully.");
            return NotFound("Ingredient sales item not found.");
        }

        [HttpPost("addOrUpdate")]
        public async Task<IActionResult> AddOrUpdateIngredientToSalesItem([FromBody] IngredientSalesItemDto ingredientSalesItemDto)
        {
            var success = await _ingredientSalesItemService.AddOrUpdateIngredientToSalesItemAsync(ingredientSalesItemDto);
            if (success)
                return Ok("Ingredient added or updated in sales item successfully.");
            return BadRequest("Failed to add or update ingredient in sales item.");
        }

        [HttpGet("{salesItemId}/{ingredientId}")]
        public async Task<IActionResult> GetIngredientSalesItemById(int salesItemId, int ingredientId)
        {
            var ingredientSalesItem = await _ingredientSalesItemService.GetIngredientSalesItemByIdAsync(salesItemId, ingredientId);
            if (ingredientSalesItem != null)
                return Ok(ingredientSalesItem);
            return NotFound("Ingredient sales item not found.");
        }
        [HttpGet("bySalesItem/{salesItemId}")]
        public async Task<IActionResult> GetAllBySalesItemId(int salesItemId)
        {
            var ingredientSalesItems = await _ingredientSalesItemService.GetAllBySalesItemIdAsync(salesItemId);
            if (ingredientSalesItems != null)
                return Ok(ingredientSalesItems);
            return NotFound("No ingredient sales items found for the given sales item.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingredientSalesItems = await _ingredientSalesItemService.GetAllAsync();
            if (ingredientSalesItems != null)
                return Ok(ingredientSalesItems);
            return NotFound("No ingredient sales items found.");
        }
    }
}
    


