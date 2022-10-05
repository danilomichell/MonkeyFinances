using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Mappings
{
    public class TransacaoMappins : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(c => c.Valor)
                .IsRequired();
            builder.Property(c => c.DataTransacao)
                .IsRequired();

            builder.HasOne(c => c.Tipo)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(c => c.IdTipo);
            builder.HasOne(c => c.Parcela)
                .WithOne(c => c.Transacao)
                .HasForeignKey<Transacao>(c => c.IdParcela);

            builder.ToTable("Transacoes");
        }
    }
}
