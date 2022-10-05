using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MonkeyFinances.Core.Data;
using MonkeyFinances.Core.DomainObject;
using MonkeyFinances.Core.Mediator;
using MonkeyFinances.Core.Messages;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data
{
    public sealed class FinancasContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public FinancasContext(DbContextOptions<FinancasContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<User?> Users { get; set; } = null!;
        public DbSet<Transacao> Transacaos { get; set; } = null!;
        public DbSet<FormaPagamento> FormaPagamentos { get; set; } = null!;
        public DbSet<Parcela> Parcelas { get; set; } = null!;
        public DbSet<Tipo> Tipos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancasContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    }
    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var entityEntries = domainEntities as EntityEntry<Entity>[] ?? domainEntities.ToArray();
            var domainEvents = entityEntries
                .SelectMany(x => x.Entity.Notificacoes!)
                .ToList();

            entityEntries.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
