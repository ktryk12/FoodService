using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.PriceService
{
    public class PricingStrategyFactory : IPricingStrategyFactory
    {
        public IPricingStrategy GetPricingStrategy(SalesItem item)
        {
            if (item.IsComposite)
            {
                return new CompositeItemPricingStrategy();
            }
            else
            {
                return new DefaultPricingStrategy();
            }
        }
    }
}