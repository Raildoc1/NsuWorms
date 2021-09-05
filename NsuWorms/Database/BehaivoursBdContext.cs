using Microsoft.EntityFrameworkCore;
using NsuWorms.World;

namespace NsuWorms.Database
{
    public class BehavioursDbContext : DbContext
    {
        public BehavioursDbContext(DbContextOptions<BehavioursDbContext> options) : base(options)
        {

        }

        public DbSet<Behaviour> Behaviours { get; set; }
    }
}
