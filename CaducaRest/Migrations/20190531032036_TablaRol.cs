using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    public partial class TablaRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 1, "Administrador" });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 2, "Vendedor" });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 3, "Cliente" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
