using FoodService.Modellayer;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DAL.Interfaces;
using Microsoft.Extensions.Logging; 

namespace FoodService.BusinessLogic.PriceService
{
    public class PricingService : IPricingService
    {
        private readonly ISalesItemData _salesItemData;
        private readonly IIngredientSalesItemData _ingredientSalesItemData;
        private readonly ISalesItemCompositionData _salesItemCompositionData;
        private readonly IPricingStrategyFactory _pricingStrategyFactory;
        private readonly ILogger<PricingService> _logger; // Tilføj en logger

        public PricingService(ISalesItemData salesItemData,
                              IIngredientSalesItemData ingredientSalesItemData,
                              IPricingStrategyFactory pricingStrategyFactory,
                              ISalesItemCompositionData salesItemCompositionData,
                              ILogger<PricingService> logger) // Inkluder ILogger i constructor
        {
            _salesItemData = salesItemData;
            _ingredientSalesItemData = ingredientSalesItemData;
            _pricingStrategyFactory = pricingStrategyFactory;
            _salesItemCompositionData = salesItemCompositionData;
            _logger = logger; // Initialiser logger
        }

        public async Task<decimal> CalculatePriceAsync(int salesItemId)
        {
            var salesItem = await _salesItemData.GetByIdAsync(salesItemId);
            if (salesItem == null)
            {
                throw new ArgumentException($"Sales item with ID {salesItemId} not found.");
            }

            var pricingStrategy = _pricingStrategyFactory.GetPricingStrategy(salesItem);
            return pricingStrategy.CalculatePrice(salesItem);
        }

        public async Task<decimal> CalculateTotalOrderPriceAsync(IEnumerable<int> orderlineIds)
        {
            decimal totalPrice = 0;

            foreach (var id in orderlineIds)
            {
                var price = await CalculatePriceAsync(id);
                totalPrice += price;
            }

            return totalPrice;
        }
        public async Task<decimal> CalculateCustomPriceAsync(int salesItemId, Dictionary<int, int> ingredientQuantities)
        {
            _logger.LogInformation($"Starting price calculation for sales item {salesItemId}");

            var salesItem = await _salesItemData.GetByIdAsync(salesItemId);
            if (salesItem == null)
            {
                _logger.LogWarning($"Sales item with ID {salesItemId} not found.");
                throw new ArgumentException($"Sales item with ID {salesItemId} not found.");
            }

            decimal totalPrice = salesItem.BasePrice;

            // Beregn prisen for ingredienser
            foreach (var kv in ingredientQuantities)
            {
                var ingredient = await _ingredientSalesItemData.GetIngredientByIdAsync(kv.Key);
                if (ingredient == null)
                {
                    _logger.LogWarning($"Ingredient with ID {kv.Key} not found.");
                    throw new ArgumentException($"Ingredient with ID {kv.Key} not found.");
                }

                totalPrice += ingredient.IngredientPrice * kv.Value;
            }

            // Tilføj logik til at håndtere SalesItemComposition
            if (salesItem.IsComposite)
            {
                var compositions = await _salesItemCompositionData.GetByParentItemIdAsync(salesItemId);
                foreach (var composition in compositions)
                {
                    if (composition.ChildItem != null)
                    {
                        // Opret en ny dictionary for tilvalgte ingredienser til child item
                        Dictionary<int, int> childIngredientsQuantities = new Dictionary<int, int>();
                        // Her kan du indsætte logik for at udfylde childIngredientsQuantities baseret på brugerens valg eller en standardkonfiguration

                        var childItemExtraCost = await CalculateCustomPriceAsync(composition.ChildItemId, childIngredientsQuantities); // Beregn kun ekstraomkostninger
                        totalPrice += childItemExtraCost; // Tilføj til den samlede pris
                    }
                }
            }

            _logger.LogInformation($"Total calculated price: {totalPrice}");
            return totalPrice;
        }

    }
}

