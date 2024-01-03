using FoodService.Modellayer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodService.Modellayer
{
    [Table("Orderline")]
    public partial class Orderline
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal OrderlinePrice { get; set; }

        
        public int OrderId { get; set; }

        
        public int SalesItemId { get; set; }

        
        public virtual SalesItem SalesItem { get; set; }

       
        public virtual ICollection<IngredientOrderline> IngredientOrderlines { get; set; } = new List<IngredientOrderline>();
        public virtual Order Order { get; set; }
       
       
        
    }
}
