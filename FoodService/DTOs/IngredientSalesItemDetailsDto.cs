
namespace FoodService.DTOs
{
    public partial class IngredientSalesItemDetailsDto
    {
        public int SalesItemId { get; set; }
        public int IngredientId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Count { get; set; }

        // Denne egenskab indeholder detaljerede oplysninger om ingrediensen
        public IngredientDto Ingredient { get; set; }
    }
}
