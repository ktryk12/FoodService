using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IOrderData
    {

        Task<Order> GetOrderByIdAsync(int id);
        Task<(int? Id, int OrderNumber)> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order orderToUpdate);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();


    }

}
