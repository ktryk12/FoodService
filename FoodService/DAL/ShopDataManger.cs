using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodService.DAL.Interfaces;
using FoodService.Modellayer;
using FoodService.DTOs;

namespace FoodService.DAL
{
    public class ShopDataManager : IShopData
    {
        private readonly IServiceContext _context;

        public ShopDataManager(IServiceContext context)
        {
            _context = context;
        }

        public async Task<Shop> CreateShopAsync(Shop shop)
        {
            try
            {
                _context.Add(shop);
                await _context.SaveChangesAsync();
                return shop;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<Shop> GetShopByIdAsync(int id)
        {
            return await _context.Shops.FirstOrDefaultAsync(s => s.Id == id);
        }

       
        public async Task<bool> UpdateShopAsync(Shop shop)
        {
            try
            {
                _context.Entry(shop).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteShopAsync(int id)
        {
            try
            {
                var shop = await _context.Shops.FindAsync(id);
                if (shop != null)
                {
                    _context.Shops.Remove(shop);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<bool> AddSalesItemToShopAsync(int shopId, SalesItem item)
        {
            var shop = await _context.Shops.FindAsync(shopId);
            if (shop == null)
            {
                return false; // Shop not found
            }

            shop.SalesItems ??= new List<SalesItem>();
            shop.SalesItems.Add(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveSalesItemFromShopAsync(int shopId, int salesItemId)
        {
            var shop = await _context.Shops.FindAsync(shopId);
            if (shop == null)
            {
                return false; // Shop not found
            }

            // Check if shop has SalesItems and find the item to remove
            var salesItemToRemove = shop.SalesItems?.FirstOrDefault(si => si.Id == salesItemId);
            if (salesItemToRemove == null)
            {
                return false; // SalesItem not found in the shop
            }

            shop.SalesItems.Remove(salesItemToRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await _context.Shops.ToListAsync();
        }
        public async Task<List<int>> GetSalesItemIdsByShopIdAsync(int shopId)
        {
            var shop = await _context.Shops
                                     .Include(s => s.SalesItems)
                                     .FirstOrDefaultAsync(s => s.Id == shopId);

            if (shop == null)
            {
                return new List<int>(); // Returner en tom liste, hvis shop ikke findes
            }

            return shop.SalesItems.Select(si => si.Id).ToList();
        }

    }
}

