using FoodService.Modellayer;
using FoodService.DTOs;
using static FoodService.BusinessLogic.IngredientService;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IIngredientService
    {
        Task<IngredientDto> CreateIngredientAsync(CreateIngredientDto createIngredientDto);
        Task<bool> UpdateIngredientAsync(IngredientDto ingredientDto);
        Task<bool> DeleteIngredientAsync(int id);
        Task<IEnumerable<IngredientSalesItemDetailsDto>> GetIngredientsWithDetailsBySalesItemIdAsync(int salesItemId);
        Task<IngredientDto?> GetIngredientByIdAsync(int id);
        Task<List<IngredientDto>> GetAllIngredientsAsync();

    }
}
