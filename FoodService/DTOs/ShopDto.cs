﻿using FoodService.Modellayer;

namespace FoodService.DTOs
{

  

    public class ShopDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public ShopTypeDto Type   { get; set; }

        public string? ImageUrl { get; set; }



    }
}
