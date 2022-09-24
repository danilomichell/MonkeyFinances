using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Mappings;

public class ParcelaMappings : IEntityTypeConfiguration<Parcela>
{
    public void Configure(EntityTypeBuilder<Parcela> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.NumParcela)
            .IsRequired();
        builder.Property(c => c.TotalParcelas)
            .IsRequired();

        builder.HasOne(c => c.FormaPagamento)
            .WithMany(c => c.Parcelas)
            .HasForeignKey(c => c.IdFormaPagamento);
        builder.HasOne(c => c.Transacao)
            .WithOne(c => c.Parcela)
            .HasForeignKey<Parcela>(c => c.IdTransacao);

        builder.ToTable("Parcelas");
    }
}