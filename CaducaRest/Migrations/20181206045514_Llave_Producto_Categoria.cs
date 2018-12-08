using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    public partial class Llave_Producto_Categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductoCategoria",
                table: "Producto",
                column: "CategoriaId",
                unique: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Categoria",
                table: "Producto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Categoria_CategoriaId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_ProductoCategoria",
                table: "Producto");
        }
    }
}
