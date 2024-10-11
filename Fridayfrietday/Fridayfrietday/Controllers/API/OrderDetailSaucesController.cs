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
    public class OrderDetailSaucesController : ControllerBase
    {
        private readonly DBContext _context;

        public OrderDetailSaucesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetailSauces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailSauce>>> GetOrderDetailSauces()
        {
            return await _context.OrderDetailSauces.ToListAsync();
        }

        // GET: api/OrderDetailSauces/5
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

        // PUT: api/OrderDetailSauces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/OrderDetailSauces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetailSauce>> PostOrderDetailSauce(OrderDetailSauce orderDetailSauce)
        {
            _context.OrderDetailSauces.Add(orderDetailSauce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetailSauce", new { id = orderDetailSauce.Id }, orderDetailSauce);
        }

        // DELETE: api/OrderDetailSauces/5
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

        private bool OrderDetailSauceExists(int id)
        {
            return _context.OrderDetailSauces.Any(e => e.Id == id);
        }
    }
}
