using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FoodService.DAL
{
    public class SalesItemDataManager : ISalesItemData
    {
        private readonly IServiceContext _context;

        public SalesItemDataManager(IServiceContext context)
        {
            _context = context;
        }
        public async Task<SalesItem> CreateSalesItemAsync(SalesItem salesItem)
        {
            _context.SalesItems.Add(salesItem);
            await _context.SaveChangesAsync();
            return salesItem;
        }


        public async Task<IEnumerable<SalesItem>> GetAllAsync()
        {
            return await _context.SalesItems.ToListAsync();
        }

        public async Task<SalesItem> GetByIdAsync(int id)
        {
            return await _context.SalesItems.FindAsync(id);
        }
        public async Task<SalesItem> GetSalesItemByOrderlineIdAsync(int orderlineId)
        {
            // Antager at du har en DbContext kaldet _context
            return await _context.SalesItems
                                 .FirstOrDefaultAsync(s => s.Orderlines.Any(o => o.Id == orderlineId));
        }

        public async Task<SalesItem> AddAsync(SalesItem salesItem)
        {
            _context.SalesItems.Add(salesItem);
            await _context.SaveChangesAsync();
            return salesItem;
        }

        public async Task<SalesItem> UpdateAsync(SalesItem salesItem)
        {
            _context.Entry(salesItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return salesItem;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var salesItem = await _context.SalesItems.FindAsync(id);
            if (salesItem == null)
                return false;

            _context.SalesItems.Remove(salesItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<SalesItem>> GetSalesItemsByCategoryAsync(string category)
        {
            return await _context.SalesItems
                                 .Where(s => s.Category == category)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<SalesItem>> GetSalesItemsByIsComposite(bool isComposite)
        {
            return await _context.SalesItems
                                 .Where(item => item.IsComposite == isComposite)
                                 .ToListAsync();
        }

    }
}
