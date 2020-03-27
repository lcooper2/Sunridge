using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class banner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Active",
            //    table: "Banner");

            //migrationBuilder.AddColumn<string>(
            //    name: "Status",
            //    table: "Banner",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.CreateTable(
            //    name: "Board",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: false),
            //        Image = table.Column<string>(nullable: false),
            //        ApplicationUserId = table.Column<int>(nullable: false),
            //        UserId = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Board", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Board_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Board_UserId",
            //    table: "Board",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Board");

            //migrationBuilder.DropColumn(
            //    name: "Status",
            //    table: "Banner");

            //migrationBuilder.AddColumn<bool>(
            //    name: "Active",
            //    table: "Banner",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);
        }
    }
}
