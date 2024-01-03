using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IIngredientSalesItemData
    {
        // Opretter en ny IngredientSalesItem
        Task<IngredientSalesItem> CreateAsync(IngredientSalesItem ingredientSalesItem);

        // Henter en specifik IngredientSalesItem
        Task<IngredientSalesItem> GetByIdAsync(int salesItemId, int ingredientId);
        // I IIngredientSalesItemData
        Task<Ingredient> GetIngredientByIdAsync(int ingredientId);


        // Opdaterer en eksisterende IngredientSalesItem
        Task<bool> UpdateAsync(IngredientSalesItem ingredientSalesItem);

        // Sletter en IngredientSalesItem
        Task<bool> DeleteAsync(int salesItemId, int ingredientId);

        // Tjekker om en bestemt kombination af SalesItem og Ingredient eksisterer
        Task<bool> ExistsAsync(int salesItemId, int ingredientId);

        // Henter alle IngredientSalesItems for et bestemt SalesItem
        Task<IEnumerable<IngredientSalesItem>> GetAllBySalesItemIdAsync(int salesItemId);

        // Henter alle IngredientSalesItems
        Task<IEnumerable<IngredientSalesItem>> GetAllAsync();
        Task<List<IngredientSalesItem>> GetIngredientSalesItemsBySalesItemIdAsync(int salesItemId);
    }
}
