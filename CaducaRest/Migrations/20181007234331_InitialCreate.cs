using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Migrations
{
    /// <summary>
    /// Tabla categoría
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// Tabla categoría
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                      .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                      //.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)  ,                   
                    Clave = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });
        }

        /// <summary>
        /// Borrado de tabla categoría
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
