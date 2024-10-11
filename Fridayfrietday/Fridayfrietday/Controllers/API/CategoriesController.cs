using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fridayfrietday;
using Fridayfrietday.Models;

namespace Fridayfrietday.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public CategoriesController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle categorieën op.
        /// </summary>
        /// <returns>Een lijst van alle categorieën</returns>
        /// <response code="200">Geeft de lijst met categorieën terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifieke categorie op.
        /// </summary>
        /// <param name="id">Het ID van de categorie</param>
        /// <returns>De categorie met het opgegeven ID</returns>
        /// <response code="200">Geeft de categorie terug</response>
        /// <response code="404">Als de categorie niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        /// <summary>
        /// Update een specifieke categorie.
        /// </summary>
        /// <param name="id">Het ID van de categorie die je wilt bijwerken</param>
        /// <param name="category">De bijgewerkte gegevens van de categorie</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als de categorie met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Voegt een nieuwe categorie toe.
        /// </summary>
        /// <param name="category">De gegevens van de nieuwe categorie</param>
        /// <returns>De nieuw aangemaakte categorie</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /Categories
        ///     {
        ///        "id": 1,
        ///        "name": "Snacks"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft de nieuw aangemaakte categorie terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        /// <summary>
        /// Verwijdert een specifieke categorie.
        /// </summary>
        /// <param name="id">Het ID van de categorie die moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als de categorie niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een categorie bestaat.
        /// </summary>
        /// <param name="id">Het ID van de categorie</param>
        /// <returns>True als de categorie bestaat, anders false</returns>
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
