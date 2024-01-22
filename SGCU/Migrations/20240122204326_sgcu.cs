using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGCU.Migrations
{
    /// <inheritdoc />
    public partial class sgcu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    ColaboradorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.ColaboradorId);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoUnidade = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.UnidadeId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_CodigoUnidade",
                table: "Unidades",
                column: "CodigoUnidade",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
