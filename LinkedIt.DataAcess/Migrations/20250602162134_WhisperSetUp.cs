using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIt.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class WhisperSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SignalUpDate",
                schema: "system",
                table: "PhantomSignalsUps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SignalDownDate",
                schema: "system",
                table: "PhantomSignalsDowns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SignalCommentDate",
                schema: "system",
                table: "PhantomSignalsComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ResignalDate",
                schema: "system",
                table: "PhantomResignals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Whispers",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhisperDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhantomSignalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whispers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Whispers_PhantomSignals_PhantomSignalId",
                        column: x => x.PhantomSignalId,
                        principalSchema: "system",
                        principalTable: "PhantomSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Whispers_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Whispers_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WhispersTalks",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TalkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TalkContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WhisperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhispersTalks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhispersTalks_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WhispersTalks_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WhispersTalks_Whispers_WhisperId",
                        column: x => x.WhisperId,
                        principalSchema: "system",
                        principalTable: "Whispers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_PhantomSignalId",
                schema: "system",
                table: "Whispers",
                column: "PhantomSignalId");

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_ReceiverId",
                schema: "system",
                table: "Whispers",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Whispers_SenderId",
                schema: "system",
                table: "Whispers",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_WhispersTalks_ReceiverId",
                schema: "system",
                table: "WhispersTalks",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_WhispersTalks_SenderId",
                schema: "system",
                table: "WhispersTalks",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_WhispersTalks_WhisperId",
                schema: "system",
                table: "WhispersTalks",
                column: "WhisperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhispersTalks",
                schema: "system");

            migrationBuilder.DropTable(
                name: "Whispers",
                schema: "system");

            migrationBuilder.DropColumn(
                name: "SignalUpDate",
                schema: "system",
                table: "PhantomSignalsUps");

            migrationBuilder.DropColumn(
                name: "SignalDownDate",
                schema: "system",
                table: "PhantomSignalsDowns");

            migrationBuilder.DropColumn(
                name: "SignalCommentDate",
                schema: "system",
                table: "PhantomSignalsComments");

            migrationBuilder.DropColumn(
                name: "ResignalDate",
                schema: "system",
                table: "PhantomResignals");
        }
    }
}
