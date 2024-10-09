using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserOnetoOneWithlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.AddColumn<int>(
                name: "WishlistId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }
    }
}
