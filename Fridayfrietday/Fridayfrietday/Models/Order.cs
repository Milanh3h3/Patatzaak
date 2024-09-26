using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fridayfrietday.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; } // Navigation property for customer
        public List<OrderDetail>? OrderDetails { get; set; } // List of order details (multiple products)

    }
}
