﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonkeyFinances.Financas.Api.Data;

#nullable disable

namespace MonkeyFinances.Financas.Api.Migrations
{
    [DbContext(typeof(FinancasContext))]
    [Migration("20221005173241_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.FormaPagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("FormaPagamentos");
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.Tipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataTransacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("FormaPagamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NumParcela")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ParcelaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TipoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalParcelas")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FormaPagamentoId");

                    b.HasIndex("TipoId");

                    b.HasIndex("UserId");

                    b.ToTable("Transacaos");
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.Transacao", b =>
                {
                    b.HasOne("MonkeyFinances.Financas.Api.Models.Entities.FormaPagamento", "FormaPagamento")
                        .WithMany("Transacaos")
                        .HasForeignKey("FormaPagamentoId")
                        .IsRequired();

                    b.HasOne("MonkeyFinances.Financas.Api.Models.Entities.Tipo", "Tipo")
                        .WithMany("Transacoes")
                        .HasForeignKey("TipoId")
                        .IsRequired();

                    b.HasOne("MonkeyFinances.Financas.Api.Models.Entities.User", "User")
                        .WithMany("Transacoes")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("FormaPagamento");

                    b.Navigation("Tipo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.FormaPagamento", b =>
                {
                    b.Navigation("Transacaos");
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.Tipo", b =>
                {
                    b.Navigation("Transacoes");
                });

            modelBuilder.Entity("MonkeyFinances.Financas.Api.Models.Entities.User", b =>
                {
                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}