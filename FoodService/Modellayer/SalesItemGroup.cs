using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{
    [Table("SalesItemGroup")]
    public partial class SalesItemGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        // Navigationsegenskab til en samling af Discounts, der relaterer til denne SalesItemGroup
        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

        // Navigationsegenskab til en samling af Products, der hører til denne ProductGroup
        public virtual ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
    }
}

