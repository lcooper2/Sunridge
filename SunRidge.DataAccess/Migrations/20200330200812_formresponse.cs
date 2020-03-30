using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class formresponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormResponse",
                columns: table => new
                {
                    FormResponseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(nullable: false),
                    FormType = table.Column<string>(maxLength: 3, nullable: false),
                    LotId = table.Column<int>(nullable: true),
                    SubmitDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: true),
                    PrivacyLevel = table.Column<string>(nullable: true),
                    Resolved = table.Column<bool>(nullable: false),
                    ResolveDate = table.Column<DateTime>(nullable: true),
                    ResolveUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormResponse", x => x.FormResponseId);
                    table.ForeignKey(
                        name: "FK_FormResponse_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InKindWorkHours_FormResponseId",
                table: "InKindWorkHours",
                column: "FormResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_FormResponse_LotId",
                table: "FormResponse",
                column: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours",
                column: "FormResponseId",
                principalTable: "FormResponse",
                principalColumn: "FormResponseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InKindWorkHours_FormResponse_FormResponseId",
                table: "InKindWorkHours");

            migrationBuilder.DropTable(
                name: "FormResponse");

            migrationBuilder.DropIndex(
                name: "IX_InKindWorkHours_FormResponseId",
                table: "InKindWorkHours");
        }
    }
}
