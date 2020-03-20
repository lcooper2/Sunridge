using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class keyhisLotMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotNumber = table.Column<string>(nullable: false),
                    TaxId = table.Column<string>(nullable: true),
                    IsArchive = table.Column<bool>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lot_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: true),
                    DateIssued = table.Column<DateTime>(nullable: false),
                    DateReturned = table.Column<DateTime>(nullable: true),
                    PaidAmount = table.Column<float>(nullable: false),
                    KeyId = table.Column<int>(nullable: false),
                    LotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyHistory_Key_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Key",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyHistory_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyHistory_KeyId",
                table: "KeyHistory",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyHistory_LotId",
                table: "KeyHistory",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_AddressId",
                table: "Lot",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyHistory");

            migrationBuilder.DropTable(
                name: "Lot");
        }
    }
}
