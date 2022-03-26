using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaducaRest.Migrations
{
    public partial class errores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

          
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UsuarioAcceso",
                type: "VARCHAR(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(400)");

            migrationBuilder.AlterColumn<bool>(
                name: "MantenerSesion",
                table: "UsuarioAcceso",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRefresh",
                table: "UsuarioAcceso",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "UsuarioAcceso",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "UsuarioAcceso",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint(1)");


            migrationBuilder.AlterColumn<string>(
                name: "Adicional1",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Usuario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "TienePermiso",
                table: "RolTablaPermiso",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<int>(
                name: "CaducidadId",
                table: "Producto",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaHora",
                table: "Historial",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Cliente",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint(1)");


            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Caducidad",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateTable(
                name: "Errorres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errorres", x => x.Id);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Producto_CaducidadId",
                table: "Producto",
                column: "CaducidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Caducidad_CaducidadId",
                table: "Producto",
                column: "CaducidadId",
                principalTable: "Caducidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Caducidad_CaducidadId",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Errorres");

            migrationBuilder.DropIndex(
                name: "IX_Producto_CaducidadId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "CaducidadId",
                table: "Producto");


            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UsuarioAcceso",
                type: "VARCHAR(400)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)");

            migrationBuilder.AlterColumn<byte>(
                name: "MantenerSesion",
                table: "UsuarioAcceso",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<byte>(
                name: "Activo",
                table: "UsuarioAcceso",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Adicional1",
                table: "Usuario",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte>(
                name: "Activo",
                table: "Usuario",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");


            migrationBuilder.AlterColumn<byte>(
                name: "TienePermiso",
                table: "RolTablaPermiso",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");


            migrationBuilder.AlterColumn<byte>(
                name: "Activo",
                table: "Cliente",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");


            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 1,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 2,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 3,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 4,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 5,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 6,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 7,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 8,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 9,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 10,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 11,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 12,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 13,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 14,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 15,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 16,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "RolTablaPermiso",
                keyColumn: "Id",
                keyValue: 17,
                column: "TienePermiso",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Activo",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 2,
                column: "Activo",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 3,
                column: "Activo",
                value: (byte)1);
        }
    }
}
