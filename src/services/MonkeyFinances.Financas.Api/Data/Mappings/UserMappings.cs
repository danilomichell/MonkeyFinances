using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyFinances.Financas.Api.Models;

namespace MonkeyFinances.Financas.Api.Data.Mappings
{
    public class UserMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(80)");
        }
    }
}
