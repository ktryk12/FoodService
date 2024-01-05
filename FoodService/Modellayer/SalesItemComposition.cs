using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{

    public class SalesItemComposition
    {
       
        public int ParentItemId { get; set; }
        public virtual SalesItem ParentItem { get; set; }
        public int ChildItemId { get; set; }
        public virtual SalesItem ChildItem { get; set; }

        
    }










}


