using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Modellayer;
using FoodService.DtosConverter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.Dto_sConverter;
using Microsoft.EntityFrameworkCore;

namespace FoodService.BusinessLogic
{
    public class IngredientSalesItemService : IIngredientSalesItemService
    {
        private readonly IIngredientSalesItemData _ingredientSalesItemData;
        private readonly ISalesItemCompositionData _salesItemCompositionData;


        public IngredientSalesItemService(IIngredientSalesItemData ingredientSalesItemData, ISalesItemCompositionData salesItemCompositionData)
        {
            _ingredientSalesItemData = ingredientSalesItemData ?? throw new ArgumentNullException(nameof(ingredientSalesItemData));
            _salesItemCompositionData = salesItemCompositionData;
        }

        public async Task<bool> AddIngredientToSalesItemAsync(IngredientSalesItemDto ingredientSalesItemDto)
        {
            var ingredientSalesItem = IngredientSalesItemConverter.ToEntity(ingredientSalesItemDto);
            var createdItem = await _ingredientSalesItemData.CreateAsync(ingredientSalesItem);
            return createdItem != null;
        }

        public async Task<bool> UpdateIngredientSalesItemAsync(IngredientSalesItemDto ingredientSalesItemDto)
        {
            var ingredientSalesItem = IngredientSalesItemConverter.ToEntity(ingredientSalesItemDto);
            return await _ingredientSalesItemData.UpdateAsync(ingredientSalesItem);
        }

        public async Task<bool> RemoveIngredientFromSalesItemAsync(int salesItemId, int ingredientId)
        {
            return await _ingredientSalesItemData.DeleteAsync(salesItemId, ingredientId);
        }

        public async Task<bool> AddOrUpdateIngredientToSalesItemAsync(IngredientSalesItemDto ingredientSalesItemDto)
        {
            var existingRelation = await _ingredientSalesItemData.GetByIdAsync(ingredientSalesItemDto.SalesItemId, ingredientSalesItemDto.IngredientId);
            if (existingRelation == null)
            {
                var newIngredientSalesItem = IngredientSalesItemConverter.ToEntity(ingredientSalesItemDto);
                var createdItem = await _ingredientSalesItemData.CreateAsync(newIngredientSalesItem);
                return createdItem != null;
            }
            else
            {
                existingRelation.Min = ingredientSalesItemDto.Min;
                existingRelation.Max = ingredientSalesItemDto.Max;
                existingRelation.Count = ingredientSalesItemDto.Count;
                return await _ingredientSalesItemData.UpdateAsync(existingRelation);
            }
        }

        public async Task<IngredientSalesItemDto?> GetIngredientSalesItemByIdAsync(int salesItemId, int ingredientId)
        {
            var ingredientSalesItem = await _ingredientSalesItemData.GetByIdAsync(salesItemId, ingredientId);
            return ingredientSalesItem != null ? IngredientSalesItemConverter.ToDto(ingredientSalesItem) : null;
        }

        public async Task<IEnumerable<IngredientSalesItemDto>> GetAllBySalesItemIdAsync(int salesItemId)
        {
            var ingredientSalesItems = await _ingredientSalesItemData.GetAllBySalesItemIdAsync(salesItemId);
            return IngredientSalesItemConverter.ToDtoList(ingredientSalesItems);
        }

        public async Task<IEnumerable<IngredientSalesItemDto>> GetAllAsync()
        {
            var ingredientSalesItems = await _ingredientSalesItemData.GetAllAsync();
            return IngredientSalesItemConverter.ToDtoList(ingredientSalesItems);
        }

        public async Task CustomizeItem(int itemId, List<int> ingredientIdsToAdd, List<int> ingredientIdsToRemove, bool isPartOfComposition = false, int? parentItemId = null)
        {
            if (isPartOfComposition && parentItemId.HasValue)
            {
                // Tjek om SalesItemComposition indeholder det specifikke SalesItem
                var salesItemCompositions = await _salesItemCompositionData.GetByParentItemIdAsync(parentItemId.Value);
                if (!salesItemCompositions.Any(sic => sic.ChildItemId == itemId))
                {
                    throw new InvalidOperationException("Det angivne SalesItem findes ikke i denne komposition.");
                }
            }

            // Tjek, om det specifikke SalesItem kan tilpasses
            var ingredientSalesItems = await _ingredientSalesItemData.GetIngredientSalesItemsBySalesItemIdAsync(itemId);
            if (!ingredientSalesItems.Any())
            {
                throw new InvalidOperationException("Dette SalesItem kan ikke tilpasses med ingredienser.");
            }

            // Tilføj og fjern ingredienser som angivet
            await AddOrRemoveIngredients(itemId, ingredientIdsToAdd, ingredientIdsToRemove);
        }


        private async Task AddOrRemoveIngredients(int salesItemId, List<int> ingredientIdsToAdd, List<int> ingredientIdsToRemove)
            {
                // Tilføj nye ingredienser til det specifikke SalesItem
                foreach (var ingredientId in ingredientIdsToAdd)
                {
                    var existingIngredient = await _ingredientSalesItemData.GetByIdAsync(salesItemId, ingredientId);
                    if (existingIngredient == null)
                    {
                        var ingredientSalesItem = new IngredientSalesItem
                        {
                            SalesItemId = salesItemId,
                            IngredientId = ingredientId
                        };
                        await _ingredientSalesItemData.CreateAsync(ingredientSalesItem);
                    }
                }

                // Fjern ingredienser fra det specifikke SalesItem
                foreach (var ingredientId in ingredientIdsToRemove)
                {
                    var existingIngredient = await _ingredientSalesItemData.GetByIdAsync(salesItemId, ingredientId);
                    if (existingIngredient != null)
                    {
                        await _ingredientSalesItemData.DeleteAsync(salesItemId, ingredientId);
                    }
                }
            }



        }
    }

