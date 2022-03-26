using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Migración
    /// </summary>
    public partial class IXProductoClave : Migration
    {
        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_ProductoClave",
                table: "Producto",
                column: "Clave",
                unique: true);
        }

        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_ProductoClave",
                table: "Producto");
        }
    }
}