using FoodService.BusinessLogic.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FoodService.DTOs;

namespace FoodService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class CustomerGroupController : ControllerBase
    {
        private readonly ICustomerGroupService _customerGroupService;

        public CustomerGroupController(ICustomerGroupService customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerGroupById(int id)
        {
            var result = await _customerGroupService.GetCustomerGroupByIdAsync(id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerGroups()
        {
            var results = await _customerGroupService.GetAllCustomerGroupsAsync();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerGroup([FromBody] CustomerGroupDto customerGroupDto)
        {
            var result = await _customerGroupService.CreateCustomerGroupAsync(customerGroupDto);
            return CreatedAtAction(nameof(GetCustomerGroupById), new { id = result }, customerGroupDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerGroupById([FromBody] CustomerGroupDto customerGroupDtoToUpdate)
        {
            var success = await _customerGroupService.UpdateCustomerGroupByIdAsync(customerGroupDtoToUpdate);
            if (success)
                return NoContent();
            return NotFound();
        }
    }
}
