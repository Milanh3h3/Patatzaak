using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class OrderDetailSauce
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrderDetail")]
        public int OrderDetailId { get; set; }

        public OrderDetail? OrderDetail { get; set; }

        [ForeignKey("Sauce")]
        public int SauceId { get; set; }

        public Sauce? Sauce { get; set; }
    }
}
