using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class stavka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaturskiIspitStavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<int>(nullable: true),
                    MaturskiIspitId = table.Column<int>(nullable: false),
                    DodjeljenPredmetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspitStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavke_DodjeljenPredmet_DodjeljenPredmetId",
                        column: x => x.DodjeljenPredmetId,
                        principalTable: "DodjeljenPredmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavke_MaturskiIspit_MaturskiIspitId",
                        column: x => x.MaturskiIspitId,
                        principalTable: "MaturskiIspit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavke_DodjeljenPredmetId",
                table: "MaturskiIspitStavke",
                column: "DodjeljenPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavke_MaturskiIspitId",
                table: "MaturskiIspitStavke",
                column: "MaturskiIspitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaturskiIspitStavke");
        }
    }
}
