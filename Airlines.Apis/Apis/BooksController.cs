using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirLines.Infrastructure.Data.Services;

namespace Airlines.Apis.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService BookService;

        //private readonly AirlinesContext _context;

        public BooksController(IBookService BookService)//AirlinesContext context)
        {
            this.BookService = BookService;
            //_context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirLines.Core.Resources.BookResponse>>> GetBooks()
        {
            var results = await this.BookService.Get();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirLines.Core.Resources.BookResponse>> GetBook(int id)
        {

            var results = await this.BookService.GetById(id);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/Books/5
        [HttpGet("Includes")]
        public async Task<ActionResult<AirLines.Core.Resources.BookResponse>> GetIncludeBook(int id, string[] includes)
        {

            var results = await this.BookService.GetById(id, includes);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, AirLines.Core.Resources.BookRequest Book)
        {
            if (id != Book.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.BookService.Put(id, Book);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBook(AirLines.Core.Resources.BookRequest Book)
        {
            if (Book == null)
            {
                return Problem("the Book  is null.");
            }

            try
            {
                var result = await this.BookService.Add(Book);
                if (!result)
                {
                    return NotFound();
                }

                return CreatedAtAction("GetBook", new { id = Book.Id }, Book);
            }
            catch (Exception)
            {
                return BadRequest("Error while try to inserting row.");
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.BookService.Remove(id);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                if (!await BookExists(id))
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

        private async Task<bool> BookExists(int id)
        {
            return await this.BookService.Exists(id);
        }
    }
}