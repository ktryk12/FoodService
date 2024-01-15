using System;
using FoodService.Modellayer;
using System.Collections.Generic;

namespace FoodService.DTOs
{
    public partial class IngredientSalesItemDto
    {
        public int SalesItemId { get; set; }

        public int IngredientId { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public int Count { get; set; }

        public int StandardCount { get; set; }
    }
}

