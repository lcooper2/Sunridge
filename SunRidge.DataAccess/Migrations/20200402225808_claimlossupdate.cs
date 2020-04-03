using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class claimlossupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_FormResponse_FormResponseId",
                table: "File");

            migrationBuilder.DropForeignKey(
                name: "FK_FormResponse_Lot_LotId",
                table: "FormResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours");

            migrationBuilder.DropIndex(
                name: "IX_InKindWorkHours_FormResponseId",
                table: "InKindWorkHours");

            migrationBuilder.DropIndex(
                name: "IX_FormResponse_LotId",
                table: "FormResponse");

            migrationBuilder.DropIndex(
                name: "IX_File_FormResponseId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InKindWorkHours");

            migrationBuilder.DropColumn(
                name: "FormResponseId",
                table: "InKindWorkHours");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "LotId",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "FormResponseId",
                table: "File");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "InKindWorkHours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "InKindWorkHours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Equipment",
                table: "InKindWorkHours",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClaimLoss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isAttorney = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    DateofIncident = table.Column<DateTime>(nullable: false),
                    TimeofIncident = table.Column<DateTime>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ClaimAddress = table.Column<string>(nullable: true),
                    LotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimLoss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimLoss_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimLoss_LotId",
                table: "ClaimLoss",
                column: "LotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimLoss");

            migrationBuilder.DropColumn(
                name: "Activity",
                table: "InKindWorkHours");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "InKindWorkHours");

            migrationBuilder.DropColumn(
                name: "Equipment",
                table: "InKindWorkHours");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InKindWorkHours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormResponseId",
                table: "InKindWorkHours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FormResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LotId",
                table: "FormResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormResponseId",
                table: "File",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InKindWorkHours_FormResponseId",
                table: "InKindWorkHours",
                column: "FormResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_FormResponse_LotId",
                table: "FormResponse",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_File_FormResponseId",
                table: "File",
                column: "FormResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_FormResponse_FormResponseId",
                table: "File",
                column: "FormResponseId",
                principalTable: "FormResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormResponse_Lot_LotId",
                table: "FormResponse",
                column: "LotId",
                principalTable: "Lot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours",
                column: "FormResponseId",
                principalTable: "FormResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
