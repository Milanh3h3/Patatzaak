using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
