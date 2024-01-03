using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderlineController : ControllerBase
    {
        private readonly IOrderlineService _orderlineService;

        public OrderlineController(IOrderlineService orderlineService)
        {
            _orderlineService = orderlineService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderline([FromBody] OrderlineDto orderlineDto)
        {
            var createdOrderline = await _orderlineService.CreateOrderlineAsync(orderlineDto);
            if (createdOrderline != null)
                return CreatedAtAction(nameof(GetOrderlineById), new { id = createdOrderline.Id }, createdOrderline);
            return BadRequest("Failed to create orderline.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderlineById(int id)
        {
            var orderline = await _orderlineService.GetOrderlineByIdAsync(id);
            if (orderline != null)
                return Ok(orderline);
            return NotFound($"Orderline with ID {id} not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderlines()
        {
            var orderlines = await _orderlineService.GetAllOrderlinesAsync();
            return Ok(orderlines);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderline([FromBody] OrderlineDto orderlineDto)
        {
            var success = await _orderlineService.UpdateOrderlineAsync(orderlineDto);
            if (success)
                return Ok("Orderline updated successfully.");
            return BadRequest("Failed to update orderline.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderline(int id)
        {
            var success = await _orderlineService.DeleteOrderlineAsync(id);
            if (success)
                return Ok("Orderline deleted successfully.");
            return NotFound($"Orderline with ID {id} not found.");
        }
    }
}

