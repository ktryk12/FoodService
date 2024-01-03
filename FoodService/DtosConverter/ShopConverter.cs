using FoodService.DTOs;
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
                Type = dto.Type
                // Andre konverteringer
            };
        }

        public static ShopDto ToDto(Shop entity)
        {
            return new ShopDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                Type = entity.Type
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
