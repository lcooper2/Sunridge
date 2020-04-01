using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class inkinrework : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
