using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserIDtypetostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostedByUserId",
                table: "Pets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequesterUserId",
                table: "AdoptionRequests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PostedByUserId",
                table: "Pets",
                column: "PostedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_RequesterUserId",
                table: "AdoptionRequests",
                column: "RequesterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_AspNetUsers_RequesterUserId",
                table: "AdoptionRequests",
                column: "RequesterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_PostedByUserId",
                table: "Pets",
                column: "PostedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_AspNetUsers_RequesterUserId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_PostedByUserId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_PostedByUserId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_RequesterUserId",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "PostedByUserId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RequesterUserId",
                table: "AdoptionRequests");
        }
    }
}
