using Microsoft.AspNetCore.Mvc;
using FoodService.DTOs;
using FoodService.BusinessLogic;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Modellayer;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemCompositionController : ControllerBase
    {
        private readonly ISalesItemCompositionService _salesItemCompositionService;

        public SalesItemCompositionController(ISalesItemCompositionService salesItemCompositionService)
        {
            _salesItemCompositionService = salesItemCompositionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesItemComposition([FromBody] SalesItemCompositionDto salesItemCompositionDto)
        {
            var salesItemComposition = await _salesItemCompositionService.CreateSalesItemCompositionAsync(salesItemCompositionDto);
            if (salesItemComposition != null)
                return CreatedAtAction(nameof(GetSalesItemCompositionById), new { parentItemId = salesItemComposition.ParentItemId, childItemId = salesItemComposition.ChildItemId }, salesItemComposition);

            return BadRequest("Failed to create sales item composition.");
        }

        [HttpGet("{parentItemId}/{childItemId}")]
        public async Task<IActionResult> GetSalesItemCompositionById(int parentItemId, int childItemId)
        {
            var salesItemComposition = await _salesItemCompositionService.GetSalesItemCompositionByIdAsync(parentItemId, childItemId);
            if (salesItemComposition != null)
                return Ok(salesItemComposition);

            return NotFound("Sales item composition not found.");
        }

        // GET: api/salesitemcomposition
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var compositions = await _salesItemCompositionService.GetAllCompositionsAsync();
            if (compositions == null)
            {
                return NotFound();
            }

            return Ok(compositions);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesItemComposition([FromBody] SalesItemCompositionDto salesItemCompositionDto)
        {
            var success = await _salesItemCompositionService.UpdateSalesItemCompositionAsync(salesItemCompositionDto);
            if (success)
                return Ok("Sales item composition updated successfully.");

            return BadRequest("Failed to update sales item composition.");
        }

        [HttpDelete("{parentItemId}/{childItemId}")]
        public async Task<IActionResult> DeleteSalesItemComposition(int parentItemId, int childItemId)
        {
            var success = await _salesItemCompositionService.DeleteSalesItemCompositionAsync(parentItemId, childItemId);
            if (success)
                return Ok("Sales item composition deleted successfully.");

            return NotFound("Sales item composition not found.");
        }
        [HttpGet("parent/{parentItemId}")]
        public async Task<ActionResult<IEnumerable<SalesItemCompositionDto>>> GetByParentItemId(int parentItemId)
        {
            var salesItemComposition = await _salesItemCompositionService.GetCompositionsByParentItem(parentItemId);
            return Ok(salesItemComposition);
        }
        // GET: api/SalesItemComposition/Details/5
        [HttpGet("Details/{parentItemId}")]
        public async Task<IActionResult> GetCompositionWithDetails(int parentItemId)
        {
            try
            {
                var compositionWithDetails = await _salesItemCompositionService.GetCompositionWithDetailsAsync(parentItemId);
                if (compositionWithDetails == null)
                {
                    return NotFound();
                }
                return Ok(compositionWithDetails);
            }
            catch
            {
                // Log fejl her (om nødvendigt)
                return StatusCode(500, "En intern fejl opstod");
            }
        }


    }
}

