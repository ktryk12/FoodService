namespace FoodService.DTOs
{
    public class CreateSalesItemDto
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public decimal BasePrice { get; set; }
        public string Category { get; set; }
        public int SalesItemGroupId { get; set; }
        public bool IsActive { get; set; }
        public bool IsComposite { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
