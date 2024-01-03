using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class DiscountConverter
    {
        public static Discount ToEntity(DiscountDto dto)
        {
            return new Discount
            {
                Id = dto.Id,
                Rate = dto.Rate,
                SalesItemGroupId = dto.SalesItemGroupId,
                CustomerGroupId = dto.CustomerGroupId
            };
        }

        public static DiscountDto ToDto(Discount entity)
        {
            return new DiscountDto
            {
                Id = entity.Id,
                Rate = entity.Rate,
                SalesItemGroupId = entity.SalesItemGroupId,
                CustomerGroupId = entity.CustomerGroupId
            };
        }
        public static List<DiscountDto> ToDtoList(IEnumerable<Discount> entities)
        {
            return entities.Select(entity => DiscountConverter.ToDto(entity)).ToList();
        }

        public static List<Discount> ToEntityList(IEnumerable<DiscountDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }
    }

}

