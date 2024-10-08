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
    }
}