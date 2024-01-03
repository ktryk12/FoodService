using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IShopData
    {
        Task<Shop> CreateShopAsync(Shop shop);
        Task<Shop> GetShopByIdAsync(int id);
       
        Task<bool> UpdateShopAsync(Shop shop);
        Task<bool> DeleteShopAsync(int id);
        Task<bool> AddSalesItemToShopAsync(int shopId, SalesItem item);
        Task<bool> RemoveSalesItemFromShopAsync(int shopId, int salesItemId);
        Task<IEnumerable<Shop>> GetAllShopsAsync();





    }
}




