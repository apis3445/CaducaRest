using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{

    /// <summary>
    /// Migración
    /// </summary>
    public partial class RolTablaPermiso : Migration
    {
        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TablaPermiso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TablaId = table.Column<int>(nullable: false),
                    PermisoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaPermiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaPermiso_Permiso_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TablaPermiso_Tabla_TablaId",
                        column: x => x.TablaId,
                        principalTable: "Tabla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolTablaPermiso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TablaPermisoId = table.Column<int>(nullable: false),
                    RolId = table.Column<int>(nullable: false),
                    TienePermiso = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolTablaPermiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolTablaPermiso_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolTablaPermiso_TablaPermiso_TablaPermisoId",
                        column: x => x.TablaPermisoId,
                        principalTable: "TablaPermiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Permiso",
                columns: new[] { "Id", "Clave", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Crear" },
                    { 2, 2, "Modificar" },
                    { 3, 3, "Borrar" },
                    { 4, 4, "Consultar" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 4, "Supervisor" });

            migrationBuilder.InsertData(
                table: "Tabla",
                columns: new[] { "Id", "Descripción", "Nombre" },
                values: new object[,]
                {
                    { 11, "Permite registrar los clientes para los usuarios del sistema", "UsuarioCliente" },
                    { 10, "Permite registrar las categorias de los usuarios del sistema", "UsuarioCategoria" },
                    { 9, "Permite registrar los usuarios del sistema", "Usuario" },
                    { 8, "Permite registrar los roles de los usuarios", "Rol" },
                    { 7, "Permite registrar los productos", "Producto" },
                    { 4, "Permite registrar las categorías de productos de cada cliente", "ClienteCategoria" },
                    { 5, "Permite registrar los productos", "Producto" },
                    { 12, "Permite registrar los roles para los usuarios del sistema", "UsuarioRol" },
                    { 3, "Permite registrar los clientes", "Cliente" },
                    { 2, "Permite registrar las categorias de los productos", "Categoria" },
                    { 1, "Permite registrar las fechas de caducidad de los productos", "Caducidad" },
                    { 6, "Permite registrar los permisos para el sistema", "Permiso" }
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "carlos@gmail.com");

            migrationBuilder.InsertData(
                table: "TablaPermiso",
                columns: new[] { "Id", "PermisoId", "TablaId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 2 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 1, 3 },
                    { 6, 2, 3 },
                    { 7, 3, 3 },
                    { 8, 4, 3 },
                    { 9, 1, 5 },
                    { 10, 2, 5 },
                    { 11, 3, 5 },
                    { 12, 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "RolTablaPermiso",
                columns: new[] { "Id", "RolId", "TablaPermisoId", "TienePermiso" },
                values: new object[,]
                {
                    { 1, 4, 1, true },
                    { 15, 4, 12, true },
                    { 14, 4, 11, true },
                    { 13, 4, 10, true },
                    { 12, 4, 9, true },
                    { 11, 2, 8, true },
                    { 10, 4, 8, true },
                    { 16, 2, 12, true },
                    { 9, 4, 7, true },
                    { 7, 4, 5, true },
                    { 6, 3, 4, true },
                    { 5, 2, 4, true },
                    { 4, 4, 4, true },
                    { 3, 4, 3, true },
                    { 2, 4, 2, true },
                    { 8, 4, 6, true },
                    { 17, 3, 12, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolTablaPermiso_RolId",
                table: "RolTablaPermiso",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UI_TablaPermiso",
                table: "RolTablaPermiso",
                columns: new[] { "TablaPermisoId", "RolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TablaPermiso_PermisoId",
                table: "TablaPermiso",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "UI_TablaPermiso",
                table: "TablaPermiso",
                columns: new[] { "TablaId", "PermisoId" },
                unique: true);
        }

        /// <summary>
        /// Migración
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolTablaPermiso");

            migrationBuilder.DropTable(
                name: "TablaPermiso");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tabla",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "correo@gmail.com");
        }
    }
}
