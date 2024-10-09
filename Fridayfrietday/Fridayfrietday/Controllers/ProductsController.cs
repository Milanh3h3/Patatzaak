using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fridayfrietday;
using Fridayfrietday.Models;
using Microsoft.AspNetCore.Authorization;
using Fridayfrietday.ViewModels;
using Microsoft.Extensions.Hosting.Internal;

namespace Fridayfrietday.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DBContext _context;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductsController(DBContext context, ShoppingCartService shoppingCartService, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Products
        public IActionResult Index()
        {
            // Load products and sauces from the database
            var products = _context.Products.Include(p => p.Category).ToList();
            var sauces = _context.Sauces.ToList();

            // Create the view model with products and available sauces
            var viewModel = new ProductSauceViewModel
            {
                Products = products,
                AvailableSauces = sauces
            };

            return View(viewModel);
        }

        // GET: Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList(); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string wwwRootPath = _hostingEnvironment.WebRootPath;
                    string imagePath = Path.Combine(wwwRootPath, "Images");
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
 
                    // Create a unique file name to prevent overwriting existing images
                    string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(imagePath, fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }

                    // Save the relative path to the database
                    product.ImageLink = fileName;
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Overview");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }
        // GET: Products/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList(); // For category dropdown
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        string wwwRootPath = _hostingEnvironment.WebRootPath;
                        string imagePath = Path.Combine(wwwRootPath, "Images");
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                        }

                        // Generate unique file name
                        string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                        string filePath = Path.Combine(imagePath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            product.ImageFile.CopyTo(fileStream);
                        }

                        if (!string.IsNullOrEmpty(product.ImageLink))
                        {
                            string oldImagePath = Path.Combine(wwwRootPath, product.ImageLink);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        product.ImageLink = fileName;
                    }
                    
                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Overview));
            }

            ViewBag.Categories = _context.Categories.ToList(); 
            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        // GET: Products/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            // Optionally delete the associated image
            if (!string.IsNullOrEmpty(product.ImageLink))
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, product.ImageLink.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Overview));
        }

        // GET: Products
        public IActionResult Overview()
        {
            var products = _context.Products.Include(p => p.Category).OrderBy(p => p.CategoryId).ToList();
            return View(products);
        }
    }
}
