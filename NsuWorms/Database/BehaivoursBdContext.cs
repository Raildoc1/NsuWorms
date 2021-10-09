using NsuWorms.World;
using System.Data.Entity;

namespace NsuWorms.Database
{
    public sealed class BehavioursDbContext : DbContext
    {
        public DbSet<Behaviour> Behaviours { get; set; }

        public BehavioursDbContext() : base("localWindowsDatabase")
        {
        
        }
    }
}
