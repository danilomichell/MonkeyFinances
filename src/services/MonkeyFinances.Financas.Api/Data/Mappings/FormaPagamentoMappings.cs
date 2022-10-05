﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Mappings
{
    public class FormaPagamentoMappings : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}
