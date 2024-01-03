using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create/{shopId}")]
        public async Task<IActionResult> CreateOrder(int shopId)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(shopId);
                if (result.Id.HasValue)
                    return CreatedAtAction(nameof(GetOrderById), new { id = result.Id.Value }, result);
                return BadRequest("Failed to create order.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null)
                return Ok(order);
            return NotFound($"Order with ID {id} not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            IEnumerable<OrderDto> orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto)
        {
            var success = await _orderService.UpdateOrderAsync(orderDto);
            if (success)
                return Ok("Order updated successfully.");
            return BadRequest("Failed to update order.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var success = await _orderService.DeleteOrderAsync(id);
            if (success)
                return Ok("Order deleted successfully.");
            return NotFound($"Order with ID {id} not found.");
        }

        [HttpPut("updateTotal/{orderId}")]
        public async Task<IActionResult> UpdateOrderTotal(int orderId)
        {
            try
            {
                await _orderService.UpdateOrderTotalAsync(orderId);
                return Ok("Order total updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}



