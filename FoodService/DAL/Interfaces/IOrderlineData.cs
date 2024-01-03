using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IOrderlineData
    {
        Task<List<Orderline>> GetOrderlinesByOrderIdAsync(int orderId);
        
        Task<Orderline> GetOrderlineWithSalesItemAsync(int orderlineId);
        Task<List<SalesItemComposition>> GetSalesItemCompositionsByOrderlineAsync(int orderlineId);
        Task<Orderline> CreateOrderlineAsync(Orderline orderline);
        Task<bool> UpdateOrderlineAsync(Orderline orderline);
        Task<bool> DeleteOrderlineAsync(int orderlineId);
        Task<Orderline> GetOrderlineByIdAsync(int id);
        Task<List<Orderline>> GetAllOrderlinesAsync();

    }
}
