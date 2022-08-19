using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirLines.Infrastructure.Data.Services;

namespace Airlines.Apis.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirPortsController : ControllerBase
    {
        private readonly IAirPortService AirPortService;

        //private readonly AirlinesContext _context;

        public AirPortsController(IAirPortService airPortService)//AirlinesContext context)
        {
            this.AirPortService = airPortService;
            //_context = context;
        }

        // GET: api/AirPorts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirLines.Core.Resources.AirPortResponse>>> GetAirPorts()
        {           
            var results=  await this.AirPortService.Get();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/AirPorts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirLines.Core.Resources.AirPortResponse>> GetAirPort(int id)
        {

            var results = await this.AirPortService.GetById(id);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/AirPorts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirPort(int id, AirLines.Core.Resources.AirPortRequest airPort)
        {
            if (id != airPort.Id)
            {
                return BadRequest();
            }
            
            try
            {
                var result = await this.AirPortService.Put(id, airPort);
                if(!result)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AirPortExists(id))
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
        public async Task<IActionResult> PostAirPort(AirLines.Core.Resources.AirPortRequest airPort)
        {
          if (airPort == null)
          {
              return Problem("the AirPort  is null.");
          }

            try
            {
                var result = await this.AirPortService.Add(airPort);
                if (!result)
                {
                    return NotFound();
                }

                return CreatedAtAction("GetAirPort", new { id = airPort.Id }, airPort);
            }
            catch (Exception)
            {
                return BadRequest("Error while try to inserting row.");
            }            
        }

        // DELETE: api/AirPorts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirPort(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.AirPortService.Remove(id);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                if (!await AirPortExists(id))
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

        private async Task<bool> AirPortExists(int id)
        {
            return await this.AirPortService.Exists(id);
        }
    }
}
