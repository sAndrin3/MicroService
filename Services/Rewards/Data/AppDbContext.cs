using Microsoft.EntityFrameworkCore;
using Rewards.Models;

namespace Rewards.Data
{
    public class AppDBContext:DbContext
    {

        public AppDBContext( DbContextOptions<AppDBContext> options):base(options)
        {
            
        }

        public DbSet<JituRewards> Rewards { get; set; } 
    }
}