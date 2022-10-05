using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyFinances.Financas.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaPagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTransacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFormaPagamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumParcela = table.Column<int>(type: "int", nullable: false),
                    TotalParcelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcelas_FormaPagamentos_IdFormaPagamento",
                        column: x => x.IdFormaPagamento,
                        principalTable: "FormaPagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTipo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdParcela = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Parcelas_IdParcela",
                        column: x => x.IdParcela,
                        principalTable: "Parcelas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacoes_Tipos_IdTipo",
                        column: x => x.IdTipo,
                        principalTable: "Tipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacoes_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_IdFormaPagamento",
                table: "Parcelas",
                column: "IdFormaPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_IdParcela",
                table: "Transacoes",
                column: "IdParcela",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_IdTipo",
                table: "Transacoes",
                column: "IdTipo");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_IdUser",
                table: "Transacoes",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "Tipos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FormaPagamentos");
        }
    }
}
