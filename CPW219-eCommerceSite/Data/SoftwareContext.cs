using CPW219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Data
{
    public class SoftwareContext : DbContext    
    {
        public SoftwareContext(DbContextOptions<SoftwareContext> options ) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
