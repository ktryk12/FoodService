using FoodService.BusinessLogic.ServiceInterface;
using FoodService.BusinessLogic;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.PriceService
{
    public class CompositeItemPricingStrategy : IPricingStrategy
    {
        private readonly IPricingStrategy _defaultStrategy = new DefaultPricingStrategy();

        public decimal CalculatePrice(SalesItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            decimal totalPrice = item.BasePrice;

            // Add cost for child items
            foreach (var composition in item.ChildCompositions)
            {
                if (composition?.ChildItem != null)
                {
                    totalPrice += CalculateAdditionalCostForChildItem(composition.ChildItem);
                }
            }

            // Optionally, handle ParentCompositions as well...

            decimal discount = CalculateCompositeDiscount(totalPrice);
            return totalPrice - discount;
        }

        private decimal CalculateAdditionalCostForChildItem(SalesItem childItem)
        {
            // Beregner ekstra omkostninger for childItem i forhold til standardprisen
            decimal childItemPriceWithIngredients = _defaultStrategy.CalculatePrice(childItem);
            decimal standardChildItemPrice = childItem.BasePrice;
            return childItemPriceWithIngredients - standardChildItemPrice; // Kun tillæg, ikke hele prisen
        }

        private decimal CalculateCompositeDiscount(decimal totalPrice)
        {
            // Definer rabatberegningen
            return totalPrice * 0.10m; // Eksempel: 10% rabat
        }
    }
}

