using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Modellayer
{
    public enum ShopType
    {
        Candystor,
        Foodstand,
        Resturant 
    }


    [Table("Shop")]
    public partial class Shop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(500)] 
        public string Location { get; set; } = null!;

        public ShopType Type { get; set; } // Using the enum

        // Navigation property for a collection of OrderData related to this Shop
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Navigation property for a collection of Products associated with this Shop
        public ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
    }
}
