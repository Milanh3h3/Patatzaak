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
    public class OrderDetailsController : ControllerBase
    {
        private readonly DBContext _context;

        public OrderDetailsController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle orderdetails op.
        /// </summary>
        /// <returns>Een lijst van alle orderdetails</returns>
        /// <response code="200">Geeft de lijst met orderdetails terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifiek orderdetail op.
        /// </summary>
        /// <param name="id">Het ID van het orderdetail</param>
        /// <returns>Het orderdetail met het opgegeven ID</returns>
        /// <response code="200">Geeft het orderdetail terug</response>
        /// <response code="404">Als het orderdetail niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        /// <summary>
        /// Update een specifiek orderdetail.
        /// </summary>
        /// <param name="id">Het ID van het orderdetail dat je wilt bijwerken</param>
        /// <param name="orderDetail">De bijgewerkte gegevens van het orderdetail</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als het orderdetail met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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
        /// Voegt een nieuw orderdetail toe.
        /// </summary>
        /// <param name="orderDetail">De gegevens van het nieuwe orderdetail</param>
        /// <returns>Het nieuw aangemaakte orderdetail</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /OrderDetails
        ///     {
        ///        "id": 1,
        ///        "orderId": 1,
        ///        "productId": 1,
        ///        "quantity": 2,
        ///        "price": 10.00
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft het nieuw aangemaakte orderdetail terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.Id }, orderDetail);
        }

        /// <summary>
        /// Verwijdert een specifiek orderdetail.
        /// </summary>
        /// <param name="id">Het ID van het orderdetail dat moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als het orderdetail niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een orderdetail bestaat.
        /// </summary>
        /// <param name="id">Het ID van het orderdetail</param>
        /// <returns>True als het orderdetail bestaat, anders false</returns>
        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
    }
}
