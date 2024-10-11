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
    public class SaucesController : ControllerBase
    {
        private readonly DBContext _context;

        public SaucesController(DBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Haalt alle sauzen op
        /// </summary>
        /// <returns>Een lijst met alle beschikbare sauzen</returns>
        /// <response code="200">Geeft de lijst met sauzen terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sauce>>> GetSauces()
        {
            return await _context.Sauces.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifieke saus op
        /// </summary>
        /// <param name="id">Het ID van de saus</param>
        /// <returns>De saus met het opgegeven ID</returns>
        /// <response code="200">Geeft de saus terug</response>
        /// <response code="404">Als de saus niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Sauce>> GetSauce(int id)
        {
            var sauce = await _context.Sauces.FindAsync(id);

            if (sauce == null)
            {
                return NotFound();
            }

            return sauce;
        }

        /// <summary>
        /// Update een specifieke saus
        /// </summary>
        /// <param name="id">Het ID van de saus die je wilt bijwerken</param>
        /// <param name="sauce">De geüpdatete sausgegevens</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als de saus met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSauce(int id, Sauce sauce)
        {
            if (id != sauce.Id)
            {
                return BadRequest();
            }

            _context.Entry(sauce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SauceExists(id))
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
        /// Voegt een nieuwe saus toe
        /// </summary>
        /// <param name="sauce">De sausgegevens die moeten worden toegevoegd</param>
        /// <returns>De toegevoegde saus</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /Sauces
        ///     {
        ///        "id": 1,
        ///        "name": "Ketchup",
        ///        "price": 0.50
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft de nieuw aangemaakte saus terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<Sauce>> PostSauce(Sauce sauce)
        {
            _context.Sauces.Add(sauce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSauce", new { id = sauce.Id }, sauce);
        }

        /// <summary>
        /// Verwijdert een specifieke saus
        /// </summary>
        /// <param name="id">Het ID van de saus die moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als de saus niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSauce(int id)
        {
            var sauce = await _context.Sauces.FindAsync(id);
            if (sauce == null)
            {
                return NotFound();
            }

            _context.Sauces.Remove(sauce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een saus bestaat
        /// </summary>
        /// <param name="id">Het ID van de saus</param>
        /// <returns>True als de saus bestaat, anders false</returns>
        private bool SauceExists(int id)
        {
            return _context.Sauces.Any(e => e.Id == id);
        }
    }
}
