using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodService.DAL
{
    public class IngredientSalesItemDataManager : IIngredientSalesItemData
    {
        private readonly IServiceContext _context;

        public IngredientSalesItemDataManager(IServiceContext context)
        {
            _context = context;
        }

        // Opretter en ny IngredientSalesItem
        public async Task<IngredientSalesItem> CreateAsync(IngredientSalesItem ingredientSalesItem)
        {
            _context.IngredientSalesItems.Add(ingredientSalesItem);
            await _context.SaveChangesAsync();
            return ingredientSalesItem;
        }

        // Henter en specifik IngredientSalesItem
        public async Task<IngredientSalesItem> GetByIdAsync(int salesItemId, int ingredientId)
        {
            return await _context.IngredientSalesItems
                .FirstOrDefaultAsync(isi => isi.SalesItemId == salesItemId && isi.IngredientId == ingredientId);
        }

        // Opdaterer en eksisterende IngredientSalesItem
        public async Task<bool> UpdateAsync(IngredientSalesItem ingredientSalesItem)
        {
            var existingItem = await _context.IngredientSalesItems
                .FirstOrDefaultAsync(isi => isi.SalesItemId == ingredientSalesItem.SalesItemId && isi.IngredientId == ingredientSalesItem.IngredientId);

            if (existingItem == null)
            {
                return false;
            }

            existingItem.Min = ingredientSalesItem.Min;
            existingItem.Max = ingredientSalesItem.Max;
            existingItem.Count = ingredientSalesItem.Count;

            _context.IngredientSalesItems.Update(existingItem);
            await _context.SaveChangesAsync();
            return true;
        }

        // Sletter en IngredientSalesItem
        public async Task<bool> DeleteAsync(int salesItemId, int ingredientId)
        {
            var ingredientSalesItem = await _context.IngredientSalesItems
                .FirstOrDefaultAsync(isi => isi.SalesItemId == salesItemId && isi.IngredientId == ingredientId);

            if (ingredientSalesItem == null)
            {
                return false;
            }

            _context.IngredientSalesItems.Remove(ingredientSalesItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            return await _context.Ingredients.FindAsync(ingredientId);
        }

        // Henter alle IngredientSalesItems
        public async Task<IEnumerable<IngredientSalesItem>> GetAllAsync()
        {
            return await _context.IngredientSalesItems.ToListAsync();
        }

        // Henter alle IngredientSalesItems for et bestemt SalesItem
        public async Task<IEnumerable<IngredientSalesItem>> GetAllBySalesItemIdAsync(int salesItemId)
        {
            return await _context.IngredientSalesItems
                .Where(isi => isi.SalesItemId == salesItemId)
                .ToListAsync();
        }

        // Tjekker om en bestemt kombination af SalesItem og Ingredient eksisterer
        public async Task<bool> ExistsAsync(int salesItemId, int ingredientId)
        {
            return await _context.IngredientSalesItems
                .AnyAsync(isi => isi.SalesItemId == salesItemId && isi.IngredientId == ingredientId);
        }
        public async Task<List<IngredientSalesItem>> GetIngredientSalesItemsBySalesItemIdAsync(int salesItemId)
        {
         return await _context.IngredientSalesItems
             .Where(isi => isi.SalesItemId == salesItemId)
             .ToListAsync();
        }
    }
}





