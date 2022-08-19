using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirLines.Infrastructure.Data.Services;

namespace Airlines.Apis.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagersController : ControllerBase
    {
        private readonly IPassagerService PassagerService;

        //private readonly AirlinesContext _context;

        public PassagersController(IPassagerService PassagerService)//AirlinesContext context)
        {
            this.PassagerService = PassagerService;
            //_context = context;
        }

        // GET: api/Passagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirLines.Core.Resources.PassagerResponse>>> GetPassagers()
        {
            var results = await this.PassagerService.Get();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Passagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirLines.Core.Resources.PassagerResponse>> GetPassager(int id)
        {

            var results = await this.PassagerService.GetById(id);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Passagers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassager(int id, AirLines.Core.Resources.PassagerRequest Passager)
        {
            if (id != Passager.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.PassagerService.Put(id, Passager);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PassagerExists(id))
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

        // POST: api/Passagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPassager(AirLines.Core.Resources.PassagerRequest Passager)
        {
            if (Passager == null)
            {
                return Problem("the Passager  is null.");
            }

            try
            {
                var result = await this.PassagerService.Add(Passager);
                if (!result)
                {
                    return NotFound();
                }

                return CreatedAtAction("GetPassager", new { id = Passager.Id }, Passager);
            }
            catch (Exception)
            {
                return BadRequest("Error while try to inserting row.");
            }
        }

        // DELETE: api/Passagers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassager(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.PassagerService.Remove(id);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                if (!await PassagerExists(id))
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

        private async Task<bool> PassagerExists(int id)
        {
            return await this.PassagerService.Exists(id);
        }
    }
}

