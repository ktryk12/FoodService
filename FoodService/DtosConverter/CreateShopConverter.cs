using FoodService.DTOs;
using FoodService.DTOsConverter;
using FoodService.Modellayer;

namespace FoodService.DtosConverter
{
    public class CreateShopConverter
    {
        public static Shop ToEntity(CreateShopDto dto)
        {
            return new Shop
            {
                
                Name = dto.Name,
                Location = dto.Location,
                Type = ShopTypeConverter.ConvertToEntity(dto.Type)

                // Andre konverteringer
            };
        }

        public static CreateShopDto ToDto(Shop entity)
        {
            // Tjek for null-værdier
            if (entity == null) return null;

            return new CreateShopDto
            {
                Name = entity.Name,
                Location = entity.Location,
                Type = entity.Type != null ? ShopTypeConverter.ConvertToDto(entity.Type) : default(ShopTypeDto)
            };
        }
    }
}
