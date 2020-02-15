using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Migración
    /// </summary>
    public partial class Caducidad : Migration
    {
        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateIndex(
                name: "IX_Caducidad_ProductoId",
                table: "Caducidad",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "UI_ClienteProducto",
                table: "Caducidad",
                columns: new[] { "ClienteId", "ProductoId" },
                unique: true);
        }

        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Caducidad_ProductoId",
                table: "Caducidad");

            migrationBuilder.DropIndex(
                name: "UI_ClienteProducto",
                table: "Caducidad");

            migrationBuilder.RenameIndex(
                name: "UI_ClienteCategoria",
                table: "ClienteCategoria",
                newName: "UI_ClienteForo");

            migrationBuilder.CreateIndex(
                name: "IX_Caducidad_ClienteId",
                table: "Caducidad",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "UI_Caducidad",
                table: "Caducidad",
                columns: new[] { "ProductoId", "ClienteId", "Fecha" },
                unique: true);
        }
    }
}
