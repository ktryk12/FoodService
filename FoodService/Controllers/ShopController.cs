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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateShop([FromForm] CreateShopDto createShopDto)
        {
            try
            {
                var createdShop = await _shopService.CreateShopAsync(createShopDto);
                if (createdShop == null)
                {
                    return BadRequest("Unable to create shop");
                }

                // Returnerer den oprettede shop med den nye Id
                return Ok(createdShop);
            }
            catch (Exception ex)
            {
                // Log fejlen
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
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
        // GET: shop/{shopId}/salesitems
        [HttpGet("{shopId}/salesitems")]
        public async Task<IActionResult> GetSalesItemIdsByShopId(int shopId)
        {
            try
            {
                var salesItemIds = await _shopService.GetSalesItemIdsByShopIdAsync(shopId);
                if (salesItemIds == null || salesItemIds.Count == 0)
                {
                    return NotFound($"Ingen SalesItems fundet for ShopId: {shopId}.");
                }

                return Ok(salesItemIds);
            }
            catch (Exception ex)
            {
                // Log exception details here
                return StatusCode(500, "Der opstod en intern serverfejl.");
            }
        }

    }
}
