using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DAL.Interfaces;
using FoodService.DTOs;
using FoodService.Dto_sConverter;
using FoodService.BusinessLogic.PriceService;
using FoodService.Modellayer;


public class OrderlineService : IOrderlineService
{
    private readonly IOrderlineData _orderlineData;
    private readonly ISalesItemService _salesItemService;
    private readonly IIngredientSalesItemData _ingredientSalesItemData;

    public OrderlineService(IOrderlineData orderlineData, ISalesItemService salesItemService, IIngredientSalesItemData ingredientSalesItemData)
    {
        _orderlineData = orderlineData;
        _salesItemService = salesItemService;
        _ingredientSalesItemData = ingredientSalesItemData;
    }


    public async Task<OrderlineDto> CreateOrderlineAsync(OrderlineDto orderlineDto)
    {
        var orderline = OrderlineConverter.ToEntity(orderlineDto);

        // Hent SalesItem
        var salesItem = await _salesItemService.GetSalesItemByIdAsync(orderline.SalesItemId);

        // Beregn og sæt ordrelinjeprisen
        orderline.OrderlinePrice = await CalculateOrderlinePriceAsync(salesItem);

        // Gem ordrelinjen og returnér DTO
        var createdOrderline = await _orderlineData.CreateOrderlineAsync(orderline);
        return OrderlineConverter.toDto(createdOrderline);
    }

    private async Task<decimal> CalculateOrderlinePriceAsync(SalesItem salesItem)
    {
        decimal price = salesItem.BasePrice;

        // Hent og tilføj prisen for ekstra ingredienser
        var ingredientSalesItems = await _ingredientSalesItemData.GetAllBySalesItemIdAsync(salesItem.Id);
        foreach (var ingredientSalesItem in ingredientSalesItems)
        {
            price += ingredientSalesItem.Ingredient.IngredientPrice * ingredientSalesItem.Count;
        }

        return price;
    }



    public async Task<bool> UpdateOrderlineAsync(OrderlineDto orderlineDto)
    {
        var orderline = OrderlineConverter.ToEntity(orderlineDto);
        return await _orderlineData.UpdateOrderlineAsync(orderline);
    }

    public async Task<OrderlineDto> GetOrderlineByIdAsync(int id)
    {
        var orderline = await _orderlineData.GetOrderlineByIdAsync(id);
        return orderline != null ? OrderlineConverter.toDto(orderline) : null; 
    }

    public async Task<List<OrderlineDto>> GetAllOrderlinesAsync()
    {
        var orderlines = await _orderlineData.GetAllOrderlinesAsync();
        return orderlines.Select(orderline => OrderlineConverter.toDto(orderline)).ToList(); 
    }

    public async Task<bool> DeleteOrderlineAsync(int id)
    {
        return await _orderlineData.DeleteOrderlineAsync(id);
    }
}

