using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Se incrementa el tamaño del campo token
    /// </summary>
    public partial class UsuarioToken : Migration
    {
        /// <summary>
        /// Se incrementa el tamaño del campo token
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UsuarioAcceso",
                type: "VARCHAR(400)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(300)");

        }
        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UsuarioAcceso",
                type: "VARCHAR(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(400)");

           

        }
    }
}
