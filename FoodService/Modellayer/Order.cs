using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{
    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int Id { get; set; }

        public int OrderNumber { get; set; }


        public DateTime Datetime { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Total { get; set; }

        public int ShopId { get; set; }

        // Navigationsegenskab til Shop
        public virtual Shop Shop { get; set; } = null!;

        // Navigationsegenskab til en samling af Orderline
        public virtual ICollection<Orderline> Orderlines { get; set; } = new List<Orderline>();
    }
}

