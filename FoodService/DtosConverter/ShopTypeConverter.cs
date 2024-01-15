using FoodService.Modellayer; 
using FoodService.DTOs;

namespace FoodService.DTOsConverter
{
    public class ShopTypeConverter
    {
        // Konverter fra ShopTypeDto til ShopType (brugt i entiteten)
        public static ShopType ConvertToEntity(ShopTypeDto dtoType)
        {
            return dtoType switch
            {
                ShopTypeDto.Candystor => ShopType.Candystor,
                ShopTypeDto.Foodstand => ShopType.Foodstand,
                ShopTypeDto.Restaurant => ShopType.Restaurant,
                _ => throw new ArgumentOutOfRangeException(nameof(dtoType), $"Not expected directive value: {dtoType}")
            };
        }

        // Konverter fra ShopType (brugt i entiteten) til ShopTypeDto
        public static ShopTypeDto ConvertToDto(ShopType entityType)
        {
            return entityType switch
            {
                ShopType.Candystor => ShopTypeDto.Candystor,
                ShopType.Foodstand => ShopTypeDto.Foodstand,
                ShopType.Restaurant => ShopTypeDto.Restaurant,
                _ => throw new ArgumentOutOfRangeException(nameof(entityType), $"Not expected directive value: {entityType}")
            };
        }
    }
}
