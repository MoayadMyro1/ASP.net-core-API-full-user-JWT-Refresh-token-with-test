using DriverApi.Data;
using DriverApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriverApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriverLocationsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DriverLocationsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DriverLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverLocation>>> GetDriverLocations()
        {
            return await _context.DriverLocations.ToListAsync();
        }

        // GET: api/DriverLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverLocation>> GetDriverLocation(int id)
        {
            var driverLocation = await _context.DriverLocations.FindAsync(id);

            if (driverLocation == null)
            {
                return NotFound();
            }

            return driverLocation;
        }

        // PUT: api/DriverLocations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverLocation(int id, DriverLocation driverLocation)
        {
            if (id != driverLocation.Locid)
            {
                return BadRequest();
            }

            _context.Entry(driverLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverLocationExists(id))
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

        // POST: api/DriverLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverLocation>> PostDriverLocation(DriverLocation driverLocation)
        {
            _context.DriverLocations.Add(driverLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverLocation", new { id = driverLocation.Locid }, driverLocation);
        }

        // DELETE: api/DriverLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverLocation(int id)
        {
            var driverLocation = await _context.DriverLocations.FindAsync(id);
            if (driverLocation == null)
            {
                return NotFound();
            }

            _context.DriverLocations.Remove(driverLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverLocationExists(int id)
        {
            return _context.DriverLocations.Any(e => e.Locid == id);
        }
    }
}
