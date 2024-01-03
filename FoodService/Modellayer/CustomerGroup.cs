using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Modellayer
{
    [Table("CustomerGroup")]  // Angiver navnet på databasetabellen, som denne klasse er knyttet til.
    public partial class CustomerGroup
    {
        [Key]  // Dette er primary key for databasetabellen.
        [Column("Id")]  // Navnet på kolonnen i databasetabellen.
        public int Id { get; set; }

        [Required]  // Feltet må ikke være null.
        [MaxLength(50)]  // Maksimal længde for strengen.
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        // Navigationsegenskab for at repræsentere en-til-mange relationen med Discount.
        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}

