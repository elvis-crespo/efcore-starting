using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_Ejemplo.Migrations
{
    /// <inheritdoc />
    public partial class IndiceUnicoNombreGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Generos_Name",
                table: "Generos",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Generos_Name",
                table: "Generos");
        }
    }
}
