using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop([FromBody] ShopDto shopDto)
        {
            var createdShop = await _shopService.CreateShopAsync(shopDto);
            if (createdShop != null)
                return CreatedAtAction(nameof(GetShopById), new { id = createdShop.Id }, createdShop);
            return BadRequest("Failed to create shop.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(int id)
        {
            var shop = await _shopService.GetShopByIdAsync(id);
            if (shop != null)
                return Ok(shop);
            return NotFound($"Shop with ID {id} not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShops()
        {
            var shops = await _shopService.GetAllShopsAsync();
            return Ok(shops);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShop([FromBody] ShopDto shopDto)
        {
            var success = await _shopService.UpdateShopAsync(shopDto);
            if (success)
                return Ok("Shop updated successfully.");
            return BadRequest("Failed to update shop.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var success = await _shopService.DeleteShopAsync(id);
            if (success)
                return Ok("Shop deleted successfully.");
            return NotFound($"Shop with ID {id} not found.");
        }

        [HttpPost("{shopId}/salesItem")]
        public async Task<IActionResult> AddSalesItemToShop(int shopId, [FromBody] SalesItemDto itemDto)
        {
            var success = await _shopService.AddSalesItemToShopAsync(shopId, itemDto);
            if (success)
                return Ok("Sales item added to shop successfully.");
            return BadRequest("Failed to add sales item to shop.");
        }

        [HttpDelete("{shopId}/salesItem/{salesItemId}")]
        public async Task<IActionResult> RemoveSalesItemFromShop(int shopId, int salesItemId)
        {
            var success = await _shopService.RemoveSalesItemFromShopAsync(shopId, salesItemId);
            if (success)
                return Ok("Sales item removed from shop successfully.");
            return NotFound($"Sales item with ID {salesItemId} not found in shop.");
        }
    }
}
