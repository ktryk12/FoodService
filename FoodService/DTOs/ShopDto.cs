using FoodService.Modellayer;

namespace FoodService.DTOs
{

    public enum ShopTypeDto
    {
        Candystor,
        Foodstand,
        Resturant
    }

    public class ShopDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public ShopType Type   { get; set; }

    }
}
