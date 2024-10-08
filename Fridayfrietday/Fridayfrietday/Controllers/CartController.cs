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
        var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            return NotFound();
        }

        List<OrderDetailSauce> orderDetailSauces = new List<OrderDetailSauce>();

        if (!string.IsNullOrEmpty(selectedSauces))
        {
            List<int> sauceIds = JsonConvert.DeserializeObject<List<int>>(selectedSauces);

            orderDetailSauces = sauceIds.Select(sauceId => new OrderDetailSauce
            {
                SauceId = sauceId
            }).ToList();
        }

        var orderDetail = new OrderDetail
        {
            ProductId = productId,
            Product = product, // Using AsNoTracking() above prevents EF from thinking this is a new Product.
            Quantity = quantity,
            SelectedSauces = orderDetailSauces
        };

        var cart = GetCartFromSession();
        cart.Add(orderDetail);
        SaveCartToSession(cart);

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmOrder(string email)
    {
        var cart = GetCartFromSession();

        if (cart == null || !cart.Any())
        {
            TempData["ErrorMessage"] = "No items in the cart to confirm.";
            return RedirectToAction("Cart");
        }

        // Find or create customer based on email
        var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
        if (customer == null)
        {
            customer = new Customer { Email = email }; // Create new customer
            _context.Customers.Add(customer);
            _context.SaveChanges(); // Save to get the ID
        }

        // get totalprice
        double totalprice = 0;
        foreach (OrderDetail detail in cart) 
        {
            double totalsauceprice = 0;
            foreach (OrderDetailSauce? ods in detail.SelectedSauces) 
            {
                ods.Sauce =_context.Sauces.FirstOrDefault(c => c.Id == ods.SauceId);
                if (ods.Sauce != null)
                {
                    totalsauceprice += ods.Sauce.Price;
                }

            } 
            totalprice += (totalsauceprice + detail.Product.Price) * detail.Quantity;
        }


        // Create a new order and initialize OrderDetails
        var newOrder = new Order
        {
            TotalPrice = totalprice,
            CustomerId = customer.Id,
            OrderDate = DateTime.Now,
            OrderDetails = new List<OrderDetail>() // Initialize the OrderDetails collection
        };

        // Prepare order details without attaching existing products
        foreach (var orderDetail in cart)
        {
            // Retrieve the existing product by its ID
            var existingProduct = _context.Products.Find(orderDetail.ProductId);
            if (existingProduct != null)
            {
                // Create a new OrderDetail with just the ProductId
                var newOrderDetail = new OrderDetail
                {
                    ProductId = existingProduct.Id, // Use existing product ID
                    Quantity = orderDetail.Quantity,
                    SelectedSauces = orderDetail.SelectedSauces // Keep the selected sauces
                };
                newOrder.OrderDetails.Add(newOrderDetail); // This should now work
            }
        }

        // Add and save the order
        _context.Orders.Add(newOrder);
        _context.SaveChanges(); // This should work without errors now

        var today = DateTime.Today;
        var orderCount = _context.Orders.Count(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == today);
        newOrder.PickupNumber = int.Parse($"{today:yyMMdd}{orderCount.ToString().PadLeft(3, '0')}");
        _context.SaveChanges(); // Save pickup number

        SaveCartToSession(new List<OrderDetail>()); // Clear cart after saving


        return RedirectToAction("Bestelverleden", "Orders", new { email });

    }

}
