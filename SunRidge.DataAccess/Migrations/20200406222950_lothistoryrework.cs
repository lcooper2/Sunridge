using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class lothistoryrework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotHistory_Lot_LotId",
                table: "LotHistory");

            migrationBuilder.DropIndex(
                name: "IX_LotHistory_LotId",
                table: "LotHistory");

            migrationBuilder.DropColumn(
                name: "LotId",
                table: "LotHistory");

            migrationBuilder.AddColumn<int>(
                name: "OwnerLotId",
                table: "LotHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LotHistory_OwnerLotId",
                table: "LotHistory",
                column: "OwnerLotId");

            migrationBuilder.AddForeignKey(
                name: "FK_LotHistory_OwnerLot_OwnerLotId",
                table: "LotHistory",
                column: "OwnerLotId",
                principalTable: "OwnerLot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotHistory_OwnerLot_OwnerLotId",
                table: "LotHistory");

            migrationBuilder.DropIndex(
                name: "IX_LotHistory_OwnerLotId",
                table: "LotHistory");

            migrationBuilder.DropColumn(
                name: "OwnerLotId",
                table: "LotHistory");

            migrationBuilder.AddColumn<int>(
                name: "LotId",
                table: "LotHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LotHistory_LotId",
                table: "LotHistory",
                column: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_LotHistory_Lot_LotId",
                table: "LotHistory",
                column: "LotId",
                principalTable: "Lot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
