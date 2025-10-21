using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaMetropolis.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposInstitucion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Editorial",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorreoContacto",
                table: "Editorial",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SitioWeb",
                table: "Editorial",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Editorial",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorreoContacto",
                table: "Editorial");

            migrationBuilder.DropColumn(
                name: "SitioWeb",
                table: "Editorial");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Editorial");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Editorial",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
