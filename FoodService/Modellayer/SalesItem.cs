using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{

    [Table("SalesItem")]  // Specifies the name of the database table that this class is mapped to.
    public class SalesItem
    {
        [Key]  // This is the primary key for the database table.
        
        public int Id { get; set; }

        [Required]  // The field must not be null.
        [MaxLength(30)]  // Maximum length for the string.
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }

        [Required]  // The field must not be null.
        [MaxLength(30)]  // Maximum length for the string. Adjust as needed.
        [Column(TypeName = "nvarchar(30)")]
        public string ProductNumber { get; set; }
        [Required]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal BasePrice { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Category { get; set; } = null!;

        
        public bool IsActive { get; set; } = true;

        
        public bool IsComposite { get; set; } = false;
       
       
        public int SalesItemGroupId { get; set; }
        public SalesItemGroup SalesItemGroup { get; set; }

        public ICollection<Shop> Shops { get; set; }
        public virtual ICollection<SalesItemComposition> ParentCompositions { get; set; }
        public virtual ICollection<SalesItemComposition> ChildCompositions { get; set; }
        public ICollection<IngredientSalesItem> IngredientSalesItems { get; set; } = new List<IngredientSalesItem>();
        public ICollection<Orderline> Orderlines { get; set; } = new List<Orderline>();

    }
   

}


