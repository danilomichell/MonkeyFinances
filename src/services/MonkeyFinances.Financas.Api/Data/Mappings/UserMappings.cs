
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Mappings
{
    public class UserMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(c => c.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            builder.HasMany(c => c.Transacoes)
                .WithOne(c => c.User)
                .HasForeignKey(x=>x.UserId);

            builder.ToTable("Users");
        }
    }
}
