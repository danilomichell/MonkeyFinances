using Microsoft.EntityFrameworkCore;

namespace MonkeyFinances.Financas.Api.Data
{
    public class FinancasContext : DbContext//, IUnitOfWork
    {
        public FinancasContext(DbContextOptions<FinancasContext> options)
            : base(options) { }

        //public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancasContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
