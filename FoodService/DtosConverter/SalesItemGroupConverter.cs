using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class SalesItemGroupConverter
    {
        public static SalesItemGroupDto ToDto(SalesItemGroup salesItemGroup)
        {
            return new SalesItemGroupDto
            {
                Id = salesItemGroup.Id,
                Name = salesItemGroup.Name,
                // Andre egenskaber...
            };
        }

        public static SalesItemGroup ToEntity(SalesItemGroupDto salesItemGroupDto)
        {
            return new SalesItemGroup
            {
                Id = salesItemGroupDto.Id,
                Name = salesItemGroupDto.Name,
                // Andre egenskaber...
            };
        }
    }
}
