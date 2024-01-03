using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IDiscountData
    {
        Task<Discount?> GetDiscountByIdAsync(int id);
        Task<List<Discount>> GetAllDiscountsAsync();
        Task<int?> CreateDiscountAsync(Discount discount);
        Task<bool> UpdateDiscountByIdAsync(Discount discountToUpdate);
        Task<bool> DeleteDiscountByIdAsync(int id);
    }

}
