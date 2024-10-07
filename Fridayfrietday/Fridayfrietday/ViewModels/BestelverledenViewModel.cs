using Fridayfrietday.Models;
namespace Fridayfrietday.ViewModels;

public class BestelverledenViewModel
{
    public string Email { get; set; } = string.Empty;
    public List<Order> Orders { get; set; } = new List<Order>();
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public List<OrderDetailSauce> OrderDetailSauces { get; set; } = new List<OrderDetailSauce>();
}

