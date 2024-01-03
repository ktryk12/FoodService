using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
using FoodService.Modellayer;
using System.Collections.Generic;
using System.Threading.Tasks;

using FoodService.BusinessLogic.ServiceInterface;

public class DiscountService : IDiscountService
{
    private readonly IDiscountData _discountData;

    public DiscountService(IDiscountData discountData)
    {
        _discountData = discountData;
    }

    public async Task<DiscountDto> GetDiscountByIdAsync(int id)
    {
        var discount = await _discountData.GetDiscountByIdAsync(id);
        return DiscountConverter.ToDto(discount); // Brug DtoConverter
    }

    public async Task<List<DiscountDto>> GetAllDiscountsAsync()
    {
        var discounts = await _discountData.GetAllDiscountsAsync();
        var discountsList = discounts.ToList(); // Konverterer IEnumerable til List
        return DiscountConverter.ToDtoList(discountsList);
        
    }

    public async Task<int> CreateDiscountAsync(DiscountDto discountDto)
    {
        var discount = DiscountConverter.ToEntity(discountDto); // Konverter til entitet
        var createdDiscount = await _discountData.CreateDiscountAsync(discount);
        return createdDiscount.HasValue ? createdDiscount.Value : -1; // Håndter hvis null
    }

    public async Task<bool> UpdateDiscountByIdAsync(DiscountDto discountDtoToUpdate)
    {
        var discount = DiscountConverter.ToEntity(discountDtoToUpdate); // Konverter til entitet
        return await _discountData.UpdateDiscountByIdAsync(discount);
    }

    public async Task<bool> DeleteDiscountByIdAsync(int id)
    {
        return await _discountData.DeleteDiscountByIdAsync(id);
    }
}
