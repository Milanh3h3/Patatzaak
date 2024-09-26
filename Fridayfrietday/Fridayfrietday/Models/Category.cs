using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string Name { get; set; }
        public string Picture { get; set; } // Image for the category
    }
}
