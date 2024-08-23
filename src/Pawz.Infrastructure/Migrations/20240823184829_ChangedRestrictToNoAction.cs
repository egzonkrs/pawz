using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRestrictToNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_AspNetUsers_RequesterUserId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Pets_PetId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PetImages_Pets_PetId",
                table: "PetImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_PostedByUserId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Breeds_BreedId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Locations_LocationId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Species_SpeciesId",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_AspNetUsers_RequesterUserId",
                table: "AdoptionRequests",
                column: "RequesterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Pets_PetId",
                table: "AdoptionRequests",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetImages_Pets_PetId",
                table: "PetImages",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_PostedByUserId",
                table: "Pets",
                column: "PostedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Breeds_BreedId",
                table: "Pets",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Locations_LocationId",
                table: "Pets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Species_SpeciesId",
                table: "Pets",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_AspNetUsers_RequesterUserId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Pets_PetId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PetImages_Pets_PetId",
                table: "PetImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_PostedByUserId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Breeds_BreedId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Locations_LocationId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Species_SpeciesId",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_AspNetUsers_RequesterUserId",
                table: "AdoptionRequests",
                column: "RequesterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Pets_PetId",
                table: "AdoptionRequests",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PetImages_Pets_PetId",
                table: "PetImages",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_PostedByUserId",
                table: "Pets",
                column: "PostedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Breeds_BreedId",
                table: "Pets",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Locations_LocationId",
                table: "Pets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Species_SpeciesId",
                table: "Pets",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
