using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class subrework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormType",
                table: "FormSubmissions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCl",
                table: "FormSubmissions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSC",
                table: "FormSubmissions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWik",
                table: "FormSubmissions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCl",
                table: "FormSubmissions");

            migrationBuilder.DropColumn(
                name: "IsSC",
                table: "FormSubmissions");

            migrationBuilder.DropColumn(
                name: "IsWik",
                table: "FormSubmissions");

            migrationBuilder.AddColumn<string>(
                name: "FormType",
                table: "FormSubmissions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
