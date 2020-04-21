using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class erikaformtweak1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitDate",
                table: "FormResponse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitDate",
                table: "FormResponse",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
