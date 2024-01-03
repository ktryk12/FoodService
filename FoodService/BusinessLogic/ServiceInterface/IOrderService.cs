using FoodService.DTOs;
using System.Threading.Tasks;
using FoodService.DTOs;
using static FoodService.BusinessLogic.OrderService;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IOrderService
    {



        Task<(int? Id, int OrderNumber)> CreateOrderAsync(int shopId);
        Task<bool> UpdateOrderAsync(OrderDto orderDtoToUpdate);
        Task<bool> DeleteOrderAsync(int id);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task UpdateOrderTotalAsync(int orderDtoId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
     
    }
}
