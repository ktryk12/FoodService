using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface ISalesItemData
    {
        Task<SalesItem> CreateSalesItemAsync(SalesItem salesItem);
        Task<IEnumerable<SalesItem>> GetAllAsync();
        Task<SalesItem> GetByIdAsync(int id);
        Task<SalesItem> AddAsync(SalesItem salesItem);
        Task<SalesItem> UpdateAsync(SalesItem salesItem);
        Task<bool> DeleteAsync(int id);
        Task<SalesItem> GetSalesItemByOrderlineIdAsync(int orderlineId);
        Task<IEnumerable<SalesItem>> GetSalesItemsByCategoryAsync(string category);
        Task<IEnumerable<SalesItem>> GetSalesItemsByIsComposite(bool isComposite);
    }
}
