using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Author Management.")]
    public class  RestApiAuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public  RestApiAuthorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get all authors.")]
        [SwaggerResponse(200, "Returns list of all authors.", typeof(IEnumerable<Author>))]
        [SwaggerResponse(404, "No author found.")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get single author by id.")]
        [SwaggerResponse(200, "Get single author.", typeof(Author))]
        [SwaggerResponse(404, "Author with id not found.")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var Author = await _context.Authors.FindAsync(id);
            if (Author == null)
            {
                return NotFound();
            }
            return Author;
        }
        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update single author by id.")]
        [SwaggerResponse(204, "Author updated sucessfully.")]
        [SwaggerResponse(400, "The request is malformed.")]
        [SwaggerResponse(404, "Author with id not found.")]
        public IActionResult Edit(int id, Author author)
        {
            if(id != author.AuthorId) {
                return BadRequest();
            }
            if(author.Name == null) { 
                return BadRequest();
            }
            try {
                var current = _context.Authors.Find(id);
                if(current == null) {
                    return NotFound();
                }
                _context.ChangeTracker.Clear();
                _context.Authors.Update(author);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if(_context.Authors.Any(e => e.AuthorId == id)) {
                    return NotFound();
                }
            }
            return NoContent();
        }

        // POST: api/Authors
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Create new author.")]
        [SwaggerResponse(201, "Author created successfully.", typeof(Author))]
        [SwaggerResponse(400, "The request is malformed.")]
        public async Task<ActionResult<Author>> Create(
            [FromBody, SwaggerRequestBody("The aothor payload", Required = true)] Author author)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'AppDbContext.Authors' is null.");
            }
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes author by id.")]
        [SwaggerResponse(204, "Author deleted successfully.")]
        [SwaggerResponse(404, "Author with id not found.")]
        [SwaggerResponse(400, "Author cannot be deleted because of related books.")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var Author = await _context.Authors.FindAsync(id);
            if (Author == null)
            {
                return NotFound();
            }
            var books = _context.Books.Where(a => a.AuthorId == id).FirstOrDefault();
            if(books != null) {
                return Problem("All related books mush be deleted before deleting the author.");
            }
            _context.Authors.Remove(Author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


