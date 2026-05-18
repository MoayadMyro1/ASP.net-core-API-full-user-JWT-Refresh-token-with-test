using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DriverApi.Data;
using DriverApi.Models;

namespace DriverApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriverSubscriptionsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DriverSubscriptionsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DriverSubscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverSubscription>>> GetDriverSubscriptions()
        {
            return await _context.DriverSubscriptions.ToListAsync();
        }

        // GET: api/DriverSubscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverSubscription>> GetDriverSubscription(int id)
        {
            var driverSubscription = await _context.DriverSubscriptions.FindAsync(id);

            if (driverSubscription == null)
            {
                return NotFound();
            }

            return driverSubscription;
        }

        // PUT: api/DriverSubscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverSubscription(int id, DriverSubscription driverSubscription)
        {
            if (id != driverSubscription.Id)
            {
                return BadRequest();
            }

            _context.Entry(driverSubscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverSubscriptionExists(id))
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

        // POST: api/DriverSubscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverSubscription>> PostDriverSubscription(DriverSubscription driverSubscription)
        {
            _context.DriverSubscriptions.Add(driverSubscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverSubscription", new { id = driverSubscription.Id }, driverSubscription);
        }

        // DELETE: api/DriverSubscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverSubscription(int id)
        {
            var driverSubscription = await _context.DriverSubscriptions.FindAsync(id);
            if (driverSubscription == null)
            {
                return NotFound();
            }

            _context.DriverSubscriptions.Remove(driverSubscription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverSubscriptionExists(int id)
        {
            return _context.DriverSubscriptions.Any(e => e.Id == id);
        }
    }
}
