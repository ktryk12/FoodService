using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodService.DAL.Interfaces;
using FoodService.Modellayer;

namespace FoodService.DAL
{
    public class OrderManager : IOrderData
    {
        private readonly IServiceContext _context;

        public OrderManager(IServiceContext context)
        {
            _context = context;
        }

        public async Task<(int? Id, int OrderNumber)> CreateOrderAsync(Order order)
        {
            try
            {
                // Generate order number
                int nextOrderNumber = await _context.Order.MaxAsync(o => (int?)o.OrderNumber) ?? 0;
                nextOrderNumber = nextOrderNumber >= 1000 ? 1 : nextOrderNumber + 1;
                order.OrderNumber = nextOrderNumber;

                // Add and save order
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
                return (order.Id, order.OrderNumber); // Returning both Id and OrderNumber
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null, 0); // Return null and 0 in case of an exception
            }
        }


        public async Task<bool> UpdateOrderAsync(Order orderToUpdate)
        {
            try
            {
                var existingOrder = await _context.Order.FindAsync(orderToUpdate.Id);
                if (existingOrder == null) return false;

                // Update the properties
                existingOrder.OrderNumber = orderToUpdate.OrderNumber;
                existingOrder.Datetime = orderToUpdate.Datetime;
                existingOrder.Total = orderToUpdate.Total;
                existingOrder.ShopId = orderToUpdate.ShopId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Order
                                 .Include(o => o.Orderlines)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Order
                                 .Include(o => o.Orderlines)
                                 .ToListAsync();
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            try
            {
                var order = await _context.Order.Include(o => o.Orderlines).FirstOrDefaultAsync(o => o.Id == id);
                if (order == null) return false;

                // If you want to cascade delete the associated Orderlines when an Order is deleted
                _context.Orderlines.RemoveRange(order.Orderlines);

                _context.Order.Remove(order);
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
