using System;
using System.Threading.Tasks;
using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
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
            
        }

        public async Task RemoveIngredientsFromOrderline(IngredientOrderlineDto ingredientOrderlinesDto)
        {
            await _ingredientOrderlineData.RemoveIngredientsFromOrderline(ingredientOrderlinesDto.OrderlineId, ingredientOrderlinesDto.IngredientIds);
            
        }

        

       
    }
}

