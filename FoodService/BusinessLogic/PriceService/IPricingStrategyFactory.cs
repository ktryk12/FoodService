using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.PriceService
{
    public interface IPricingStrategyFactory
    {
        
            IPricingStrategy GetPricingStrategy(SalesItem item);
        
    }
}
