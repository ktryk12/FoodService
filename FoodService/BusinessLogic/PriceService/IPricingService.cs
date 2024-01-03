namespace FoodService.BusinessLogic.PriceService
{
    public interface IPricingService
    {
        Task<decimal> CalculatePriceAsync(int salesItemId);
        Task<decimal> CalculateTotalOrderPriceAsync(IEnumerable<int> orderlineIds);
        Task<decimal> CalculateCustomPriceAsync(int salesItemId, Dictionary<int, int> ingredientQuantities);
    }
}
