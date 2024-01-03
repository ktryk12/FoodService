using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemGroupController : ControllerBase
    {
        private readonly ISalesItemGroupService _salesItemGroupService;

        public SalesItemGroupController(ISalesItemGroupService salesItemGroupService)
        {
            _salesItemGroupService = salesItemGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesItemGroup([FromBody] SalesItemGroupDto salesItemGroupDto)
        {
            var createdSalesItemGroup = await _salesItemGroupService.CreateSalesItemGroupAsync(salesItemGroupDto);
            if (createdSalesItemGroup != null)
                return CreatedAtAction(nameof(GetSalesItemGroupById), new { id = createdSalesItemGroup.Id }, createdSalesItemGroup);
            return BadRequest("Failed to create sales item group.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesItemGroupById(int id)
        {
            var salesItemGroup = await _salesItemGroupService.GetSalesItemGroupByIdAsync(id);
            if (salesItemGroup != null)
                return Ok(salesItemGroup);
            return NotFound($"Sales item group with ID {id} not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesItemGroups()
        {
            var salesItemGroups = await _salesItemGroupService.GetAllSalesItemGroupsAsync();
            return Ok(salesItemGroups);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesItemGroup([FromBody] SalesItemGroupDto salesItemGroupDto)
        {
            var success = await _salesItemGroupService.UpdateSalesItemGroupAsync(salesItemGroupDto);
            if (success)
                return Ok("Sales item group updated successfully.");
            return BadRequest("Failed to update sales item group.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesItemGroup(int id)
        {
            var success = await _salesItemGroupService.DeleteSalesItemGroupAsync(id);
            if (success)
                return Ok("Sales item group deleted successfully.");
            return NotFound($"Sales item group with ID {id} not found.");
        }
    }
}

