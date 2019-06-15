using Microsoft.EntityFrameworkCore.Migrations;

namespace CaducaRest.Migrations
{
    public partial class UsuariosRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_ClienteProducto",
                table: "Caducidad");

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Activo", "Adicional1", "Clave", "Email", "Nombre", "Password" },
                values: new object[] { 1, true, "2a3efe03a96840478bde71ae36a20f2e", "Juan", "correo@gmail.com", "Juan Peréz", "9f9b901a43d795295661443f7f7098ee8e6c6c3694428717c54d5fd058220fed" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Activo", "Adicional1", "Clave", "Email", "Nombre", "Password" },
                values: new object[] { 2, true, "37b93bbd77b2d7a586cc7d5032f83808", "Maria", "correo@gmail.com", "Maria Lopez", "6ad9ebcfe2bebed6655a4abb3e0409c83ad1e6db35098083476744cfe0d106b9" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Activo", "Adicional1", "Clave", "Email", "Nombre", "Password" },
                values: new object[] { 3, true, "5dd69f799e8ac1fd877460c4d461eb74", "Carlos", "correo@gmail.com", "Carlos Hernández", "6c60e72d7ea36a7defc15f0b551cd739180d2254ddaf4c8833ece2ecf8b48c5a" });

            migrationBuilder.InsertData(
                table: "UsuarioRol",
                columns: new[] { "Id", "RolId", "UsuarioId" },
                values: new object[] { 1, 3, 1 });

            migrationBuilder.InsertData(
                table: "UsuarioRol",
                columns: new[] { "Id", "RolId", "UsuarioId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "UsuarioRol",
                columns: new[] { "Id", "RolId", "UsuarioId" },
                values: new object[] { 3, 1, 3 });

            migrationBuilder.CreateIndex(
                name: "UI_ClienteProducto",
                table: "Caducidad",
                columns: new[] { "ClienteId", "ProductoId", "Fecha" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_ClienteProducto",
                table: "Caducidad");

            migrationBuilder.DeleteData(
                table: "UsuarioRol",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UsuarioRol",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UsuarioRol",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateIndex(
                name: "UI_ClienteProducto",
                table: "Caducidad",
                columns: new[] { "ClienteId", "ProductoId" },
                unique: true);
        }
    }
}
