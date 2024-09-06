using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationIdToAdoptionRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "AdoptionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_LocationId",
                table: "AdoptionRequests",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Locations_LocationId",
                table: "AdoptionRequests",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Locations_LocationId",
                table: "AdoptionRequests");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_LocationId",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AdoptionRequests");
        }
    }
}
