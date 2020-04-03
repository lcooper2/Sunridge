using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class BlogModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogThread",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    WhenPosted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogThread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogThread_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    BlogThreadId = table.Column<int>(nullable: false),
                    WhenPosted = table.Column<DateTime>(nullable: false),
                    BlogCommentText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComment_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BlogComment_BlogThread_BlogThreadId",
                        column: x => x.BlogThreadId,
                        principalTable: "BlogThread",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgPath = table.Column<string>(nullable: false),
                    BlogCommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogImage_BlogComment_BlogCommentId",
                        column: x => x.BlogCommentId,
                        principalTable: "BlogComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogLike",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    BlogCommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogLike_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BlogLike_BlogComment_BlogCommentId",
                        column: x => x.BlogCommentId,
                        principalTable: "BlogComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogReply",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogCommentId = table.Column<int>(nullable: false),
                    WhenPosted = table.Column<DateTime>(nullable: false),
                    ReplyText = table.Column<string>(nullable: true),
                    Depth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogReply_BlogComment_BlogCommentId",
                        column: x => x.BlogCommentId,
                        principalTable: "BlogComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_ApplicationUserId",
                table: "BlogComment",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_BlogThreadId",
                table: "BlogComment",
                column: "BlogThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogImage_BlogCommentId",
                table: "BlogImage",
                column: "BlogCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogLike_ApplicationUserId",
                table: "BlogLike",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogLike_BlogCommentId",
                table: "BlogLike",
                column: "BlogCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogReply_BlogCommentId",
                table: "BlogReply",
                column: "BlogCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogThread_ApplicationUserId",
                table: "BlogThread",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogImage");

            migrationBuilder.DropTable(
                name: "BlogLike");

            migrationBuilder.DropTable(
                name: "BlogReply");

            migrationBuilder.DropTable(
                name: "BlogComment");

            migrationBuilder.DropTable(
                name: "BlogThread");
        }
    }
}
