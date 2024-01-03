using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.PriceService
{
    public class DefaultPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(SalesItem item)
        {
            decimal price = item.BasePrice;

            // Tjek for og tilføj prisen for ekstra ingredienser
            foreach (var ingredient in item.IngredientSalesItems)
            {
                price += ingredient.Ingredient.IngredientPrice;
            }

            return price;
        }
    }

}
