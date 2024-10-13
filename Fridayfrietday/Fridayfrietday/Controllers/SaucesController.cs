using Microsoft.AspNetCore.Mvc;
using Fridayfrietday.Models;
using System.Linq;
namespace Fridayfrietday.Controllers;
public class SaucesController : Controller
{
    private readonly DBContext _context;

    public SaucesController(DBContext context)
    {
        _context = context;
    }

    // GET: Sauces
    public IActionResult Index()
    {
        var sauces = _context.Sauces.ToList();
        return View(sauces);
    }

    // GET: Sauces/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Sauces/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Sauce sauce)
    {
        if (ModelState.IsValid)
        {
            _context.Sauces.Add(sauce);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(sauce);
    }

    // GET: Sauces/Edit/5
    public IActionResult Edit(int id)
    {
        var sauce = _context.Sauces.Find(id);
        if (sauce == null)
        {
            return NotFound();
        }
        return View(sauce);
    }

    // POST: Sauces/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Sauce sauce)
    {
        if (id != sauce.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(sauce);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(sauce);
    }

    // GET: Sauces/Delete/5
    public IActionResult Delete(int id)
    {
        var sauce = _context.Sauces.Find(id);
        if (sauce == null)
        {
            return NotFound();
        }
        return View(sauce);
    }

    // POST: Sauces/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var sauce = _context.Sauces.Find(id);
        if (sauce == null)
        {
            return NotFound();
        }

        _context.Sauces.Remove(sauce);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
