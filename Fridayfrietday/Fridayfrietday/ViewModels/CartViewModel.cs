using Fridayfrietday.Models;

namespace Fridayfrietday.ViewModels
{
    public class CartViewModel
    {
        public List<OrderDetail> CartItems { get; set; } = new List<OrderDetail>();
    }
}
