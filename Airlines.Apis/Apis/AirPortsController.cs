using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Airlines.Apis.Data;
using Airlines.Apis.Models;

namespace Airlines.Apis.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirPortsController : ControllerBase
    {
        private readonly AirlinesContext _context;

        public AirPortsController(AirlinesContext context)
        {
            _context = context;
        }

        // GET: api/AirPorts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirPort>>> GetAirPorts()
        {
          if (_context.AirPorts == null)
          {
              return NotFound();
          }
            return await _context.AirPorts.ToListAsync();
        }

        // GET: api/AirPorts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirPort>> GetAirPort(int id)
        {
          if (_context.AirPorts == null)
          {
              return NotFound();
          }
            var airPort = await _context.AirPorts.FindAsync(id);

            if (airPort == null)
            {
                return NotFound();
            }

            return airPort;
        }

        // PUT: api/AirPorts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirPort(int id, AirPort airPort)
        {
            if (id != airPort.Id)
            {
                return BadRequest();
            }

            _context.Entry(airPort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirPortExists(id))
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

        // POST: api/AirPorts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirPort>> PostAirPort(AirPort airPort)
        {
          if (_context.AirPorts == null)
          {
              return Problem("Entity set 'AirlinesContext.AirPorts'  is null.");
          }
            _context.AirPorts.Add(airPort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirPort", new { id = airPort.Id }, airPort);
        }

        // DELETE: api/AirPorts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirPort(int id)
        {
            if (_context.AirPorts == null)
            {
                return NotFound();
            }
            var airPort = await _context.AirPorts.FindAsync(id);
            if (airPort == null)
            {
                return NotFound();
            }

            _context.AirPorts.Remove(airPort);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirPortExists(int id)
        {
            return (_context.AirPorts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
