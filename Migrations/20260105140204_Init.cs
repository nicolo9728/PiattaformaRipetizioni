using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RipetizioniApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Cognome = table.Column<string>(type: "text", nullable: false),
                    Data_Nascita = table.Column<DateOnly>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CriterioPagamento = table.Column<string>(type: "text", nullable: true),
                    Tariffa = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    DocenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => new { x.DocenteId, x.Id });
                    table.ForeignKey(
                        name: "FK_Materia_Utente_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Utente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Richiesta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDocente = table.Column<Guid>(type: "uuid", nullable: false),
                    IdStudente = table.Column<Guid>(type: "uuid", nullable: false),
                    Causa = table.Column<string>(type: "text", nullable: true),
                    Modalita = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    MetodoPagamentoAccettato = table.Column<string>(type: "text", nullable: true),
                    TariffaAccettata = table.Column<int>(type: "integer", nullable: true),
                    MetodoPagamentoProposto = table.Column<string>(type: "text", nullable: true),
                    TariffaProposta = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Richiesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Richiesta_Utente_IdDocente",
                        column: x => x.IdDocente,
                        principalTable: "Utente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Richiesta_Utente_IdStudente",
                        column: x => x.IdStudente,
                        principalTable: "Utente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposta",
                columns: table => new
                {
                    RichiestaEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Momento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MetodoPagamentoProposto = table.Column<string>(type: "text", nullable: false),
                    TariffaProposta = table.Column<int>(type: "integer", nullable: false),
                    IdDocente = table.Column<Guid>(type: "uuid", nullable: true),
                    IdStudente = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposta", x => new { x.RichiestaEntityId, x.Id });
                    table.ForeignKey(
                        name: "FK_Proposta_Richiesta_RichiestaEntityId",
                        column: x => x.RichiestaEntityId,
                        principalTable: "Richiesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Richiesta_IdDocente",
                table: "Richiesta",
                column: "IdDocente");

            migrationBuilder.CreateIndex(
                name: "IX_Richiesta_IdStudente",
                table: "Richiesta",
                column: "IdStudente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Proposta");

            migrationBuilder.DropTable(
                name: "Richiesta");

            migrationBuilder.DropTable(
                name: "Utente");
        }
    }
}
