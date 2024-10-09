using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WishlistAndPetJointTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetWishlist");

            migrationBuilder.CreateTable(
                name: "WishlistPets",
                columns: table => new
                {
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistPets", x => new { x.WishlistId, x.PetId });
                    table.ForeignKey(
                        name: "FK_WishlistPets_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistPets_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistPets_PetId",
                table: "WishlistPets",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistPets");

            migrationBuilder.CreateTable(
                name: "PetWishlist",
                columns: table => new
                {
                    PetsId = table.Column<int>(type: "int", nullable: false),
                    WishlistedByUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetWishlist", x => new { x.PetsId, x.WishlistedByUsersId });
                    table.ForeignKey(
                        name: "FK_PetWishlist_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetWishlist_Wishlists_WishlistedByUsersId",
                        column: x => x.WishlistedByUsersId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetWishlist_WishlistedByUsersId",
                table: "PetWishlist",
                column: "WishlistedByUsersId");
        }
    }
}
