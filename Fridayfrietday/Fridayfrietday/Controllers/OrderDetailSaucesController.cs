using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fridayfrietday;
using Fridayfrietday.Models;

namespace Fridayfrietday.Controllers
{
    public class OrderDetailSaucesController : Controller
    {
        private readonly DBContext _context;

        public OrderDetailSaucesController(DBContext context)
        {
            _context = context;
        }

        // GET: OrderDetailSauces
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.OrderDetailSauces.Include(o => o.OrderDetail).Include(o => o.Sauce);
            return View(await dBContext.ToListAsync());
        }

        // GET: OrderDetailSauces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailSauce = await _context.OrderDetailSauces
                .Include(o => o.OrderDetail)
                .Include(o => o.Sauce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetailSauce == null)
            {
                return NotFound();
            }

            return View(orderDetailSauce);
        }

        // GET: OrderDetailSauces/Create
        public IActionResult Create()
        {
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "Id", "Id");
            ViewData["SauceId"] = new SelectList(_context.Sauces, "Id", "Name");
            return View();
        }

        // POST: OrderDetailSauces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDetailId,SauceId")] OrderDetailSauce orderDetailSauce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetailSauce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "Id", "Id", orderDetailSauce.OrderDetailId);
            ViewData["SauceId"] = new SelectList(_context.Sauces, "Id", "Name", orderDetailSauce.SauceId);
            return View(orderDetailSauce);
        }

        // GET: OrderDetailSauces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailSauce = await _context.OrderDetailSauces.FindAsync(id);
            if (orderDetailSauce == null)
            {
                return NotFound();
            }
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "Id", "Id", orderDetailSauce.OrderDetailId);
            ViewData["SauceId"] = new SelectList(_context.Sauces, "Id", "Name", orderDetailSauce.SauceId);
            return View(orderDetailSauce);
        }

        // POST: OrderDetailSauces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDetailId,SauceId")] OrderDetailSauce orderDetailSauce)
        {
            if (id != orderDetailSauce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetailSauce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailSauceExists(orderDetailSauce.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "Id", "Id", orderDetailSauce.OrderDetailId);
            ViewData["SauceId"] = new SelectList(_context.Sauces, "Id", "Name", orderDetailSauce.SauceId);
            return View(orderDetailSauce);
        }

        // GET: OrderDetailSauces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailSauce = await _context.OrderDetailSauces
                .Include(o => o.OrderDetail)
                .Include(o => o.Sauce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetailSauce == null)
            {
                return NotFound();
            }

            return View(orderDetailSauce);
        }

        // POST: OrderDetailSauces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetailSauce = await _context.OrderDetailSauces.FindAsync(id);
            if (orderDetailSauce != null)
            {
                _context.OrderDetailSauces.Remove(orderDetailSauce);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailSauceExists(int id)
        {
            return _context.OrderDetailSauces.Any(e => e.Id == id);
        }
    }
}
