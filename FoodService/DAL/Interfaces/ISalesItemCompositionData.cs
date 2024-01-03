using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface ISalesItemCompositionData
    {
        Task<IEnumerable<SalesItemComposition>> GetAllAsync();
        Task<SalesItemComposition> AddAsync(SalesItemComposition salesItemComposition);
        Task<SalesItemComposition> UpdateAsync(SalesItemComposition salesItemComposition);
        Task<bool> DeleteAsync(int parentItemId, int childItemId);
        Task<IEnumerable<SalesItemComposition>> GetByParentItemIdAsync(int parentItemId);
        Task<SalesItemComposition> GetByIdAsync(int parentItemId, int childItemId);


       }
}
