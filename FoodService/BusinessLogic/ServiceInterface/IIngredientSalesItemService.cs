using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FoodService.DTOs;
using FoodService.Modellayer;
using static FoodService.BusinessLogic.IngredientSalesItemService;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IIngredientSalesItemService
    {
        Task<bool> AddIngredientToSalesItemAsync(IngredientSalesItemDto ingredientSalesItemDto);
        Task<bool> UpdateIngredientSalesItemAsync(IngredientSalesItemDto ingredientSalesItemDto);
        Task<bool> RemoveIngredientFromSalesItemAsync(int salesItemId, int ingredientId);
        Task<bool> AddOrUpdateIngredientToSalesItemAsync(IngredientSalesItemDto ingredientSalesItemDto);
        Task<IngredientSalesItemDto?> GetIngredientSalesItemByIdAsync(int salesItemId, int ingredientId);
        Task<IEnumerable<IngredientSalesItemDto>> GetAllBySalesItemIdAsync(int salesItemId);
        Task<IEnumerable<IngredientSalesItemDto>> GetAllAsync();

    }
}
