using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.DtosConverter
{
    public class CreateIngredientConverter
    {

        public static Ingredient ToEntity(CreateIngredientDto dto)
        {
            var ingredient = new Ingredient
            {
                Name = dto.Name,
                IngredientPrice = dto.IngredientPrice,
                // ImageUrl er ikke inkluderet her, da det håndteres i servicen
            };
            return ingredient;
        }

        public static CreateIngredientDto ToDto(Ingredient entity)
        {
            return new CreateIngredientDto
            {
                Name = entity.Name,
                IngredientPrice = entity.IngredientPrice,
                // ImageFile kan ikke inkluderes her, da det ikke er del af entiteten
            };
        }
    }
}
