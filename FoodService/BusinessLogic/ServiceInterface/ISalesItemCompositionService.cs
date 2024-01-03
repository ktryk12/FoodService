using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface ISalesItemCompositionService
    {
        Task<SalesItemComposition> CreateSalesItemCompositionAsync(SalesItemCompositionDto salesItemCompositionDto);
        Task<IEnumerable<SalesItemCompositionDto>> GetAllCompositionsAsync();
        Task<bool> UpdateSalesItemCompositionAsync(SalesItemCompositionDto salesItemCompositionDto);
        Task<bool> DeleteSalesItemCompositionAsync(int parentItemId, int childItemId);
        Task<IEnumerable<SalesItemCompositionDto>> GetCompositionsByParentItem(int parentItemId);
        Task<SalesItemComposition> GetSalesItemCompositionByIdAsync(int parentItemId, int childItemId);

        Task<SalesItemCompositionWithDetailsDto> GetCompositionWithDetailsAsync(int parentItemId);
    }
}
