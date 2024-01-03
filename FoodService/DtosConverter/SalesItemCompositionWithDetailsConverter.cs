using FoodService.Modellayer;
using FoodService.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace FoodService.DtosConverter
{
    public static class SalesItemCompositionWithDetailsConverter
    {
        public static SalesItemCompositionWithDetailsDto ConvertToDto(SalesItem parentItem, IEnumerable<SalesItem> childItems)
        {
            var parentItemDto = ConvertToSalesItemDto(parentItem); // Konverterer SalesItem til SalesItemDto
            var childItemsDto = childItems.Select(ConvertToSalesItemDto).ToList(); // Gør det samme for hvert child item

            return new SalesItemCompositionWithDetailsDto
            {
                ParentItem = parentItemDto, // Nu er dette en SalesItemDto
                ChildItems = childItemsDto // Og dette er en List<SalesItemDto>
            };
        }


        private static SalesItemDto ConvertToSalesItemDto(SalesItem salesItem)
        {
            if (salesItem == null)
                return null;

            return new SalesItemDto
            {
                Id = salesItem.Id,
                Name = salesItem.Name,
                ProductNumber = salesItem.ProductNumber,
                ImageUrl = salesItem.ImageUrl,
                BasePrice = salesItem.BasePrice,
                Category = salesItem.Category,
                SalesItemGroupId = salesItem.SalesItemGroupId,
                // Andre relevante felter...
            };
        }
    }
}

