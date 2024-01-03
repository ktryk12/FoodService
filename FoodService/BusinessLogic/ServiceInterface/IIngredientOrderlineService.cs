using FoodService.DTOs;
using FoodService.Modellayer;
using static FoodService.BusinessLogic.IngredientOrderlineService;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IIngredientOrderlineService
    {
        Task<int?> CreateIngredientOrderlineAsync(IngredientOrderlineDto ingredientOrderlineDto);
        Task<IngredientOrderlineDto?> GetIngredientOrderlineByIdAsync(IngredientOrderlineDto idDto);
        Task<bool> UpdateIngredientOrderlineAsync(IngredientOrderlineDto ingredientOrderlineDto);
        Task<bool> DeleteIngredientOrderlineAsync(int orderlineId, int ingredientId);
        Task RemoveIngredientsFromOrderline(IngredientOrderlineDto ingredientOrderlinesDto);
        Task AddIngredientsToOrderline(IngredientOrderlineDto ingredientOrderlinesDto);





    }
}
