using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
using FoodService.Modellayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.BusinessLogic
{
    public class ShopService : IShopService
    {
        private readonly IShopData _shopData;

        public ShopService(IShopData shopData)
        {
            _shopData = shopData;
        }

        public async Task<ShopDto> CreateShopAsync(ShopDto shopDto)
        {
            var shop = ShopConverter.ToEntity(shopDto);
            var createdShop = await _shopData.CreateShopAsync(shop);
            return ShopConverter.ToDto(createdShop);
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


        // Yderligere metoder efter behov...
    }
}
