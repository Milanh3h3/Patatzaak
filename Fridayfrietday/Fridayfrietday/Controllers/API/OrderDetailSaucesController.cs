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
    public class OrderDetailSaucesController : ControllerBase
    {
        private readonly DBContext _context;

        public OrderDetailSaucesController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle orderdetailsauzen op.
        /// </summary>
        /// <returns>Een lijst van alle orderdetailsauzen</returns>
        /// <response code="200">Geeft de lijst met orderdetailsauzen terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailSauce>>> GetOrderDetailSauces()
        {
            return await _context.OrderDetailSauces.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifieke orderdetailsaus op.
        /// </summary>
        /// <param name="id">Het ID van de orderdetailsaus</param>
        /// <returns>De orderdetailsaus met het opgegeven ID</returns>
        /// <response code="200">Geeft de orderdetailsaus terug</response>
        /// <response code="404">Als de orderdetailsaus niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailSauce>> GetOrderDetailSauce(int id)
        {
            var orderDetailSauce = await _context.OrderDetailSauces.FindAsync(id);

            if (orderDetailSauce == null)
            {
                return NotFound();
            }

            return orderDetailSauce;
        }

        /// <summary>
        /// Update een specifieke orderdetailsaus.
        /// </summary>
        /// <param name="id">Het ID van de orderdetailsaus die je wilt bijwerken</param>
        /// <param name="orderDetailSauce">De bijgewerkte gegevens van de orderdetailsaus</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als de orderdetailsaus met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetailSauce(int id, OrderDetailSauce orderDetailSauce)
        {
            if (id != orderDetailSauce.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDetailSauce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailSauceExists(id))
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
        /// Voegt een nieuwe orderdetailsaus toe.
        /// </summary>
        /// <param name="orderDetailSauce">De gegevens van de nieuwe orderdetailsaus</param>
        /// <returns>De nieuw aangemaakte orderdetailsaus</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /OrderDetailSauces
        ///     {
        ///        "id": 1,
        ///        "orderDetailId": 1,
        ///        "sauceId": 2
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft de nieuw aangemaakte orderdetailsaus terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<OrderDetailSauce>> PostOrderDetailSauce(OrderDetailSauce orderDetailSauce)
        {
            _context.OrderDetailSauces.Add(orderDetailSauce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetailSauce", new { id = orderDetailSauce.Id }, orderDetailSauce);
        }

        /// <summary>
        /// Verwijdert een specifieke orderdetailsaus.
        /// </summary>
        /// <param name="id">Het ID van de orderdetailsaus die moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als de orderdetailsaus niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailSauce(int id)
        {
            var orderDetailSauce = await _context.OrderDetailSauces.FindAsync(id);
            if (orderDetailSauce == null)
            {
                return NotFound();
            }

            _context.OrderDetailSauces.Remove(orderDetailSauce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een orderdetailsaus bestaat.
        /// </summary>
        /// <param name="id">Het ID van de orderdetailsaus</param>
        /// <returns>True als de orderdetailsaus bestaat, anders false</returns>
        private bool OrderDetailSauceExists(int id)
        {
            return _context.OrderDetailSauces.Any(e => e.Id == id);
        }
    }
}
