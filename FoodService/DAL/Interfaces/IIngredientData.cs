using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IIngredientData
    {

        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<List<Ingredient>> GetIngredientsByIdAsync(List<int> ingredientIds);
        Task<Ingredient?> GetIngredientByIdAsync(int id);
        Task<int?> GetIngredientIdByNameAsync(string name);

        Task<Ingredient> CreateIngredientAsync(Ingredient ingredient);
        Task<bool> UpdateIngredientAsync(Ingredient ingredientToUpdate);
        Task<bool> DeleteIngredientAsync(int id);
    }

}
