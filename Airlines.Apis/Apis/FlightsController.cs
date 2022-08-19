using AirLines.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Airlines.Apis.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService FlightService;

        //private readonly AirlinesContext _context;

        public FlightsController(IFlightService FlightService)//AirlinesContext context)
        {
            this.FlightService = FlightService;
            //_context = context;
        }

        
        // GET: api/Flights
        [HttpGet("GetByFilter")]
        public async Task<ActionResult<IEnumerable<AirLines.Core.Resources.FlightResponse>>> GetByFilterFlights(int? id, DateTime? from, DateTime? to, DateTime? depart, DateTime? arrival)
        {
            var results = await this.FlightService.Get(id, from, to, depart, arrival);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }
        


        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirLines.Core.Resources.FlightResponse>>> GetFlights()
        {
            var results = await this.FlightService.Get();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirLines.Core.Resources.FlightResponse>> GetFlight(int id)
        {

            var results = await this.FlightService.GetById(id);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, AirLines.Core.Resources.FlightRequest Flight)
        {
            if (id != Flight.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.FlightService.Put(id, Flight);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FlightExists(id))
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

        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatusFlight(int id, int status)
        {           

            try
            {
                var result = await this.FlightService.ChangeStatus(id, status);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostFlight(AirLines.Core.Resources.FlightRequest Flight)
        {
            if (Flight == null)
            {
                return Problem("the Flight  is null.");
            }

            try
            {
                var result = await this.FlightService.Add(Flight);
                if (!result)
                {
                    return NotFound();
                }

                return CreatedAtAction("GetFlight", new { id = Flight.Id }, Flight);
            }
            catch (Exception)
            {
                return BadRequest("Error while try to inserting row.");
            }
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.FlightService.Remove(id);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                if (!await FlightExists(id))
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

        private async Task<bool> FlightExists(int id)
        {
            return await this.FlightService.Exists(id);
        }
    }
}