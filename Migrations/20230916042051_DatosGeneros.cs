using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore_Ejemplo.Migrations
{
    /// <inheritdoc />
    public partial class DatosGeneros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "Peliculas",
                newName: "Titulo");

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Documental" },
                    { 3, "Fantasía" },
                    { 4, "Musical" },
                    { 5, "Aventura" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Peliculas",
                newName: "titulo");
        }
    }
}
