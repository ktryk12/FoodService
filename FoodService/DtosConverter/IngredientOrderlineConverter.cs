using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class IngredientOrderlineConverter
    {
        public static IngredientOrderline ToEntity(IngredientOrderlineDto dto)
        {
            return new IngredientOrderline
            {
                IngredientId = dto.IngredientId,
                OrderlineId = dto.OrderlineId,
                Delta = dto.Delta,

            };
        }
        public static IngredientOrderlineDto ToDto(IngredientOrderline entity)
        {
            return new IngredientOrderlineDto
            {
                IngredientId = entity.IngredientId,
                OrderlineId = entity.OrderlineId,
                Delta = entity.Delta,
            };

        }
    }
}
