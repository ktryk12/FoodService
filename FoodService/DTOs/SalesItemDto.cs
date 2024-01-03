namespace FoodService.DTOs
{
    public class SalesItemDto
    {
        // Felter og egenskaber
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string? ImageUrl { get; set; }
        public decimal BasePrice { get; set; }
        public string Category { get; set; }
        public int SalesItemGroupId { get; set; }
        public bool IsActive { get; set; }

        public bool IsComposite { get; set; }
    }   

}


