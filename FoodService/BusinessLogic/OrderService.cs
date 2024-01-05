using Microsoft.EntityFrameworkCore;
using FoodService.DAL;
using System.Collections.Generic;
using System.Linq;
using System;
using FoodService.Dto_sConverter;
using FoodService.DAL.Interfaces;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DTOs;
using FoodService.Modellayer;


namespace FoodService.BusinessLogic
{
    public class OrderService : IOrderService
    {
        private readonly IOrderData _orderData;
        private readonly IOrderlineData _orderlineData;
        private readonly IIngredientSalesItemData _ingredientSalesItemData;
        private readonly IIngredientData _ingredientData;
        private readonly IIngredientOrderlineData _ingredientOrderlineData;
        private readonly ISalesItemService _salesItemService;
       



        public OrderService(IOrderData orderData, IOrderlineData orderlineData, IIngredientSalesItemData ingredientSalesItemData,
            IIngredientData ingredientData, IIngredientOrderlineData ingredientOrderlineData, ISalesItemService salesItemService)
        {
            _orderData = orderData;
            _orderlineData = orderlineData;
            _ingredientSalesItemData = ingredientSalesItemData;
            _ingredientData = ingredientData;
            _ingredientOrderlineData = ingredientOrderlineData;
            _salesItemService = salesItemService;
            
        }




        public async Task<(int? Id, int OrderNumber)> CreateOrderAsync(int shopId)
        {
            if (shopId <= 0) throw new ArgumentException("ShopId is invalid.");

            var order = new Order
            {
                ShopId = shopId,
                Datetime = DateTime.Now
            };

            return await _orderData.CreateOrderAsync(order);
        }


        public async Task<bool> UpdateOrderAsync(OrderDto orderDtoToUpdate)
        {
            var orderToUpdate = OrderConverter.ToEntity(orderDtoToUpdate);
            return await _orderData.UpdateOrderAsync(orderToUpdate);
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderData.DeleteOrderAsync(id);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderData.GetOrderByIdAsync(id);
            return order != null ? OrderConverter.ToDto(order) : null;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderData.GetAllOrdersAsync();
            var ordersList = orders.ToList(); // Konverterer IEnumerable til List
            return OrderConverter.ToDtoList(ordersList);
        }



        public async Task UpdateOrderTotalAsync(int orderId)
        {
            var order = await _orderData.GetOrderByIdAsync(orderId);
            if (order == null) throw new InvalidOperationException("Order not found.");

            decimal total = 0;
            foreach (var orderline in order.Orderlines)
            {
                var salesItem = await _salesItemService.GetSalesItemByIdAsync(orderline.SalesItemId);
                decimal orderlineTotal = salesItem.BasePrice;

                // Tilføj prisen for eventuelle ekstra ingredienser
                var ingredientSalesItems = await _ingredientSalesItemData.GetAllBySalesItemIdAsync(salesItem.Id);
                foreach (var ingredientSalesItem in ingredientSalesItems)
                {
                    orderlineTotal += ingredientSalesItem.Ingredient.IngredientPrice * ingredientSalesItem.Count;
                }

                total += orderlineTotal;
            }

            order.Total = total;
            await _orderData.UpdateOrderAsync(order);
        }

    }
}




