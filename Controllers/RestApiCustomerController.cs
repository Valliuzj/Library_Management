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
    [SwaggerTag("Customer Management.")]
    public class RestApiCustomerController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RestApiCustomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Customers
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get all customers.")]
        [SwaggerResponse(200, "Returns list of all customers.", typeof(IEnumerable<Customer>))]
        [SwaggerResponse(404, "No customers found.")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_dbContext.Customers == null)
            {
                return NotFound();
            }
            return await _dbContext.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get single customer by id.")]
        [SwaggerResponse(200, "Get single customer.", typeof(Customer))]
        [SwaggerResponse(404, "Customer with id not found.")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_dbContext.Customers == null)
            {
                return NotFound();
            }
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // POST: api/Customers
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Create new customer.")]
        [SwaggerResponse(201, "Customer created successfully.", typeof(Customer))]
        [SwaggerResponse(400, "The request is malformed.")]
        public async Task<ActionResult<Customer>> Create([FromBody, SwaggerRequestBody("The customer payload", Required = true)] Customer customer)
        {
            if (_dbContext.Customers == null)
            {
                return Problem("Entity set 'AppDbContext.Customers' is null.");
            }
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update single customer by id.")]
        [SwaggerResponse(204, "Customer updated successfully.")]
        [SwaggerResponse(400, "The request is malformed.")]
        [SwaggerResponse(404, "Customer with id not found.")]
        public async Task<IActionResult> Edit(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            if (customer.Name == null)
            {
                return BadRequest();
            }
            try
            {
                var current = await _dbContext.Customers.FindAsync(id);
                if (current == null)
                {
                    return NotFound();
                }
                _dbContext.ChangeTracker.Clear();
                _dbContext.Customers.Update(customer);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Customers.Any(e => e.CustomerId == id))
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

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete customer by id.")]
        [SwaggerResponse(204, "Customer deleted successfully.")]
        [SwaggerResponse(404, "Customer with id not found.")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_dbContext.Customers == null)
            {
                return NotFound();
            }
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
