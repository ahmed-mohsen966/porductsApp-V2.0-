using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using porductsApp.Models;

namespace porductsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<ApplicationUser> User { get; set; }
    }
}