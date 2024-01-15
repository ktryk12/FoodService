using FoodService.Modellayer;
using FoodService.DTOs;
using static FoodService.BusinessLogic.ShopService;
using FoodService.DTOs;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IShopService
    {
        Task<ShopDto> CreateShopAsync(CreateShopDto createShopDto);
        Task<ShopDto> GetShopByIdAsync(int id);
        Task<IEnumerable<ShopDto>> GetAllShopsAsync();
        Task<bool> UpdateShopAsync(ShopDto shopDto);
        Task<bool> DeleteShopAsync(int id);
        Task<bool> AddSalesItemToShopAsync(int shopId, SalesItemDto itemDto);
        Task<bool> RemoveSalesItemFromShopAsync(int shopId, int salesItemId);
        Task<List<int>> GetSalesItemIdsByShopIdAsync(int shopId);
        


    }
}
