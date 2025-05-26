using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIt.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceUserFollowWithUserLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollow",
                schema: "security");

            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.CreateTable(
                name: "UserLink",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinkerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LinkedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLink_Users_LinkedUserId",
                        column: x => x.LinkedUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLink_Users_LinkerUserId",
                        column: x => x.LinkerUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLink_LinkedUserId",
                schema: "system",
                table: "UserLink",
                column: "LinkedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLink_LinkerUserId",
                schema: "system",
                table: "UserLink",
                column: "LinkerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLink",
                schema: "system");

            migrationBuilder.CreateTable(
                name: "UserFollow",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollow_Users_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFollow_Users_FollowerUserId",
                        column: x => x.FollowerUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_FollowedUserId",
                schema: "security",
                table: "UserFollow",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_FollowerUserId",
                schema: "security",
                table: "UserFollow",
                column: "FollowerUserId");
        }
    }
}
