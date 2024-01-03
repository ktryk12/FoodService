

using FoodService.Modellayer;

namespace FoodService.DTOs
{
    public class SalesItemCompositionDto
    {
        public int ParentItemId { get; set; }
        public string ParentItemName { get; set; } // Antager at du kun har brug for navnet
        public int ChildItemId { get; set; }
        public string ChildItemName { get; set; }

    }
}
