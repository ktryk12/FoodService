using FoodService.Modellayer;
using FoodService.DTOs;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface ISalesItemService
    {
        Task<SalesItemDto> CreateSalesItemAsync(CreateSalesItemDto createSalesItemDto);
        Task<SalesItem> GetSalesItemByIdAsync(int id);
        Task<IEnumerable<SalesItem>> GetAllSalesItemsAsync();
        Task<bool> UpdateSalesItemAsync(SalesItemDto salesItemDto);
        Task<bool> DeleteSalesItemAsync(int id);
       
        Task<IEnumerable<SalesItemDto>> GetSalesItemsByCategoryAsync(string category);
        Task<IEnumerable<SalesItemDto>> GetSalesItemsByIsCompositeAsync(bool isComposite);

    }
}
