using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class Classifieds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Board_AspNetUsers_UserId",
                table: "Board");

            migrationBuilder.DropIndex(
                name: "IX_Board_UserId",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Board");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Board",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Board",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Board",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Board",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Board",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassifiedCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassifiedListing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ClassifiedCategoryId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 75, nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    ListingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedListing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassifiedListing_ClassifiedCategory_ClassifiedCategoryId",
                        column: x => x.ClassifiedCategoryId,
                        principalTable: "ClassifiedCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassifiedListing_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassifiedImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassifiedListingId = table.Column<int>(nullable: false),
                    IsMainImage = table.Column<bool>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassifiedImage_ClassifiedListing_ClassifiedListingId",
                        column: x => x.ClassifiedListingId,
                        principalTable: "ClassifiedListing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedImage_ClassifiedListingId",
                table: "ClassifiedImage",
                column: "ClassifiedListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedListing_ClassifiedCategoryId",
                table: "ClassifiedListing",
                column: "ClassifiedCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedListing_UserId",
                table: "ClassifiedListing",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassifiedImage");

            migrationBuilder.DropTable(
                name: "ClassifiedListing");

            migrationBuilder.DropTable(
                name: "ClassifiedCategory");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Board");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Board");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Board",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Board",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Board",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Board_UserId",
                table: "Board",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Board_AspNetUsers_UserId",
                table: "Board",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
