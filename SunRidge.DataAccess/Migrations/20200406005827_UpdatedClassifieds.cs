using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class UpdatedClassifieds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ClassifiedListing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ClassifiedListing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ClassifiedListing");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ClassifiedListing");
        }
    }
}
