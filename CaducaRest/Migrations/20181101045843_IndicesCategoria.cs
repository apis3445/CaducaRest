using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Índices categoría
    /// </summary>
    public partial class IndicesCategoria : Migration
    {
        /// <summary>
        /// Índices categoría
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UI_CategoriaClave",
                table: "Categoria",
                column: "Clave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_CategoriaNombre",
                table: "Categoria",
                column: "Nombre",
                unique: true);
        }

        /// <summary>
        /// Borrrado de Índices categoría
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_CategoriaClave",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "UI_CategoriaNombre",
                table: "Categoria");
        }
    }
}
