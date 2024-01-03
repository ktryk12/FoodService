using FoodService.Modellayer;


namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(SalesItem item);
        
    }
}
