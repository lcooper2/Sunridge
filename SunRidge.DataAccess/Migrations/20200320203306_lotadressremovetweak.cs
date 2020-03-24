using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class lotadressremovetweak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lot_Address_AddressId",
                table: "Lot");

            migrationBuilder.DropIndex(
                name: "IX_Lot_AddressId",
                table: "Lot");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Lot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Lot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lot_AddressId",
                table: "Lot",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_Address_AddressId",
                table: "Lot",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
