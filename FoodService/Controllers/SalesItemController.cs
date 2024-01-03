using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Dto_sConverter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemController : ControllerBase
    {
        private readonly ISalesItemService _salesItemService;

        public SalesItemController(ISalesItemService salesItemService)
        {
            _salesItemService = salesItemService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateSalesItem([FromForm] CreateSalesItemDto createSalesItemDto)
        {
            var createdSalesItemDto = await _salesItemService.CreateSalesItemAsync(createSalesItemDto);

            if (createdSalesItemDto != null)
            {
                return CreatedAtAction(nameof(GetSalesItemById), new { id = createdSalesItemDto.Id }, createdSalesItemDto);
            }

            return BadRequest("Failed to create sales item.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesItemById(int id)
        {
            var salesItem = await _salesItemService.GetSalesItemByIdAsync(id);
            if (salesItem == null)
            {
                return NotFound($"Sales item with ID {id} not found.");
            }

            var salesItemDto = SalesItemConverter.ToDto(salesItem);
            return Ok(salesItemDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesItems()
        {
            var salesItems = await _salesItemService.GetAllSalesItemsAsync();
            return Ok(salesItems);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesItem([FromBody] SalesItemDto salesItemDto)
        {
            var success = await _salesItemService.UpdateSalesItemAsync(salesItemDto);
            if (success)
                return Ok("Sales item updated successfully.");
            return BadRequest("Failed to update sales item.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesItem(int id)
        {
            var success = await _salesItemService.DeleteSalesItemAsync(id);
            if (success)
                return Ok("Sales item deleted successfully.");
            return NotFound($"Sales item with ID {id} not found.");
        }

        [HttpGet("ByCategory/{category}")]
        public async Task<IActionResult> GetSalesItemsByCategory(string category)
        {
            var salesItems = await _salesItemService.GetSalesItemsByCategoryAsync(category);
            if (salesItems == null)
            {
                return NotFound($"Ingen salgsitems fundet i kategorien '{category}'.");
            }

            return Ok(salesItems);
        }
        [HttpGet("ByIsComposite/{isComposite}")]
        public async Task<IActionResult> GetSalesItemsByComposite(bool isComposite)
        {
            var salesItems = await _salesItemService.GetSalesItemsByIsCompositeAsync(isComposite);
            if (salesItems == null)
            {
                return NotFound($"Ingen salgsitems fundet i kategorien '{isComposite}'.");
            }

            return Ok(salesItems);
        }
    }
}
