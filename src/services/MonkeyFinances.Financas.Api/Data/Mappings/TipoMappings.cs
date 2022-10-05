using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Mappings;

public class TipoMappings : IEntityTypeConfiguration<Tipo>
{
    public void Configure(EntityTypeBuilder<Tipo> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasColumnType("varchar(100)"); 

        builder.ToTable("Tipos");
    }
}