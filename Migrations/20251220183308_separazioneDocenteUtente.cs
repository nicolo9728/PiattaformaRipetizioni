using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RipetizioniApp.Migrations
{
    /// <inheritdoc />
    public partial class separazioneDocenteUtente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Docente",
                table: "Docente");

            migrationBuilder.RenameTable(
                name: "Docente",
                newName: "Utente");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Utente",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utente",
                table: "Utente",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Utente",
                table: "Utente");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Utente");

            migrationBuilder.RenameTable(
                name: "Utente",
                newName: "Docente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Docente",
                table: "Docente",
                column: "Id");
        }
    }
}
