using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaMetropolis.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionesYTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    IdAutor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.IdAutor);
                });

            migrationBuilder.CreateTable(
                name: "Editorial",
                columns: table => new
                {
                    IdEdit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editorial", x => x.IdEdit);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    IdPais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.IdPais);
                });

            migrationBuilder.CreateTable(
                name: "TipoRecurso",
                columns: table => new
                {
                    IdTipoR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRecurso", x => x.IdTipoR);
                });

            migrationBuilder.CreateTable(
                name: "Recurso",
                columns: table => new
                {
                    IdRec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AnnoPublic = table.Column<int>(type: "int", nullable: false),
                    Edicion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PalabrasBusqueda = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CantidadUnidades = table.Column<int>(type: "int", nullable: false),
                    PrecioIndividual = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IdPais = table.Column<int>(type: "int", nullable: false),
                    IdTipoR = table.Column<int>(type: "int", nullable: false),
                    IdEdit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurso", x => x.IdRec);
                    table.ForeignKey(
                        name: "FK_Recurso_Editorial_IdEdit",
                        column: x => x.IdEdit,
                        principalTable: "Editorial",
                        principalColumn: "IdEdit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recurso_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Pais",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recurso_TipoRecurso_IdTipoR",
                        column: x => x.IdTipoR,
                        principalTable: "TipoRecurso",
                        principalColumn: "IdTipoR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutoresRecursos",
                columns: table => new
                {
                    IdRec = table.Column<int>(type: "int", nullable: false),
                    IdAutor = table.Column<int>(type: "int", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoresRecursos", x => new { x.IdRec, x.IdAutor });
                    table.ForeignKey(
                        name: "FK_AutoresRecursos_Autor_IdAutor",
                        column: x => x.IdAutor,
                        principalTable: "Autor",
                        principalColumn: "IdAutor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoresRecursos_Recurso_IdRec",
                        column: x => x.IdRec,
                        principalTable: "Recurso",
                        principalColumn: "IdRec",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoresRecursos_IdAutor",
                table: "AutoresRecursos",
                column: "IdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_IdEdit",
                table: "Recurso",
                column: "IdEdit");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_IdPais",
                table: "Recurso",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_IdTipoR",
                table: "Recurso",
                column: "IdTipoR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoresRecursos");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Recurso");

            migrationBuilder.DropTable(
                name: "Editorial");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "TipoRecurso");
        }
    }
}
