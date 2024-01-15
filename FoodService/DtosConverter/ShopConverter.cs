using FoodService.DTOs;
using FoodService.DTOsConverter;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class ShopConverter
    {
        public static Shop ToEntity(ShopDto dto)
        {
            return new Shop
            {
                Id = dto.Id,
                Name = dto.Name,
                Location = dto.Location,
                Type = ShopTypeConverter.ConvertToEntity(dto.Type),
                ImageUrl = dto.ImageUrl
                // Andre konverteringer
            };
        }

        public static ShopDto ToDto(Shop entity)
        {
            if (entity == null)
            {
                // Handle null case, perhaps return null or throw a more informative exception
                return null; // or throw new ArgumentNullException(nameof(entity));
            }
            return new ShopDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                Type = ShopTypeConverter.ConvertToDto(entity.Type),
                ImageUrl = entity.ImageUrl
                // Andre konverteringer
            };
        }
        public static List<ShopDto> ToDtoList(IEnumerable<Shop> entities)
        {
            return entities.Select(entity => ShopConverter.ToDto(entity)).ToList();
        }

        public static List<Shop> ToEntityList(IEnumerable<ShopDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }
    }
}
