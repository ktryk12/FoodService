using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{
    [Table("Discount")]
    public partial class Discount
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column(TypeName = "decimal(5, 2)")]  // Eksempel på angivelse af decimal precision.
        public decimal? Rate { get; set; }

  
        public int? SalesItemGroupId { get; set; }

       
        public int? CustomerGroupId { get; set; }

        // Navigationsegenskaber for at repræsentere relationen til andre klasser.
        public virtual CustomerGroup? CustomerGroup { get; set; }

        public virtual SalesItemGroup? SalesItemGroup { get; set; }
    }
}
