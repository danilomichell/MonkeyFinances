using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Mappings
{
    public class TransacaoMappings : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(x => x.Valor)
                .IsRequired();

            builder.Property(x => x.NumParcela)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(x => x.TotalParcelas)
                .IsRequired();
            builder.HasOne(x => x.FormaPagamento)
                .WithMany(x => x.Transacaos)
                .HasForeignKey(x => x.FormaPagamentoId);

            builder.HasOne(x => x.Tipo)
                    .WithMany(x => x.Transacoes)
                    .HasForeignKey(x => x.TipoId);
        }
    }
}
