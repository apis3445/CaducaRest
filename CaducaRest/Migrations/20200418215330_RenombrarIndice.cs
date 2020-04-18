using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    public partial class RenombrarIndice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UI_TablaPermiso",
                table: "RolTablaPermiso",
                newName: "UI_RolTablaPermiso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UI_RolTablaPermiso",
                table: "RolTablaPermiso",
                newName: "UI_TablaPermiso");
        }
    }
}
