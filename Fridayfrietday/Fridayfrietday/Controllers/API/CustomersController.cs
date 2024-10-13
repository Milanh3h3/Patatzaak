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
    public class CustomersController : ControllerBase
    {
        private readonly DBContext _context;

        public CustomersController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle klanten op.
        /// </summary>
        /// <returns>Een lijst van alle klanten</returns>
        /// <response code="200">Geeft de lijst met klanten terug</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Haalt een specifieke klant op.
        /// </summary>
        /// <param name="id">Het ID van de klant</param>
        /// <returns>De klant met het opgegeven ID</returns>
        /// <response code="200">Geeft de klant terug</response>
        /// <response code="404">Als de klant niet wordt gevonden</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Update een specifieke klant.
        /// </summary>
        /// <param name="id">Het ID van de klant die je wilt bijwerken</param>
        /// <param name="customer">De bijgewerkte gegevens van de klant</param>
        /// <returns>Geen inhoud als de update succesvol is</returns>
        /// <response code="204">Update was succesvol</response>
        /// <response code="400">Als de verstrekte gegevens onjuist zijn</response>
        /// <response code="404">Als de klant met het opgegeven ID niet bestaat</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
        /// Voegt een nieuwe klant toe.
        /// </summary>
        /// <param name="customer">De gegevens van de nieuwe klant</param>
        /// <returns>De nieuw aangemaakte klant</returns>
        /// <remarks>
        /// Voorbeeld request:
        ///
        ///     POST /Customers
        ///     {
        ///        "id": 1,
        ///        "name": "Jan de Vries",
        ///        "email": "jan@example.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Geeft de nieuw aangemaakte klant terug</response>
        /// <response code="400">Als de verstrekte gegevens ongeldig zijn</response>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Verwijdert een specifieke klant.
        /// </summary>
        /// <param name="id">Het ID van de klant die moet worden verwijderd</param>
        /// <returns>Geen inhoud als de verwijdering succesvol is</returns>
        /// <response code="204">Verwijdering was succesvol</response>
        /// <response code="404">Als de klant niet wordt gevonden</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Controleert of een klant bestaat.
        /// </summary>
        /// <param name="id">Het ID van de klant</param>
        /// <returns>True als de klant bestaat, anders false</returns>
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
