using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface ISalesItemGroupData
    {
        Task<int?> CreateSalesItemGroupAsync(SalesItemGroup salesItemGroup);
        Task<SalesItemGroup> GetSalesItemGroupByIdAsync(int id);
       
        Task<IEnumerable<SalesItemGroup>> GetAllSalesItemsGroupAsync();
        Task<bool> UpdateSalesItemGroupAsync(SalesItemGroup salesItemGroup);
        Task<bool> DeleteSalesItemGroupAsync(int id);
    }
}
