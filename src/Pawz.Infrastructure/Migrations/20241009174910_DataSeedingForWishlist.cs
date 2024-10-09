using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingForWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Pets_PetId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_PetId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Wishlists");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetWishlist");

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_PetId",
                table: "Wishlists",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Pets_PetId",
                table: "Wishlists",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
