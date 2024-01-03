using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class OrderConverter
    {
        public static Order ToEntity(OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                OrderNumber = dto.OrderNumber,
                Datetime = dto.Datetime,
                Total = dto.Total,
                ShopId = dto.ShopId,
            };
        }
        public static OrderDto ToDto(Order entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                OrderNumber = entity.OrderNumber,
                Datetime = entity.Datetime,
                Total = entity.Total,
                ShopId = entity.ShopId,
            };
        }
        public static List<OrderDto> ToDtoList(IEnumerable<Order> entities)
        {
            return entities.Select(entity => OrderConverter.ToDto(entity)).ToList();
        }

        public static List<Order> ToEntityList(IEnumerable<OrderDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();
        }
    }
}
