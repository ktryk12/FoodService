using System.Collections.Generic;
using FoodService.Modellayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FoodService.DAL
{
    public class DiscountDataManager : IDiscountData
    {
        private readonly IServiceContext _context;


        public DiscountDataManager(IServiceContext context)
        {
            _context = context;

        }

        public async Task<Discount?> GetDiscountByIdAsync(int id)
        {
            return await _context.Discounts
                .Include(d => d.CustomerGroup)
                .Include(d => d.SalesItemGroup)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Discount>> GetAllDiscountsAsync()
        {

            return await _context.Discounts
                    .Include(d => d.CustomerGroup)
                    .Include(d => d.SalesItemGroup)
                    .ToListAsync();

        }

        public async Task<int?> CreateDiscountAsync(Discount discount)
        {
            try
            {
                await _context.Discounts.AddAsync(discount);
                await _context.SaveChangesAsync();
                return discount.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> UpdateDiscountByIdAsync(Discount discountToUpdate)
        {
            try
            {
                var existingDiscount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discountToUpdate.Id);
                if (existingDiscount == null) return false;

                _context.Entry(existingDiscount).CurrentValues.SetValues(discountToUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteDiscountByIdAsync(int id)
        {
            try
            {
                var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);
                if (discount == null) return false;

                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}



