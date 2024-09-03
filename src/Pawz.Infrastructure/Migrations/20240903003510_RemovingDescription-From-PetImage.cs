using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingDescriptionFromPetImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PetImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PetImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
