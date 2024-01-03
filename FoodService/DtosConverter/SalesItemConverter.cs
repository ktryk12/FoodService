using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class SalesItemConverter
    {
        public static SalesItem ToEntity(SalesItemDto dto)
        {
            var salesItem = new SalesItem
            {
                Name = dto.Name,
                ProductNumber = dto.ProductNumber,
                Category = dto.Category,
                ImageUrl = dto.ImageUrl,
                IsActive = dto.IsActive,
                IsComposite = dto.IsComposite,
                // Andre feltkonverteringer efter behov
            };

            if (dto.Id.HasValue)
            {
                salesItem.Id = dto.Id.Value; // Brug Value, da Id er nullable
            }

            return salesItem;
        }

        public static SalesItemDto ToDto(SalesItem entity)
        {
            return new SalesItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ProductNumber = entity.ProductNumber,
                Category = entity.Category,
                ImageUrl = entity.ImageUrl,
                // Udelad ImageFile, da det ikke er en del af entiteten
                IsActive = entity.IsActive,
                IsComposite = entity.IsComposite,
                // Andre feltkonverteringer efter behov
            };
        }
        public static List<SalesItemDto> ToDtoList(IEnumerable<SalesItem> entities)
        {
            return entities.Select(entity => SalesItemConverter.ToDto(entity)).ToList();
        }

        public static List<SalesItem> ToEntityList(IEnumerable<SalesItemDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }

    }
}
