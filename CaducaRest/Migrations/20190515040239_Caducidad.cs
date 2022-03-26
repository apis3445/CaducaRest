using System;
using Microsoft.EntityFrameworkCore.Metadata;
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
            migrationBuilder.CreateTable(
                name: "Caducidad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caducidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caducidad_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Caducidad_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            
           
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
