using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class Sauce
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
