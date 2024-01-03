using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class IngredientSalesItemConverter
    {
        public static IngredientSalesItem ToEntity(IngredientSalesItemDto dto)
        {
            return new IngredientSalesItem
            {
                SalesItemId = dto.SalesItemId,
                IngredientId = dto.IngredientId,
                Count = dto.Count,
                Max = dto.Max,
                Min = dto.Min,
            };
        }
        public static IngredientSalesItemDto ToDto(IngredientSalesItem entity)
        {
            return new IngredientSalesItemDto
            {
                SalesItemId = entity.SalesItemId,
                IngredientId = entity.IngredientId,
                Count = entity.Count,
                Max = entity.Max,
                Min = entity.Min,

            };
        }
        public static List<IngredientSalesItemDto> ToDtoList(IEnumerable<IngredientSalesItem> entities)
        {
            return entities.Select(entity => IngredientSalesItemConverter.ToDto(entity)).ToList();
        }

        public static List<IngredientSalesItem> ToEntityList(IEnumerable<IngredientSalesItemDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }
    }
}
    

