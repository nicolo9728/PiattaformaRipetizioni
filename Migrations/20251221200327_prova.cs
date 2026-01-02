using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RipetizioniApp.Migrations
{
    /// <inheritdoc />
    public partial class prova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CriterioPagamento",
                table: "Utente",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tariffa",
                table: "Utente",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriterioPagamento",
                table: "Utente");

            migrationBuilder.DropColumn(
                name: "Tariffa",
                table: "Utente");
        }
    }
}
