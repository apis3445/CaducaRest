﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Indices
    /// </summary>
    public partial class IndicePermiso : Migration
    {
        /// <summary>
        /// Creacion de indices
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_ProductoNombre",
                table: "Producto",
                newName: "UI_ProductoNombre");

            migrationBuilder.RenameIndex(
                name: "UX_ProductoClave",
                table: "Producto",
                newName: "UI_ProductoClave");

            migrationBuilder.CreateIndex(
                name: "UI_PermisoClave",
                table: "Permiso",
                column: "Clave",
                unique: true);
        }

        /// <summary>
        /// Borrado de indices
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_PermisoClave",
                table: "Permiso");

            migrationBuilder.RenameIndex(
                name: "UI_ProductoNombre",
                table: "Producto",
                newName: "UX_ProductoNombre");

            migrationBuilder.RenameIndex(
                name: "UI_ProductoClave",
                table: "Producto",
                newName: "UX_ProductoClave");
        }
    }
}
