using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class ProfPicReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BlogReply",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogReply_ApplicationUserId",
                table: "BlogReply",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogReply_AspNetUsers_ApplicationUserId",
                table: "BlogReply",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogReply_AspNetUsers_ApplicationUserId",
                table: "BlogReply");

            migrationBuilder.DropIndex(
                name: "IX_BlogReply_ApplicationUserId",
                table: "BlogReply");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BlogReply");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");
        }
    }
}
