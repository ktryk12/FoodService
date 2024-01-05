using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL
{
    public class OrderlineDataManager : IOrderlineData
    {
        private readonly IServiceContext _context;

        public OrderlineDataManager(IServiceContext context)
        {
            _context = context;
        }
        public async Task<List<Orderline>> GetOrderlinesByOrderIdAsync(int orderId)
        {
            return await _context.Orderlines
                .Where(ol => ol.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<Orderline> GetOrderlineWithSalesItemAsync(int orderlineId)
        {
            return await _context.Orderlines
                .Include(ol => ol.SalesItem)
                .FirstOrDefaultAsync(ol => ol.Id == orderlineId);
        }

        public async Task<List<SalesItemComposition>> GetSalesItemCompositionsByOrderlineAsync(int orderlineId)
        {
            return await _context.SalesItemCompositions
                .Where(sic => sic.ParentItemId == orderlineId || sic.ChildItemId == orderlineId)
                .ToListAsync();
        }

        public async Task<Orderline> CreateOrderlineAsync(Orderline orderline)
        {
            await _context.Orderlines.AddAsync(orderline);
            await _context.SaveChangesAsync();
            return orderline;
        }
        public async Task<Orderline> GetOrderlineByIdAsync(int id)
        {
            return await _context.Orderlines
                .FirstOrDefaultAsync(ol => ol.Id == id);
        }

        public async Task<List<Orderline>> GetAllOrderlinesAsync()
        {
            return await _context.Orderlines.ToListAsync();
        }
        public async Task<bool> UpdateOrderlineAsync(Orderline orderline)
        {
            var existingOrderline = await _context.Orderlines.FindAsync(orderline.Id);
            if (existingOrderline == null) return false;
                   
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderlineAsync(int orderlineId)
        {
            var orderline = await _context.Orderlines.FindAsync(orderlineId);
            if (orderline == null) return false;

            _context.Orderlines.Remove(orderline);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}





