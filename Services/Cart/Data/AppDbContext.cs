using Microsoft.EntityFrameworkCore;
using Cart.Models;

namespace Cart.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<CartDetails> CartDetails { get; set; }

        public DbSet<CartHeader> CartHeaders { get; set; }
       
    }
}