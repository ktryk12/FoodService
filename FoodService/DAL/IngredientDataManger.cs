using System.Collections.Generic;
using FoodService.Modellayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FoodService.DAL
{
    public class IngredientDataManager : IIngredientData
    {
        private readonly IServiceContext _context;


        public IngredientDataManager(IServiceContext context)
        {
            _context = context;

        }

        

        public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
        {

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;


        }

        public async Task<bool> UpdateIngredientAsync(Ingredient ingredientToUpdate)
        {
            try
            {
                var existingIngredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientToUpdate.Id);
                if (existingIngredient == null) return false;

                _context.Entry(existingIngredient).CurrentValues.SetValues(ingredientToUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<Ingredient?> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredients
                                 .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int?> GetIngredientIdByNameAsync(string name)
        {
            var ingredient = await _context.Ingredients
                                           .FirstOrDefaultAsync(i => i.Name == name);

            return ingredient?.Id;  // Returns the Id if ingredient is found, otherwise null.
        }
        public async Task<List<Ingredient>> GetIngredientsByIdAsync(List<int> ingredientIds)
        {
            return await _context.Ingredients.Where(i => ingredientIds.Contains(i.Id)).ToListAsync();
        }


        public async Task<bool> DeleteIngredientAsync(int id)
        {
            try
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
                if (ingredient == null) return false;

                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }


    }
}

