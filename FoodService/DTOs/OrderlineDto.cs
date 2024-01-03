using System;
using FoodService.Modellayer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodService.DTOs
{
    public partial class OrderlineDto
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int SalesItemId { get; set; }
        public decimal OrderlinePrice { get; set; }
        public int OrderId { get; set; }




    }
}
   




