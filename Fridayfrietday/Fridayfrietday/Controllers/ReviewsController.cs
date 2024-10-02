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

        // GET: Display reviews and the form to add a new review
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews.ToListAsync(); // Load the reviews from the database
            var viewModel = new ReviewViewModel
            {
                Reviews = reviews,
                NewReview = new Review()  // Initialize a new Review for the form
            };
            return View(viewModel); // Pass the ViewModel to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReviewViewModel viewModel) // Use ReviewViewModel as parameter
        {
            if (ModelState.IsValid)
            {
                // Add the new review to the database
                _context.Add(viewModel.NewReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, re-fetch the reviews and return the view model
            viewModel.Reviews = await _context.Reviews.ToListAsync();
            return View(viewModel); // Return the same ViewModel
        }
    }
}

