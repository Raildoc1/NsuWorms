using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NsuWormsWorldBehaviourGenerator.Core;
using System;

namespace NsuWormsWorldBehaviourGenerator.Database
{
    public class BehavioursDbContext : DbContext
    {
        public DbSet<Behaviour> Behaviours { get; set; }

        public BehavioursDbContext(DbContextOptions<BehavioursDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.CreateTables();
            }
            catch (Exception) { }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Behaviour>()
                .ToTable("Behaviours")
                .HasKey(s => s.Id);
        }
    }
}
