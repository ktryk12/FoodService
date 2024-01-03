using FoodService.Modellayer;

namespace FoodService.DTOs
{
    public class SalesItemCompositionWithDetailsDto
    {
        public SalesItemDto ParentItem { get; set; }
        public List<SalesItemDto> ChildItems { get; set; } = new List<SalesItemDto>();
    }
}
