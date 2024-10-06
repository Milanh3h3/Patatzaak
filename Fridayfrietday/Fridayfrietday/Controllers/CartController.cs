using Fridayfrietday.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Fridayfrietday;

namespace Fridayfrietday.Controllers;
public class CartController : Controller
{
    private readonly DBContext _context;

    public CartController(DBContext context)
    {
        _context = context;
    }
    public IActionResult ViewCart()
    {
        var orders = GetCartFromSession();

        // Load sauce details based on SauceId
        foreach (var order in orders)
        {
            foreach (var orderDetailSauce in order.SelectedSauces)
            {
                // Fetch the Sauce entity using the SauceId
                orderDetailSauce.Sauce = _context.Sauces.FirstOrDefault(s => s.Id == orderDetailSauce.SauceId);
            }
        }

        return View(orders);
    }


    [HttpPost]
    public IActionResult AddToCart(int productId, string selectedSauces, int quantity = 1)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            return NotFound();
        }

        List<OrderDetailSauce> orderDetailSauces = new List<OrderDetailSauce>(); // Initialize an empty list for sauces

        // Check if selectedSauces is not null or empty
        if (!string.IsNullOrEmpty(selectedSauces))
        {
            // Deserialize the JSON string to a list of integers
            List<int> sauceIds = JsonConvert.DeserializeObject<List<int>>(selectedSauces);

            // Create OrderDetailSauce objects
            orderDetailSauces = sauceIds.Select(sauceId => new OrderDetailSauce
            {
                SauceId = sauceId // Store the SauceId
            }).ToList();
        }

        var orderDetail = new OrderDetail
        {
            ProductId = productId,
            Product = product,
            Quantity = quantity,
            SelectedSauces = orderDetailSauces // Store OrderDetailSauces directly
        };

        var cart = GetCartFromSession();
        cart.Add(orderDetail);
        SaveCartToSession(cart); // Save the entire cart including sauces

        return Json(new { success = true, message = "Product added to cart." });
    }




    // Retrieve cart from session
    private List<OrderDetail> GetCartFromSession()
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        if (string.IsNullOrEmpty(cartJson))
        {
            return new List<OrderDetail>();
        }

        // Deserialize the cart and include OrderDetailSauces
        return JsonConvert.DeserializeObject<List<OrderDetail>>(cartJson);
    }


    // Save cart to session
    private void SaveCartToSession(List<OrderDetail> cart)
    {
        var cartJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("Cart", cartJson);
    }


    public IActionResult Cart()
    {
        var cart = GetCartFromSession();
        return View(cart);
    }

    public IActionResult ConfirmOrder()
    {
        var cart = GetCartFromSession();
        var newOrder = new Order
        {
            TotalPrice = cart.Sum(od => od.Product.Price * od.Quantity + od.SelectedSauces.Sum(s => s.Sauce.Price)),
            CustomerId = 1, // Set based on the logged-in customer
            OrderDetails = cart
        };

        _context.Orders.Add(newOrder);
        _context.SaveChanges();

        // Clear cart after saving
        SaveCartToSession(new List<OrderDetail>());

        return RedirectToAction("OrderConfirmation");
    }
}
