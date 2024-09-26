using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; } // Navigation property for order
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; } // Navigation property for product
        [Required]
        public int Quantity { get; set; }
        public List<OrderDetailSauce> SelectedSauces { get; set; } = [];

    }
}
