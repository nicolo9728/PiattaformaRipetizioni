using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RipetizioniApp.Migrations
{
    /// <inheritdoc />
    public partial class Prova3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Utente_ListaMaterieDocenteId",
                table: "Materia");

            migrationBuilder.RenameColumn(
                name: "ListaMaterieDocenteId",
                table: "Materia",
                newName: "DocenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Utente_DocenteId",
                table: "Materia",
                column: "DocenteId",
                principalTable: "Utente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Utente_DocenteId",
                table: "Materia");

            migrationBuilder.RenameColumn(
                name: "DocenteId",
                table: "Materia",
                newName: "ListaMaterieDocenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Utente_ListaMaterieDocenteId",
                table: "Materia",
                column: "ListaMaterieDocenteId",
                principalTable: "Utente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
