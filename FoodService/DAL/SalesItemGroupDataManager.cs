using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using System.Threading.Tasks;



namespace FoodService.DAL
{
    public class SalesItemGroupDataManager : ISalesItemGroupData
    {
        private readonly IServiceContext _context;
        private readonly ILogger<SalesItemGroupDataManager> _logger;

        public SalesItemGroupDataManager(IServiceContext context, ILogger<SalesItemGroupDataManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create a new SalesItemGroup
        public async Task<int?> CreateSalesItemGroupAsync(SalesItemGroup salesItemGroup)
        {
            try
            {
                _context.Add(salesItemGroup);
                await _context.SaveChangesAsync();
                return salesItemGroup.Id; // Returner ID'et for det oprettede produkt
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating salesItem: {ex}");
                return null; // Returner null i tilfælde af fejl
            }
        }


        // Get a SalesItemGroup by ID
        public async Task<SalesItemGroup> GetSalesItemGroupByIdAsync(int id)
        {
            return await _context.Set<SalesItemGroup>().FirstOrDefaultAsync(p => p.Id == id);
        }

        // Get all SalesItemGroup
        public async Task<IEnumerable<SalesItemGroup>> GetAllSalesItemsGroupAsync()
        {
            return await _context.Set<SalesItemGroup>().ToListAsync();
        }

        // Update a SalesItemGroup
        public async Task<bool> UpdateSalesItemGroupAsync(SalesItemGroup salesItemGroup)
        {
            try
            {
                _context.Entry(salesItemGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating salesItem: {ex}");
                return false;
            }
        }

        // Delete a SalesItemGroup by ID
        public async Task<bool> DeleteSalesItemGroupAsync(int id)
        {
            try
            {
                var salesItemGroup = await _context.Set<SalesItemGroup>().FindAsync(id);
                if (salesItemGroup != null)
                {
                    _context.Set<SalesItemGroup>().Remove(salesItemGroup);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting salesItemGroup: {ex}");
                return false;
            }
        }
    }
}

