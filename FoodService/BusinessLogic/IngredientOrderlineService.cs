using System;
using System.Threading.Tasks;
using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
using FoodService.BusinessLogic.PriceService;
using FoodService.Modellayer;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.BusinessLogic
{
    public class IngredientOrderlineService : IIngredientOrderlineService
    {
        private readonly IIngredientOrderlineData _ingredientOrderlineData;
        private readonly IIngredientData _ingredientData;
        private readonly ISalesItemData _salesItemData;

        public IngredientOrderlineService(IIngredientOrderlineData ingredientOrderlineData,
                                          IIngredientData ingredientData,
                                          ISalesItemData salesItemData)
        {
            _ingredientOrderlineData = ingredientOrderlineData;
            _ingredientData = ingredientData;
            _salesItemData = salesItemData;
        }

        public async Task<int?> CreateIngredientOrderlineAsync(IngredientOrderlineDto ingredientOrderlineDto)
        {
            var ingredientOrderline = IngredientOrderlineConverter.ToEntity(ingredientOrderlineDto);
            return await _ingredientOrderlineData.CreateIngredientOrderlineAsync(ingredientOrderline);
        }

        public async Task<IngredientOrderlineDto?> GetIngredientOrderlineByIdAsync(IngredientOrderlineDto idDto)
        {
            var ingredientOrderline = await _ingredientOrderlineData.GetIngredientOrderlineByIdAsync(idDto.OrderlineId, idDto.IngredientId);
            return IngredientOrderlineConverter.ToDto(ingredientOrderline);
        }

        public async Task<bool> UpdateIngredientOrderlineAsync(IngredientOrderlineDto ingredientOrderlineDto)
        {
            var ingredientOrderline = IngredientOrderlineConverter.ToEntity(ingredientOrderlineDto);
            return await _ingredientOrderlineData.UpdateIngredientOrderlineAsync(ingredientOrderline);
        }

        public async Task<bool> DeleteIngredientOrderlineAsync(int orderlineId, int ingredientId)
        {
            return await _ingredientOrderlineData.DeleteIngredientOrderlineAsync(orderlineId, ingredientId);
        }
        public async Task AddIngredientsToOrderline(IngredientOrderlineDto ingredientOrderlinesDto)
        {
            await _ingredientOrderlineData.AddIngredientsToOrderline(ingredientOrderlinesDto.OrderlineId, ingredientOrderlinesDto.IngredientIds);
            await RecalculateAndUpdateSalesItemPrice(ingredientOrderlinesDto.OrderlineId);
        }

        public async Task RemoveIngredientsFromOrderline(IngredientOrderlineDto ingredientOrderlinesDto)
        {
            await _ingredientOrderlineData.RemoveIngredientsFromOrderline(ingredientOrderlinesDto.OrderlineId, ingredientOrderlinesDto.IngredientIds);
            await RecalculateAndUpdateSalesItemPrice(ingredientOrderlinesDto.OrderlineId);
        }

        private async Task RecalculateAndUpdateSalesItemPrice(int orderlineId)
        {
            // Step 1: Fetch the SalesItem associated with the orderlineId
            var salesItem = await _salesItemData.GetSalesItemByOrderlineIdAsync(orderlineId);
            if (salesItem == null)
            {
                throw new InvalidOperationException($"SalesItem not found for orderline ID {orderlineId}");
            }

            // Step 2: Determine the correct pricing strategy
            IPricingStrategy pricingStrategy = salesItem.IsComposite
                                               ? new CompositeItemPricingStrategy()
                                               : new DefaultPricingStrategy();

            // Step 3: Recalculate the price using the chosen strategy
            decimal newPrice = pricingStrategy.CalculatePrice(salesItem);

            // Step 4: Update the SalesItem with the new price
            salesItem.BasePrice = newPrice; // Assuming BasePrice is the field to update
            await _salesItemData.UpdateAsync(salesItem);
        }


       
    }
}

