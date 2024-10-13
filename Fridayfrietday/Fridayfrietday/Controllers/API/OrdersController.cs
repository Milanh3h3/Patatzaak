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
    public class OrdersController : ControllerBase
    {
        private readonly DBContext _context;

        public OrdersController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle bestellingen op.
        /// </summary>
        /// <returns>Een lijst met alle beschikbare bestellingen</returns>
        /// <response code="200">Geeft de lijst met bestellingen terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifieke bestelling op.
        /// </summary>
        /// <param name="id">Het ID van de bestelling</param>
        /// <returns>De bestelling met het opgegeven ID</returns>
        /// <response code="200">Geeft de bestelling terug</response>
        /// <response code="404">Als de bestelling niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        /// <summary>
        /// Update een specifieke bestelling.
        /// </summary>
        /// <param name="id">Het ID van de bestelling die je wilt bijwerken</param>
        /// <param name="order">De bijgewerkte gegevens van de bestelling</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als de bestelling met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        /// Voegt een nieuwe bestelling toe.
        /// </summary>
        /// <param name="order">De gegevens van de nieuwe bestelling</param>
        /// <returns>De nieuw aangemaakte bestelling</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /Orders
        ///     {
        ///        "id": 1,
        ///        "customerId": 1,
        ///        "totalPrice": 15.00,
        ///        "orderDate": "2024-10-11T12:34:56",
        ///        "status": "Bestelling aangekomen"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft de nieuw aangemaakte bestelling terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        /// <summary>
        /// Verwijdert een specifieke bestelling.
        /// </summary>
        /// <param name="id">Het ID van de bestelling die moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als de bestelling niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een bestelling bestaat.
        /// </summary>
        /// <param name="id">Het ID van de bestelling</param>
        /// <returns>True als de bestelling bestaat, anders false</returns>
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
