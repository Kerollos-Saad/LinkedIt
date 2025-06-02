using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIt.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class GeneratePhantomSignals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhantomSignals",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhantomFlag = table.Column<bool>(type: "bit", nullable: false),
                    SignalContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpCount = table.Column<int>(type: "int", nullable: false),
                    DownCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    ResignalCount = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomSignals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhantomSignals_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PhantomResignals",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhantomSignalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomResignals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhantomResignals_PhantomSignals_PhantomSignalId",
                        column: x => x.PhantomSignalId,
                        principalSchema: "system",
                        principalTable: "PhantomSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhantomResignals_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PhantomSignalsComments",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhantomSignalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomSignalsComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhantomSignalsComments_PhantomSignals_PhantomSignalId",
                        column: x => x.PhantomSignalId,
                        principalSchema: "system",
                        principalTable: "PhantomSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhantomSignalsComments_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PhantomSignalsDowns",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhantomSignalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomSignalsDowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhantomSignalsDowns_PhantomSignals_PhantomSignalId",
                        column: x => x.PhantomSignalId,
                        principalSchema: "system",
                        principalTable: "PhantomSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhantomSignalsDowns_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PhantomSignalsUps",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhantomSignalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomSignalsUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhantomSignalsUps_PhantomSignals_PhantomSignalId",
                        column: x => x.PhantomSignalId,
                        principalSchema: "system",
                        principalTable: "PhantomSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhantomSignalsUps_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhantomResignals_ApplicationUserId",
                schema: "system",
                table: "PhantomResignals",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomResignals_PhantomSignalId",
                schema: "system",
                table: "PhantomResignals",
                column: "PhantomSignalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignals_ApplicationUserId",
                schema: "system",
                table: "PhantomSignals",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignalsComments_ApplicationUserId",
                schema: "system",
                table: "PhantomSignalsComments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignalsComments_PhantomSignalId",
                schema: "system",
                table: "PhantomSignalsComments",
                column: "PhantomSignalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignalsDowns_ApplicationUserId",
                schema: "system",
                table: "PhantomSignalsDowns",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignalsDowns_PhantomSignalId",
                schema: "system",
                table: "PhantomSignalsDowns",
                column: "PhantomSignalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignalsUps_ApplicationUserId",
                schema: "system",
                table: "PhantomSignalsUps",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhantomSignalsUps_PhantomSignalId",
                schema: "system",
                table: "PhantomSignalsUps",
                column: "PhantomSignalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhantomResignals",
                schema: "system");

            migrationBuilder.DropTable(
                name: "PhantomSignalsComments",
                schema: "system");

            migrationBuilder.DropTable(
                name: "PhantomSignalsDowns",
                schema: "system");

            migrationBuilder.DropTable(
                name: "PhantomSignalsUps",
                schema: "system");

            migrationBuilder.DropTable(
                name: "PhantomSignals",
                schema: "system");
        }
    }
}
