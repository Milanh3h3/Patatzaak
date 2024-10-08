using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fridayfrietday;
using Fridayfrietday.Models;
using Fridayfrietday.ViewModels;
using Fridayfrietday.Controllers;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Fridayfrietday.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DBContext _context;

        public OrdersController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Bestelverleden(string email)
        {
            var viewModel = new BestelverledenViewModel { Email = email };

            if (!string.IsNullOrEmpty(email))
            {
                // Fetch customer and include their orders, order details, and selected sauces
                var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

                if (customer != null)
                {
                    // Haal de orders van de klant op, inclusief de benodigde navigatie-eigenschappen
                    viewModel.Orders = _context.Orders
                        .Where(o => o.CustomerId == customer.Id)
                        .OrderByDescending(o => o.OrderDate) // Sorteer de orders aflopend op OrderDate
                        .Include(o => o.OrderDetails) // Include order details for each order
                        .ThenInclude(od => od.Product) // Include the product for each order detail
                        .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.SelectedSauces) // Include selected sauces for each order detail
                        .ThenInclude(ods => ods.Sauce) // Include the sauce details
                        .ToList();
                }

            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Reorder(int orderId)
        {
            // Find the order and its details
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.SelectedSauces)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("Bestelverleden");
            }

            List<OrderDetail> cart = GetCartFromSession();

            // Add each OrderDetail from the old order to the cart
            foreach (var orderDetail in order.OrderDetails)
            {
                var cartItem = new OrderDetail
                {
                    ProductId = orderDetail.ProductId,
                    Product = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == orderDetail.ProductId), // read cartcontroller
                    Quantity = orderDetail.Quantity,
                    SelectedSauces = orderDetail.SelectedSauces.Select(s => new OrderDetailSauce
                    {
                        SauceId = s.SauceId
                    }).ToList()
                };

                cart.Add(cartItem);
            }

            SaveCartToSession(cart);

            // Redirect to the View Cart page
            return RedirectToAction("ViewCart", "Cart");
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

        public IActionResult Index()
        {

            IEnumerable<Order> orders = [];
            orders = _context.Orders
                .Where(o => o.OrderDate > DateTime.Now.AddDays(-1))
                .OrderByDescending(o => o.OrderDate) // Sorteer de orders aflopend op OrderDate
                .Include(o => o.OrderDetails) // Include order details for each order
                .ThenInclude(od => od.Product) // Include the product for each order detail
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.SelectedSauces) // Include selected sauces for each order detail
                .ThenInclude(ods => ods.Sauce) // Include the sauce details
                .ToList();
            
            return View(orders);
        }
        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string OrderStatus)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus;
                _context.SaveChanges();
            }

            return RedirectToAction("Index"); // Verander indien nodig naar de juiste actie
        }

    }
}