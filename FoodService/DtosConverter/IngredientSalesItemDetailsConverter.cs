using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.DtosConverter
{
    public class IngredientSalesItemDetailsConverter
    {
        public static IngredientSalesItemDetailsDto ConvertToIngredientSalesItemDetailsDto(IngredientSalesItem ingredientSalesItem, Ingredient ingredient)
        {
            return new IngredientSalesItemDetailsDto
            {
                SalesItemId = ingredientSalesItem.SalesItemId,
                IngredientId = ingredientSalesItem.IngredientId,
                Min = ingredientSalesItem.Min,
                Max = ingredientSalesItem.Max,
                Count = ingredientSalesItem.Count,
                Ingredient = new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    ImageUrl = ingredient.ImageUrl,
                    IngredientPrice = ingredient.IngredientPrice
                }
            };
        }
    }
}
