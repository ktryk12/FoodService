using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
using FoodService.Modellayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.DAL;
using FoodService.DtosConverter;

namespace FoodService.BusinessLogic
{
    public class ShopService : IShopService
    {
        private readonly IShopData _shopData;
        private readonly IImageData _imageData;

        public ShopService(IShopData shopData, IImageData imageData)
        {
            _shopData = shopData;
            _imageData = imageData;
        }

        public async Task<ShopDto> CreateShopAsync(CreateShopDto createShopDto)
        {
            // Convert DTO to an entity
            var shop = CreateShopConverter.ToEntity(createShopDto);

            // Handle image upload
            if (createShopDto.ImageFile != null)
            {
                shop.ImageUrl = await _imageData.SaveImageAsync(createShopDto.ImageFile);
            }
            else
            {
                shop.ImageUrl = "standardImageUrl.jpg"; // Standard image
            }

            // Create shop and return as DTO
            var createdShop = await _shopData.CreateShopAsync(shop);
            return ShopConverter.ToDto(createdShop); // Convert to ShopDto instead of CreateShopDto
        }

        public async Task<ShopDto> GetShopByIdAsync(int id)
        {
            var shop = await _shopData.GetShopByIdAsync(id);
            return shop != null ? ShopConverter.ToDto(shop) : null;
        }

        public async Task<bool> UpdateShopAsync(ShopDto shopDto)
        {
            var shop = ShopConverter.ToEntity(shopDto);
            return await _shopData.UpdateShopAsync(shop);
        }

        public async Task<bool> DeleteShopAsync(int id)
        {
            return await _shopData.DeleteShopAsync(id);
        }

        public async Task<bool> AddSalesItemToShopAsync(int shopId, SalesItemDto itemDto)
        {
            var item = SalesItemConverter.ToEntity(itemDto);
            return await _shopData.AddSalesItemToShopAsync(shopId, item);
        }

        public async Task<bool> RemoveSalesItemFromShopAsync(int shopId, int salesItemId)
        {
            return await _shopData.RemoveSalesItemFromShopAsync(shopId, salesItemId);
        }
        public async Task<IEnumerable<ShopDto>> GetAllShopsAsync()
        {
            var shops = await _shopData.GetAllShopsAsync();
            var shopsList = shops.ToList(); // Konverterer IEnumerable til List
            return ShopConverter.ToDtoList(shopsList);
        }
        public async Task<List<int>> GetSalesItemIdsByShopIdAsync(int shopId)
        {
            return await _shopData.GetSalesItemIdsByShopIdAsync(shopId);
        }


        // Yderligere metoder efter behov...
    }
}
