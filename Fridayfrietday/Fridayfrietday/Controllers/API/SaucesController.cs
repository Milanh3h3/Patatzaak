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
    public class SaucesController : ControllerBase
    {
        private readonly DBContext _context;

        public SaucesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Sauces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sauce>>> GetSauces()
        {
            return await _context.Sauces.ToListAsync();
        }

        // GET: api/Sauces/5
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

        // PUT: api/Sauces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Sauces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sauce>> PostSauce(Sauce sauce)
        {
            _context.Sauces.Add(sauce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSauce", new { id = sauce.Id }, sauce);
        }

        // DELETE: api/Sauces/5
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

        private bool SauceExists(int id)
        {
            return _context.Sauces.Any(e => e.Id == id);
        }
    }
}
