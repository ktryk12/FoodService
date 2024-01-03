using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class IngredientConverter
    {
        public static Ingredient ToEntity(IngredientDto dto)
        {
            return new Ingredient
            {
                Id = dto.Id,
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                IngredientPrice = dto.IngredientPrice,
            };
        }
        public static IngredientDto ToDto(Ingredient entity)
        {
            return new IngredientDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                IngredientPrice = entity.IngredientPrice,
            };
        }
        public static List<IngredientDto> ToDtoList(IEnumerable<Ingredient> entities)
        {
            return entities.Select(entity => IngredientConverter.ToDto(entity)).ToList();
        }

        public static List<Ingredient> ToEntityList(IEnumerable<IngredientDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }
    }
}
