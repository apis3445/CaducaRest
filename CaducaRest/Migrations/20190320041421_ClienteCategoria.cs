using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Tabla Cliente Categoría
    /// </summary>
    public partial class ClienteCategoria : Migration
    {
        /// <summary>
        /// Tabla Cliente Categoría
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteCategoria_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteCategoria_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UI_ClienteCategoriaClave",
                table: "Cliente",
                column: "Clave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_ClienteCategoriaNombre",
                table: "Cliente",
                column: "RazonSocial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienteCategoria_CategoriaId",
                table: "ClienteCategoria",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "UI_ClienteForo",
                table: "ClienteCategoria",
                columns: new[] { "ClienteId", "CategoriaId" },
                unique: true);
        }

        /// <summary>
        /// Tabla Cliente Categoría
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteCategoria");

            migrationBuilder.DropIndex(
                name: "UI_ClienteCategoriaClave",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "UI_ClienteCategoriaNombre",
                table: "Cliente");
        }
    }
}
