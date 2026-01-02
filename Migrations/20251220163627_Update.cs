using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RipetizioniApp.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cognome",
                table: "Docente",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Data_Nascita",
                table: "Docente",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Docente",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cognome",
                table: "Docente");

            migrationBuilder.DropColumn(
                name: "Data_Nascita",
                table: "Docente");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Docente");
        }
    }
}
