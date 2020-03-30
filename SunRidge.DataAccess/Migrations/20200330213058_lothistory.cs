using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class lothistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormResponse",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "FormResponseId",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "FormResponse");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FormResponse",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FormResponse",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormResponse",
                table: "FormResponse",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LotHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivacyLevel = table.Column<string>(nullable: true),
                    IsArchive = table.Column<bool>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotHistory_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotHistory_LotId",
                table: "LotHistory",
                column: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours",
                column: "FormResponseId",
                principalTable: "FormResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours");

            migrationBuilder.DropTable(
                name: "LotHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormResponse",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FormResponse");

            migrationBuilder.AddColumn<int>(
                name: "FormResponseId",
                table: "FormResponse",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "FormResponse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormResponse",
                table: "FormResponse",
                column: "FormResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours",
                column: "FormResponseId",
                principalTable: "FormResponse",
                principalColumn: "FormResponseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
