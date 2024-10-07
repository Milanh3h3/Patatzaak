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
                    // Add orders to the view model
                    viewModel.Orders = _context.Orders.Where(o => o.CustomerId == customer.Id).OrderBy(o => o.OrderDate).ToList();
                    foreach(Order order in viewModel.Orders)
                    {
                        viewModel.OrderDetails = _context.OrderDetails.Where(o => o.OrderId == order.Id).Include(p => p.Product).ToList();
                        foreach(OrderDetail orderd in viewModel.OrderDetails)
                        {
                            viewModel.OrderDetailSauces = _context.OrderDetailSauces.Where(o => o.OrderDetailId == orderd.Id).Include(p => p.Sauce).ToList();
                        }
                    }
                }
            }

            return View(viewModel);
        }
    }
}