using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{
    [Table("IngredientSalesItem")]
    public partial class IngredientSalesItem
    {
        public int SalesItemId { get; set; }
        public int IngredientId { get; set; }

        public int Min { get; set; }
        public int Max { get; set; }
        public int Count { get; set; } = 0;

        public Ingredient Ingredient { get; set; }
        public SalesItem SalesItem { get; set; }
    }
}
