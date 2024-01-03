using FoodService.Modellayer;
using FoodService.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace FoodService.Dto_sConverter
{
    public class SalesItemCompositionConverter
    {
        public static SalesItemComposition ToEntity(SalesItemCompositionDto dto)
        {
            return new SalesItemComposition
            {
                ParentItemId = dto.ParentItemId,
                ChildItemId = dto.ChildItemId,
                // Bemærk: ParentItem og ChildItem entiteter bør hentes fra databasen
                // baseret på deres ID'er, hvis det er nødvendigt
            };
        }

        public static SalesItemCompositionDto ToDto(SalesItemComposition entity)
        {
            return new SalesItemCompositionDto
            {
                ParentItemId = entity.ParentItemId,
                ChildItemId = entity.ChildItemId,
                
            };
        }

        public static List<SalesItemCompositionDto> ToDtoList(IEnumerable<SalesItemComposition> entities)
        {
            return entities.Select(ToDto).ToList();
        }

        public static List<SalesItemComposition> ToEntityList(IEnumerable<SalesItemCompositionDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }
    }
}
