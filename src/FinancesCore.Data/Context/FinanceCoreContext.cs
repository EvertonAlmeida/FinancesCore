using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinancesCore.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesCore.Data.Context
{
    public class FinanceCoreContext : DbContext
    {
        public FinanceCoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transaction> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceCoreContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null)) {
                
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                
                if (entry.State == EntityState.Modified)
                    entry.Property("CreatedAt").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedAt") != null)) {
                
                if (entry.State == EntityState.Added)
                    entry.Property("UpdatedAt").IsModified = false;

                if (entry.State == EntityState.Modified)
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}