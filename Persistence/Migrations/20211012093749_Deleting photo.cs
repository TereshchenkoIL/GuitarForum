using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Deletingphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_OwnerId",
                table: "Photos",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_OwnerId",
                table: "Photos",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_OwnerId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_OwnerId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
