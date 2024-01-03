using System;
using FoodService.Modellayer;
using System.Collections.Generic;

namespace FoodService.DTOs
{
    public partial class IngredientOrderlineDto
    {
        public int IngredientId { get; set; }

        public int OrderlineId { get; set; }

        public int Delta { get; set; }
        public List<int> IngredientIds { get; set; }

    }
}
