using FoodService.DTOs;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using FoodService.Dto_sConverter;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DAL;
using FoodService.DtosConverter;

namespace FoodService.BusinessLogic
{
    public class SalesItemService : ISalesItemService
    {
        private readonly ISalesItemData _salesItemData;
        private readonly ISalesItemCompositionData _salesItemCompositionData;
        private readonly IImageData _imageData;

        public SalesItemService(ISalesItemData salesItemData, IImageData imageData, ISalesItemCompositionData salesItemCompositionData)
        {
            _salesItemData = salesItemData;
            _salesItemCompositionData = salesItemCompositionData;
            _imageData = imageData;
        }

        public async Task<SalesItemDto> CreateSalesItemAsync(CreateSalesItemDto createSalesItemDto)
        {
            var salesItem = CreateSalesItemConverter.ToEntity(createSalesItemDto);

            if (createSalesItemDto.ImageFile != null)
            {
                salesItem.ImageUrl = await _imageData.SaveImageAsync(createSalesItemDto.ImageFile);
            }
            else
            {
                salesItem.ImageUrl = "standardImageUrl.jpg"; // Standardbillede
            }

            var createdSalesItem = await _salesItemData.CreateSalesItemAsync(salesItem);

            if (createdSalesItem != null)
            {
                return SalesItemConverter.ToDto(createdSalesItem); // Brug en passende konverter til at lave SalesItemDto
            }
            else
            {
                // Håndter fejl her
                throw new Exception("Fejl ved oprettelse af SalesItem.");
            }
        }




        public async Task<SalesItem> GetSalesItemByIdAsync(int id)
        {
            return await _salesItemData.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SalesItem>> GetAllSalesItemsAsync()
        {
            return await _salesItemData.GetAllAsync();
        }

        public async Task<bool> UpdateSalesItemAsync(SalesItemDto salesItemDto)
        {
            var salesItem = SalesItemConverter.ToEntity(salesItemDto);
            var updatedSalesItem = await _salesItemData.UpdateAsync(salesItem);
            return updatedSalesItem != null;
        }

        public async Task<bool> DeleteSalesItemAsync(int id)
        {
            return await _salesItemData.DeleteAsync(id);
        }

        public async Task<IEnumerable<SalesItemDto>> GetSalesItemsByCategoryAsync(string category)
        {
            var salesItems = await _salesItemData.GetSalesItemsByCategoryAsync(category);
            var salesItemsList = salesItems.ToList(); // Konverterer IEnumerable til List
            return SalesItemConverter.ToDtoList(salesItemsList);
        }
        public async Task<IEnumerable<SalesItemDto>> GetSalesItemsByIsCompositeAsync(bool isComposite)
        {
            var salesItems = await _salesItemData.GetSalesItemsByIsComposite(isComposite);
            var salesItemsList = salesItems.ToList(); // Konverterer IEnumerable til List
            return SalesItemConverter.ToDtoList(salesItemsList);
        }
    }
}
        




