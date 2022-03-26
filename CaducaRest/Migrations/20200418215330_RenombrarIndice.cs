using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Renombrar indice
    /// </summary>
    public partial class RenombrarIndice : Migration
    {
        /// <summary>
        /// Renombrar indice
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UI_TablaPermiso",
                table: "RolTablaPermiso",
                newName: "UI_RolTablaPermiso");
        }

        /// <summary>
        /// Rollback renombrar indice
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UI_RolTablaPermiso",
                table: "RolTablaPermiso",
                newName: "UI_TablaPermiso");
        }
    }
}
