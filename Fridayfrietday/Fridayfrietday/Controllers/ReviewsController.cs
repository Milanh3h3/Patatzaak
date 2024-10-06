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
using System.Diagnostics;

namespace Fridayfrietday.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly DBContext _context;

        public ReviewsController(DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews.ToListAsync(); 
            var viewModel = new ReviewViewModel
            {
                Reviews = reviews,
                NewReview = new Review()  
            };
            return View(viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReviewViewModel viewModel)
        {
            // Verwijder de 'Reviews' uit ModelState om validatie te laten werken want hij is stom
            ModelState.Remove("Reviews");
            if (ModelState.IsValid)
            {
                viewModel.NewReview.Date = DateTime.Now;
                _context.Add(viewModel.NewReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Reviews = _context.Reviews.ToList();
            return View(viewModel);
        }
    }
}

