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
    public class ReviewsController : ControllerBase
    {
        private readonly DBContext _context;

        public ReviewsController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle reviews op
        /// </summary>
        /// <returns>Een lijst met alle beschikbare reviews</returns>
        /// <response code="200">Geeft de lijst met reviews terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifieke review op
        /// </summary>
        /// <param name="id">Het ID van de review</param>
        /// <returns>De review met het opgegeven ID</returns>
        /// <response code="200">Geeft de review terug</response>
        /// <response code="404">Als de review niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        /// <summary>
        /// Update een specifieke review
        /// </summary>
        /// <param name="id">Het ID van de review die je wilt bijwerken</param>
        /// <param name="review">De geüpdatete reviewgegevens</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als de review met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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
        /// Voegt een nieuwe review toe
        /// </summary>
        /// <param name="review">De reviewgegevens die moeten worden toegevoegd</param>
        /// <returns>De toegevoegde review</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /Reviews
        ///     {
        ///        "id": 1,
        ///        "title": "Goede service",
        ///        "rating": 5,
        ///        "comment": "Ik ben zeer tevreden met de friet!"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft de nieuw aangemaakte review terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        /// <summary>
        /// Verwijdert een specifieke review
        /// </summary>
        /// <param name="id">Het ID van de review die moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als de review niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een review bestaat
        /// </summary>
        /// <param name="id">Het ID van de review</param>
        /// <returns>True als de review bestaat, anders false</returns>
        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }

    }
}
