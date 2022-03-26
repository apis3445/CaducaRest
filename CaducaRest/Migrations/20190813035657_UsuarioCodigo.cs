using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Agregamos campos para registrar el número de intentos incorrectos y el código
    /// para desbloquear el usuario.
    /// </summary>
    public partial class UsuarioCodigo : Migration
    {
        /// <summary>
        /// Agregamos campos para registrar el número de intentos incorrectos y el código
        /// para desbloquear el usuario.
        /// </summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intentos",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);
        }
        /// <summary>
        /// Agregamos campos para registrar el número de intentos incorrectos y el código
        /// para desbloquear el usuario.
        /// </summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Intentos",
                table: "Usuario");
        }
    }
}
