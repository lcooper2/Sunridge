using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class commentmodelredo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Lot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Private = table.Column<bool>(nullable: false),
                    LotHistoryId = table.Column<int>(nullable: true),
                    FormResponseId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_FormResponse_FormResponseId",
                        column: x => x.FormResponseId,
                        principalTable: "FormResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_LotHistory_LotHistoryId",
                        column: x => x.LotHistoryId,
                        principalTable: "LotHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lot_AddressId",
                table: "Lot",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ApplicationUserId",
                table: "Comment",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FormResponseId",
                table: "Comment",
                column: "FormResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_LotHistoryId",
                table: "Comment",
                column: "LotHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_Address_AddressId",
                table: "Lot",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lot_Address_AddressId",
                table: "Lot");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Lot_AddressId",
                table: "Lot");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Lot");
        }
    }
}
