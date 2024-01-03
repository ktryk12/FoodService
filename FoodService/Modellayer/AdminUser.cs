using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodService.Modellayer
{
    public class AdminUser
    {
        [Key]  // This is the primary key for the database table.
        [Column("Id")]
        public int Id { get; set; }
        [Required]  // The field must not be null.
        [MaxLength(30)]  // Maximum length for the string.
        [Column(TypeName = "nvarchar(30)")]
        public string Username { get; set; }
        [Required]  // The field must not be null.
        [MaxLength(300)]  // Maximum length for the string.
        [Column(TypeName = "nvarchar(300)")]
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(300)] // Juster efter behov
        [Column(TypeName = "nvarchar(300)")]
        public string Salt { get; set; }
    }
}
