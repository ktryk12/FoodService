

using FoodService.Modellayer;

namespace FoodService.DTOs
{
    public class SalesItemCompositionDto
    {
        public int ParentItemId { get; set; }
        public string ParentItemName { get; set; } 
        public int ChildItemId { get; set; }
        public string ChildItemName { get; set; }

    }
}
