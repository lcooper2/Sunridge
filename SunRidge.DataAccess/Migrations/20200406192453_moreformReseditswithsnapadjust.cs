using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class moreformReseditswithsnapadjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormSubmissionsId",
                table: "FormResponse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FormSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmitDate = table.Column<DateTime>(nullable: false),
                    FormType = table.Column<string>(nullable: true),
                    FormId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSubmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionComplaint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Suggestion = table.Column<string>(nullable: true),
                    Complaint = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionComplaint", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormResponse_FormSubmissionsId",
                table: "FormResponse",
                column: "FormSubmissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormResponse_FormSubmissions_FormSubmissionsId",
                table: "FormResponse",
                column: "FormSubmissionsId",
                principalTable: "FormSubmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormResponse_FormSubmissions_FormSubmissionsId",
                table: "FormResponse");

            migrationBuilder.DropTable(
                name: "FormSubmissions");

            migrationBuilder.DropTable(
                name: "SuggestionComplaint");

            migrationBuilder.DropIndex(
                name: "IX_FormResponse_FormSubmissionsId",
                table: "FormResponse");

            migrationBuilder.DropColumn(
                name: "FormSubmissionsId",
                table: "FormResponse");
        }
    }
}
