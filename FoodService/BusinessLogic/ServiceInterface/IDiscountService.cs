using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IDiscountService
    {
        Task<DiscountDto> GetDiscountByIdAsync(int id);
        Task<List<DiscountDto>> GetAllDiscountsAsync();
        Task<int> CreateDiscountAsync(DiscountDto discountDto);
        Task<bool> UpdateDiscountByIdAsync(DiscountDto discountDtoToUpdate);
        Task<bool> DeleteDiscountByIdAsync(int id);
    }
}
