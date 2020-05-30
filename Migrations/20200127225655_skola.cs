using Microsoft.EntityFrameworkCore.Migrations;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class skola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkolaId",
                table: "MaturskiIspit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_SkolaId",
                table: "MaturskiIspit",
                column: "SkolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaturskiIspit_Skola_SkolaId",
                table: "MaturskiIspit",
                column: "SkolaId",
                principalTable: "Skola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaturskiIspit_Skola_SkolaId",
                table: "MaturskiIspit");

            migrationBuilder.DropIndex(
                name: "IX_MaturskiIspit_SkolaId",
                table: "MaturskiIspit");

            migrationBuilder.DropColumn(
                name: "SkolaId",
                table: "MaturskiIspit");
        }
    }
}
