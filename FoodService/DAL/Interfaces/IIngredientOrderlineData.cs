using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IIngredientOrderlineData
    {
        Task<int?> CreateIngredientOrderlineAsync(IngredientOrderline ingredientOrderline);
        Task<IngredientOrderline?> GetIngredientOrderlineByIdAsync(int orderlineId, int ingredientId);
        Task<bool> UpdateIngredientOrderlineAsync(IngredientOrderline ingredientOrderline);
        Task<bool> DeleteIngredientOrderlineAsync(int orderlineId, int ingredientId);
        Task<IEnumerable<IngredientOrderline>> GetAllIngredientOrderlinesByOrderlineIdAsync(int orderlineId);
        Task<List<IngredientOrderline>> GetOrderlinesByOrderlineIdAsync(int orderlineId);
        Task AddIngredientsToOrderline(int orderlineId, List<int> ingredientIds);
        Task RemoveIngredientsFromOrderline(int orderlineId, List<int> ingredientIds);
    }




}
