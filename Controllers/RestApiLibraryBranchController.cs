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
    [SwaggerTag("Library Branch Management.")]
    public class RestApiLibraryBranchController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RestApiLibraryBranchController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/LibraryBranches
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get all library branches.")]
        [SwaggerResponse(200, "Returns list of all library branches.", typeof(IEnumerable<LibraryBranch>))]
        [SwaggerResponse(404, "No library branches found.")]
        public async Task<ActionResult<IEnumerable<LibraryBranch>>> GetLibraryBranches()
        {
            if (_dbContext.LibraryBranches == null)
            {
                return NotFound();
            }
            return await _dbContext.LibraryBranches.ToListAsync();
        }

        // GET: api/LibraryBranches/5
        /// <summary>
        /// Get a single library branch by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the library branch to retrieve.</param>
        /// <returns>An ActionResult containing the library branch if found, or a NotFound result if not found or if the library branches data is unavailable.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get single library branch by id.")]
        [SwaggerResponse(200, "Get single library branch.", typeof(LibraryBranch))]
        [SwaggerResponse(404, "Library branch with id not found.")]
        public async Task<ActionResult<LibraryBranch>> GetLibraryBranch(int id)
        {
            if (_dbContext.LibraryBranches == null)
            {
                return NotFound();
            }
            var libraryBranch = await _dbContext.LibraryBranches.FindAsync(id);
            if (libraryBranch == null)
            {
                return NotFound();
            }
            return libraryBranch;
        }

        // POST: api/LibraryBranches
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Create new library branch.")]
        [SwaggerResponse(201, "Library branch created successfully.", typeof(LibraryBranch))]
        [SwaggerResponse(400, "The request is malformed.")]
        public async Task<ActionResult<LibraryBranch>> Create([FromBody, SwaggerRequestBody("The library branch payload", Required = true)] LibraryBranch libraryBranch)
        {
            if (_dbContext.LibraryBranches == null)
            {
                return Problem("Entity set 'AppDbContext.LibraryBranches' is null.");
            }
            if (ModelState.IsValid)
            {
                _dbContext.LibraryBranches.Add(libraryBranch);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction("GetLibraryBranch", new { id = libraryBranch.LibraryBranchId }, libraryBranch);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/LibraryBranches/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update single library branch by id.")]
        [SwaggerResponse(204, "Library branch updated successfully.")]
        [SwaggerResponse(400, "The request is malformed.")]
        [SwaggerResponse(404, "Library branch with id not found.")]
        public async Task<IActionResult> Edit(int id, [FromBody] LibraryBranch libraryBranch)
        {
            if (id != libraryBranch.LibraryBranchId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var current = await _dbContext.LibraryBranches.FindAsync(id);
                    if (current == null)
                    {
                        return NotFound();
                    }
                    _dbContext.ChangeTracker.Clear();
                    _dbContext.LibraryBranches.Update(libraryBranch);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.LibraryBranches.Any(e => e.LibraryBranchId == id))
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
            return BadRequest(ModelState);
        }

        // DELETE: api/LibraryBranches/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete library branch by id.")]
        [SwaggerResponse(204, "Library branch deleted successfully.")]
        [SwaggerResponse(404, "Library branch with id not found.")]
        public async Task<IActionResult> DeleteLibraryBranch(int id)
        {
            if (_dbContext.LibraryBranches == null)
            {
                return NotFound();
            }
            var branch = await _dbContext.LibraryBranches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            _dbContext.LibraryBranches.Remove(branch);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
