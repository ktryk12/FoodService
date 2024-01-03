using FoodService.DTOs;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using FoodService.Dto_sConverter;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DtosConverter;
using FoodService.DAL;

namespace FoodService.BusinessLogic
{
    public class SalesItemCompositionService : ISalesItemCompositionService
    {
        private readonly ISalesItemCompositionData _salesItemCompositionData;
        private readonly ISalesItemData _salesItemData;


        public SalesItemCompositionService(ISalesItemData salesItemData, ISalesItemCompositionData salesItemCompositionData)
        {
            _salesItemCompositionData = salesItemCompositionData;
            _salesItemData = salesItemData;
        }
        public async Task<SalesItemComposition> CreateSalesItemCompositionAsync(SalesItemCompositionDto salesItemCompositionDto)
        {
            var salesItemComposition = SalesItemCompositionConverter.ToEntity(salesItemCompositionDto);
            return await _salesItemCompositionData.AddAsync(salesItemComposition);
        }

        public async Task<SalesItemComposition> GetSalesItemCompositionByIdAsync(int parentItemId, int childItemId)
        {
            return await _salesItemCompositionData.GetByIdAsync(parentItemId, childItemId);
        }

        public async Task<IEnumerable<SalesItemCompositionDto>> GetCompositionsByParentItem(int parentItemId)
        {
            var compositions = await _salesItemCompositionData.GetByParentItemIdAsync(parentItemId);
            return compositions.Select(c => new SalesItemCompositionDto
            {
                ParentItemId = c.ParentItemId,
                ParentItemName = c.ParentItem?.Name, // Navn på forældreelementet, hvis det er nødvendigt
                ChildItemId = c.ChildItemId,
                ChildItemName = c.ChildItem?.Name // Navn på barnelementet
            }).ToList();
        }

        public async Task<IEnumerable<SalesItemCompositionDto>> GetAllCompositionsAsync()
        {
            var compositions = await _salesItemCompositionData.GetAllAsync();
            return compositions.Select(c => new SalesItemCompositionDto
            {
                ParentItemId = c.ParentItemId,
                ParentItemName = c.ParentItem?.Name,
                ChildItemId = c.ChildItemId,
                ChildItemName = c.ChildItem?.Name
            }).ToList();
        }


        public async Task<bool> UpdateSalesItemCompositionAsync(SalesItemCompositionDto salesItemCompositionDto)
        {
            var salesItemComposition = SalesItemCompositionConverter.ToEntity(salesItemCompositionDto);
            var updatedComposition = await _salesItemCompositionData.UpdateAsync(salesItemComposition);
            return updatedComposition != null;
        }

        public async Task<bool> DeleteSalesItemCompositionAsync(int parentItemId, int childItemId)
        {
            return await _salesItemCompositionData.DeleteAsync(parentItemId, childItemId);
        }
        public async Task<SalesItemCompositionWithDetailsDto> GetCompositionWithDetailsAsync(int parentItemId)
        {
            // Henter det specifikke parent item
            var parentItem = await _salesItemData.GetByIdAsync(parentItemId);
            if (parentItem == null)
            {
                // Håndter tilfælde, hvor parent item ikke findes
                return null;
            }

            // Henter alle SalesItemComposition baseret på parentItemId
            var compositions = await _salesItemCompositionData.GetByParentItemIdAsync(parentItemId);

            // Kontrollerer, om der findes nogen child items
            if (!compositions.Any())
            {
                // Hvis der ikke er child items, kan du vælge at returnere data kun for parent item
                // Eller returnere null, afhængigt af din applikations logik
            }

            // Konverterer til DTO
            return SalesItemCompositionWithDetailsConverter.ConvertToDto(
                parentItem, // Dette er nu det fulde SalesItem objekt for parent item
                compositions.Select(c => c.ChildItem) // Liste af child items
            );
        }

    }
}





