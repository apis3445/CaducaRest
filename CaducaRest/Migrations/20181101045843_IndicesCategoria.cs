﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    public partial class IndicesCategoria : Migration
    {
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
