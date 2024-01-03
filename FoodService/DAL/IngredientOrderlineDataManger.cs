using System.Collections.Generic;
using FoodService.Modellayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL
{
    public class IngredientOrderlineDataManager : IIngredientOrderlineData
    {
        private readonly IServiceContext _context;

        public IngredientOrderlineDataManager(IServiceContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<int?> CreateIngredientOrderlineAsync(IngredientOrderline ingredientOrderline)
        {
            try
            {
                await _context.IngredientOrderlines.AddAsync(ingredientOrderline);
                await _context.SaveChangesAsync();

                return ingredientOrderline.OrderlineId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        // READ
        public async Task<IngredientOrderline?> GetIngredientOrderlineByIdAsync(int orderlineId, int ingredientId)
        {
            return await _context.Set<IngredientOrderline>()
                .FirstOrDefaultAsync(io => io.OrderlineId == orderlineId && io.IngredientId == ingredientId);
        }

        // UPDATE
        public async Task<bool> UpdateIngredientOrderlineAsync(IngredientOrderline ingredientOrderline)
        {
            try
            {
                _context.Entry(ingredientOrderline).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // DELETE
        public async Task<bool> DeleteIngredientOrderlineAsync(int orderlineId, int ingredientId)
        {
            try
            {
                var ingredientOrderline = await _context.Set<IngredientOrderline>()
                    .FirstOrDefaultAsync(io => io.OrderlineId == orderlineId && io.IngredientId == ingredientId);

                if (ingredientOrderline == null)
                {
                    return false;
                }

                _context.Set<IngredientOrderline>().Remove(ingredientOrderline);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<IEnumerable<IngredientOrderline>> GetAllIngredientOrderlinesByOrderlineIdAsync(int orderlineId)
        {
            return await _context.IngredientOrderlines
                .Where(io => io.OrderlineId == orderlineId)
                .ToListAsync();
        }
        public async Task<List<IngredientOrderline>> GetOrderlinesByOrderlineIdAsync(int orderlineId)
        {
            return await _context.IngredientOrderlines
                                 .Where(io => io.OrderlineId == orderlineId)
                                 .ToListAsync();
        }
        public async Task AddIngredientsToOrderline(int orderlineId, List<int> ingredientIds)
        {
            var orderline = await _context.Orderlines.FindAsync(orderlineId);
            if (orderline == null) return;

            foreach (var ingredientId in ingredientIds)
            {
                var ingredientOrderline = new IngredientOrderline
                {
                    OrderlineId = orderlineId,
                    IngredientId = ingredientId
                };
                _context.IngredientOrderlines.Add(ingredientOrderline);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveIngredientsFromOrderline(int orderlineId, List<int> ingredientIds)
        {
            var ingredientOrderlines = await _context.IngredientOrderlines
                .Where(io => io.OrderlineId == orderlineId && ingredientIds.Contains(io.IngredientId))
                .ToListAsync();

            _context.IngredientOrderlines.RemoveRange(ingredientOrderlines);
            await _context.SaveChangesAsync();
        }

    }
}






