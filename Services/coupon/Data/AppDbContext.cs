using Microsoft.EntityFrameworkCore;
using Coupons.Models;

namespace Coupons.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }


        public DbSet<Coupon> Coupons { get; set; }
    }
}
