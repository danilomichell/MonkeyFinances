using Microsoft.EntityFrameworkCore;
using MonkeyFinances.Core.Data;
using MonkeyFinances.Financas.Api.Models;

namespace MonkeyFinances.Financas.Api.Data
{
    public sealed class FinancasContext : DbContext, IUnitOfWork
    {
        public FinancasContext(DbContextOptions<FinancasContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancasContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;

            return success;
        }
    }
}
