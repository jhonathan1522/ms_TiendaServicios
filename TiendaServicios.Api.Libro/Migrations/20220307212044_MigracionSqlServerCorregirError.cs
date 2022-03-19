using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaServicios.Api.Libro.Migrations
{
    public partial class MigracionSqlServerCorregirError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AutorLibro",
                table: "LibreriaMaterial",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibreriaMaterialGuid",
                table: "LibreriaMaterial",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LibreriaMaterialGuid",
                table: "LibreriaMaterial");

            migrationBuilder.AlterColumn<Guid>(
                name: "AutorLibro",
                table: "LibreriaMaterial",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
