using System.ComponentModel.DataAnnotations;

namespace FoodService.DTOs
{
    public class CreateIngredientDto
    {


        public string Name { get; set; } = null!;
        public decimal IngredientPrice { get; set; }
        public IFormFile ImageFile { get; set; }

    }

}
