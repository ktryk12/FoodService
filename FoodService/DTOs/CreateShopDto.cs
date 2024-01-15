using FoodService.Modellayer;


namespace FoodService.DTOs
{
      public class CreateShopDto
    {
        
        public string Name { get; set; }

        public string Location { get; set; }

        public ShopTypeDto Type { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}

