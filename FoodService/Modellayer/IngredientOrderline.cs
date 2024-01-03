using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{
    [Table("IngredientOrderline")]
    public partial class IngredientOrderline
    {

        public int IngredientId { get; set; }

       
        public int OrderlineId { get; set; }

        public int Delta { get; set; }

        // Navigationsegenskaber for at repræsentere relationen til andre klasser.
        public virtual Ingredient Ingredient { get; set; } = null!;

        public virtual Orderline Orderline { get; set; } = null!;
    }
}

