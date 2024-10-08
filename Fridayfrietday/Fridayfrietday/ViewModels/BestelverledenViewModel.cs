using Fridayfrietday.Models;
namespace Fridayfrietday.ViewModels;

public class BestelverledenViewModel
{
    public string Email { get; set; } = string.Empty;
    public List<Order>? Orders { get; set; } = new List<Order>();
}


