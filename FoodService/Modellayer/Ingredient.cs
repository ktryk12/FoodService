using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.Modellayer
{
    [Table("Ingredient")]
    public partial class Ingredient
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]

        public string Name { get; set; } = null!;

        [Required]  // The field must not be null.
        [MaxLength(500)]  // Maximum length for the string. Adjust as needed.
        [Column(TypeName = "nvarchar(500)")]
        public string ImageUrl { get; set; }  // Changed from Image to ImageUrl to better reflect its purpose.
        [Column(TypeName = "decimal(10, 2)")]
        public decimal IngredientPrice { get; set; }
        // Navigationsegenskab for at repræsentere mange-til-mange relationen med IngeredientOrderlines.
        public virtual ICollection<IngredientOrderline> IngredientOrderlines { get; set; } = new List<IngredientOrderline>();
        // Navigationsegenskab for at repræsentere mange-til-mange relationen med IngredientProduct.
        public virtual ICollection<IngredientSalesItem> IngredientSalesItems { get; set; } = new List<IngredientSalesItem>();
    }
}