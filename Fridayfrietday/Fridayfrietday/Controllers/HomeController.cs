using Fridayfrietday.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Fridayfrietday.ViewModels;

namespace Fridayfrietday.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBContext _context;

        public HomeController(ILogger<HomeController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
        // Create a ProductReviewViewModel instance
        var viewModel = new ProductReviewViewModel
        {
            Products = _context.Products.Include(p => p.Category).ToList(),
            Reviews = _context.Reviews.ToList()
        };

        // Pass the viewModel to the view
        return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Milan()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
