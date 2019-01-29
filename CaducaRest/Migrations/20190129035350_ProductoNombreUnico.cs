using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Indice unico para el nombre de los productos
    /// </summary>
    public partial class ProductoNombreUnico : Migration
    {
        /// <summary>
        /// Indice unico para el nombre de los productos
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_ProductoNombre",
                table: "Producto",
                column: "Nombre",
                unique: true);
        }

        /// <summary>
        /// Indice unico para el nombre de los productos
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_ProductoNombre",
                table: "Producto");
        }
    }
}
