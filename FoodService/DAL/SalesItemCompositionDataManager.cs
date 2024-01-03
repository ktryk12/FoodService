using FoodService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodService.Modellayer;
using FoodService.DTOs;

namespace FoodService.DAL
{
    public class SalesItemCompositionDataManager : ISalesItemCompositionData
    {
        private readonly IServiceContext _context;

        public SalesItemCompositionDataManager(IServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesItemComposition>> GetAllAsync()
        {
            return await _context.SalesItemCompositions
                                 .Include(sic => sic.ParentItem)
                                 .Include(sic => sic.ChildItem)
                                 .ToListAsync();
        }


        public async Task<SalesItemComposition> GetByIdAsync(int parentItemId, int childItemId)
        {
            return await _context.SalesItemCompositions
                .FindAsync(parentItemId, childItemId);
        }

        public async Task<SalesItemComposition> AddAsync(SalesItemComposition salesItemComposition)
        {
            _context.SalesItemCompositions.Add(salesItemComposition);
            await _context.SaveChangesAsync();
            return salesItemComposition;
        }

        public async Task<SalesItemComposition> UpdateAsync(SalesItemComposition salesItemComposition)
        {
            _context.Entry(salesItemComposition).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return salesItemComposition;
        }

        public async Task<bool> DeleteAsync(int parentItemId, int childItemId)
        {
            var salesItemComposition = await _context.SalesItemCompositions.FindAsync(parentItemId, childItemId);
            if (salesItemComposition == null)
                return false;

            _context.SalesItemCompositions.Remove(salesItemComposition);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<SalesItemComposition>> GetByParentItemIdAsync(int parentItemId)
        {
            return await _context.SalesItemCompositions
                                 .Where(sic => sic.ParentItemId == parentItemId)
                                 .Include(sic => sic.ParentItem) // Optional: Include parent item details
                                 .Include(sic => sic.ChildItem)  // Optional: Include child item details
                                 .ToListAsync();

        }
        
    }
}