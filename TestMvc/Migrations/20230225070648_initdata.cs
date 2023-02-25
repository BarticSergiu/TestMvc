using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvc.Migrations
{
    /// <inheritdoc />
    public partial class initdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persoana",
                columns: table => new
                {
                    PersoanaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersoanaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persoana", x => x.PersoanaId);
                    table.ForeignKey(
                        name: "FK_Persoana_Persoana_PersoanaId1",
                        column: x => x.PersoanaId1,
                        principalTable: "Persoana",
                        principalColumn: "PersoanaId");
                });

            migrationBuilder.CreateTable(
                name: "Sarcina",
                columns: table => new
                {
                    SarcinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioritate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipSarcina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OreEstimate = table.Column<int>(type: "int", nullable: false),
                    PersoanaRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sarcina", x => x.SarcinaId);
                    table.ForeignKey(
                        name: "FK_Sarcina_Persoana_PersoanaRefId",
                        column: x => x.PersoanaRefId,
                        principalTable: "Persoana",
                        principalColumn: "PersoanaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pontaj",
                columns: table => new
                {
                    PontajId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<int>(type: "int", nullable: false),
                    Durata = table.Column<int>(type: "int", nullable: false),
                    SarcinaRefId = table.Column<int>(type: "int", nullable: false),
                    Observatii = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontaj", x => x.PontajId);
                    table.ForeignKey(
                        name: "FK_Pontaj_Sarcina_SarcinaRefId",
                        column: x => x.SarcinaRefId,
                        principalTable: "Sarcina",
                        principalColumn: "SarcinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persoana_PersoanaId1",
                table: "Persoana",
                column: "PersoanaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pontaj_SarcinaRefId",
                table: "Pontaj",
                column: "SarcinaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sarcina_PersoanaRefId",
                table: "Sarcina",
                column: "PersoanaRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontaj");

            migrationBuilder.DropTable(
                name: "Sarcina");

            migrationBuilder.DropTable(
                name: "Persoana");
        }
    }
}
