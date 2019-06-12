using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    public partial class TablasSeguridad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabla",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: false),
                    Descripción = table.Column<string>(type: "VARCHAR(200)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    Adicional1 = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Historial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TabladId = table.Column<int>(nullable: false),
                    OrigenId = table.Column<int>(nullable: false),
                    Actividad = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    Observa = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    TablaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historial_Tabla_TablaId",
                        column: x => x.TablaId,
                        principalTable: "Tabla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historial_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAcceso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    SistemaOperativo = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Navegador = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Ciudad = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    Estado = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    RefreshToken = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    FechaRefresh = table.Column<DateTime>(nullable: false),
                    MantenerSesion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAcceso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioAcceso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioCliente_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: false),
                    RolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historial_TablaId",
                table: "Historial",
                column: "TablaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialTabla",
                table: "Historial",
                column: "TabladId");

            migrationBuilder.CreateIndex(
                name: "IX_ctrUsuario",
                table: "Historial",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividad",
                table: "Historial",
                columns: new[] { "Actividad", "TabladId", "FechaHora" });

            migrationBuilder.CreateIndex(
                name: "IX_Historial",
                table: "Historial",
                columns: new[] { "TabladId", "OrigenId", "Actividad" });

            migrationBuilder.CreateIndex(
                name: "UI_UsuarioClave",
                table: "Usuario",
                column: "Clave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_RefreshToken",
                table: "UsuarioAcceso",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_Token",
                table: "UsuarioAcceso",
                columns: new[] { "UsuarioId", "Token" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCliente_ClienteId",
                table: "UsuarioCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "UI_UsuarioCliente",
                table: "UsuarioCliente",
                columns: new[] { "UsuarioId", "ClienteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UI_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "RolId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historial");

            migrationBuilder.DropTable(
                name: "UsuarioAcceso");

            migrationBuilder.DropTable(
                name: "UsuarioCliente");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Tabla");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
