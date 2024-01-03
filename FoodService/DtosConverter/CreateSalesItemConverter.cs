using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.DtosConverter
{
    public class CreateSalesItemConverter
    {
        public static SalesItem ToEntity(CreateSalesItemDto dto)
        {
            var salesItem = new SalesItem
            {
                Name = dto.Name,
                ProductNumber = dto.ProductNumber,
                BasePrice = dto.BasePrice,
                Category = dto.Category,
                SalesItemGroupId = dto.SalesItemGroupId,
                IsActive = dto.IsActive,
                IsComposite = dto.IsComposite,
                
            };

            
            return salesItem;
        }

        public static CreateSalesItemDto ToDto(SalesItem entity)
        {
            return new CreateSalesItemDto
            {
                Name = entity.Name,
                ProductNumber = entity.ProductNumber,
                Category = entity.Category,
                // Udelad ImageFile, da det ikke er en del af entiteten
                SalesItemGroupId = entity.SalesItemGroupId,
                IsActive = entity.IsActive,
                IsComposite = entity.IsComposite,
            };
        }
    }
}
