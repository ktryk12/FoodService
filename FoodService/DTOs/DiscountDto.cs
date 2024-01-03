using System;
using FoodService.Modellayer;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodService.DTOs
{
    public partial class DiscountDto
    {
        public int Id { get; set; }

        public decimal? Rate { get; set; }

        public int? SalesItemGroupId { get; set; }

        public int? CustomerGroupId { get; set; }
       


    }
}
