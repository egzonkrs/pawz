using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePetsFromSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Species_SpeciesId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_SpeciesId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Pets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpeciesId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SpeciesId",
                table: "Pets",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Species_SpeciesId",
                table: "Pets",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id");
        }
    }
}
