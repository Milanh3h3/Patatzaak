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
    public class ProductsController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductsController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle producten op, inclusief de bijbehorende categorieën.
        /// </summary>
        /// <returns>Een lijst met alle beschikbare producten en hun categorieën</returns>
        /// <response code="200">Geeft de lijst met producten terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        /// <summary>
        /// Haalt een specifiek product op
        /// </summary>
        /// <param name="id">Het ID van het product</param>
        /// <returns>Het product met het opgegeven ID</returns>
        /// <response code="200">Geeft het product terug</response>
        /// <response code="404">Als het product niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /// <summary>
        /// Update een specifiek product
        /// </summary>
        /// <param name="id">Het ID van het product dat je wilt bijwerken</param>
        /// <param name="product">De geüpdatete productgegevens</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als het product met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        /// Voegt een nieuw product toe
        /// </summary>
        /// <param name="product">De productgegevens die moeten worden toegevoegd</param>
        /// <returns>Het toegevoegde product</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /Products
        ///     {
        ///        "id": 1,
        ///        "name": "Frietje met",
        ///        "price": 2.50,
        ///        "categoryId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft het nieuw aangemaakte product terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Verwijdert een specifiek product
        /// </summary>
        /// <param name="id">Het ID van het product dat moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als het product niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een product bestaat
        /// </summary>
        /// <param name="id">Het ID van het product</param>
        /// <returns>True als het product bestaat, anders false</returns>
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}
