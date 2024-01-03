using FoodService.DTOs;
using System.Threading.Tasks;

using FoodService.Modellayer;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IOrderlineService
    {
        Task<OrderlineDto> CreateOrderlineAsync(OrderlineDto orderlineDto);

        Task<bool> UpdateOrderlineAsync(OrderlineDto orderlineDto);

        Task<OrderlineDto> GetOrderlineByIdAsync(int id);
        Task<List<OrderlineDto>> GetAllOrderlinesAsync();
        Task<bool> DeleteOrderlineAsync(int id);
        


    }
}
