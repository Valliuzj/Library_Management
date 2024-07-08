using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibraryManagement
{
    public class AppDbContext : IdentityDbContext
    {

        public DbSet<Models.Author> Authors { get; set; }
        public DbSet<Models.LibraryBranch> LibraryBranches { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Book> Books { get; set; }
        public readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}