using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class OrderlineConverter
    {
        public static Orderline ToEntity(OrderlineDto dto)
        {
            return new Orderline
            {
                Id = dto.Id,
                Quantity = dto.Quantity,
                SalesItemId = dto.SalesItemId,
                OrderId = dto.OrderId,
                OrderlinePrice = dto.OrderlinePrice,
            };

        }
        public static OrderlineDto toDto(Orderline entity)
        {
            return new OrderlineDto
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                OrderlinePrice = entity.OrderlinePrice,
                OrderId = entity.OrderId,
                SalesItemId = entity.SalesItemId,

            };
        }

        
    }
}
