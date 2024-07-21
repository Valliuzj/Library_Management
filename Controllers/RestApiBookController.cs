using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Book Management.")]
    public class RestApiBookController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RestApiBookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Books
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get all books.")]
        [SwaggerResponse(200, "Returns list of all books.", typeof(IEnumerable<Book>))]
        [SwaggerResponse(404, "No books found.")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_dbContext.Books == null)
            {
                return NotFound();
            }
            return await _dbContext.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get single book by id.")]
        [SwaggerResponse(200, "Get single book.", typeof(Book))]
        [SwaggerResponse(404, "Book with id not found.")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (_dbContext.Books == null)
            {
                return NotFound();
            }
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // POST: api/Books
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Create new book.")]
        [SwaggerResponse(201, "Book created successfully.", typeof(Book))]
        [SwaggerResponse(400, "The request is malformed.")]
        public async Task<ActionResult<Book>> Create([FromBody, SwaggerRequestBody("The book payload", Required = true)] Book book)
        {
            if (_dbContext.Books == null)
            {
                return Problem("Entity set 'AppDbContext.Books' is null.");
            }
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update single book by id.")]
        [SwaggerResponse(204, "Book updated successfully.")]
        [SwaggerResponse(400, "The request is malformed.")]
        [SwaggerResponse(404, "Book with id not found.")]
        public async Task<IActionResult> Edit(int id, [FromBody] Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }
            if (book.Title == null)
            {
                return BadRequest();
            }
            try
            {
                var current = await _dbContext.Books.FindAsync(id);
                if (current == null)
                {
                    return NotFound();
                }
                _dbContext.ChangeTracker.Clear();
                _dbContext.Books.Update(book);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Books.Any(e => e.BookId == id))
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

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete book by id.")]
        [SwaggerResponse(204, "Book deleted successfully.")]
        [SwaggerResponse(404, "Book with id not found.")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_dbContext.Books == null)
            {
                return NotFound();
            }
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
