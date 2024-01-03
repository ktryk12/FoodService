using FoodService.BusinessLogic.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.DTOs;

namespace FoodService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var result = await _ingredientService.GetIngredientByIdAsync(id);

            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var results = await _ingredientService.GetAllIngredientsAsync();
            return Ok(results);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateIngredient([FromForm] CreateIngredientDto createIngredientDto)
        {
            var createdIngredientDto = await _ingredientService.CreateIngredientAsync(createIngredientDto);

            if (createdIngredientDto != null)
            {
                return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredientDto.Id }, createdIngredientDto);
            }

            return BadRequest("Failed to create ingredient.");
        }






        [HttpPut]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDto ingredientDto)
        {
            var success = await _ingredientService.UpdateIngredientAsync(ingredientDto);

            if (success)
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var success = await _ingredientService.DeleteIngredientAsync(id);

            if (success)
                return NoContent();
            return NotFound();
        }
        // GET: /Ingredient/DetailsBySalesItemId/{salesItemId}
        [HttpGet("DetailsBySalesItemId/{salesItemId}")]
        public async Task<IActionResult> GetIngredientsWithDetailsBySalesItemId(int salesItemId)
        {
            try
            {
                var ingredientDetails = await _ingredientService.GetIngredientsWithDetailsBySalesItemIdAsync(salesItemId);
                if (ingredientDetails == null)
                {
                    return NotFound();
                }

                return Ok(ingredientDetails);
            }
            catch
            {
                // Her kan du tilføje mere detaljeret fejlhåndtering efter behov
                return StatusCode(500, "En intern serverfejl opstod");
            }
        }
    }
}
