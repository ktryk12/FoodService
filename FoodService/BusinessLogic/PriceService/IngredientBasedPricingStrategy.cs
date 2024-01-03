using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Modellayer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FoodService.BusinessLogic.PriceService
{
    public class IngredientBasedPricingStrategy : IPricingStrategy
    {
        private readonly IIngredientService _ingredientService;

        // Tilføj en constructor for at injecte dependency
        public IngredientBasedPricingStrategy(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }


        public decimal CalculatePrice(SalesItem item)
        {
            decimal price = item.BasePrice;
            foreach (var ingredient in item.IngredientSalesItems)
            {
                price += ingredient.Ingredient.IngredientPrice; // Add price for each added ingredient
            }
            return price;
        }
        public async Task<decimal> CalculateCustomPriceAsync(SalesItem item, Dictionary<int, int> ingredientQuantities)
        {
            decimal price = item.BasePrice;

            // Iterer gennem alle ingrediensmængder og hent hver ingrediens asynkront
            foreach (var ingredientQuantity in ingredientQuantities)
            {
                var ingredientId = ingredientQuantity.Key;
                var quantity = ingredientQuantity.Value;

                // Antager at GetIngredientByIdAsync er en metode i IIngredientService
                var ingredientDto = await _ingredientService.GetIngredientByIdAsync(ingredientId);
                if (ingredientDto != null)
                {
                    price += ingredientDto.IngredientPrice * quantity;
                }
            }

            return price;
        }
    }
}
