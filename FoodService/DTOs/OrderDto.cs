using System;
using FoodService.Modellayer;
using System.Collections.Generic;

namespace FoodService.DTOs
{
    public partial class OrderDto
    {
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public DateTime Datetime { get; set; }

        public decimal Total { get; set; }

        public int ShopId { get; set; }



    }
}
