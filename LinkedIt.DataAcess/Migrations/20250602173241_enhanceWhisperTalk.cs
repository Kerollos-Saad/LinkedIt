using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIt.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class enhanceWhisperTalk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhispersTalks_Users_ReceiverId",
                schema: "system",
                table: "WhispersTalks");

            migrationBuilder.DropForeignKey(
                name: "FK_WhispersTalks_Whispers_WhisperId",
                schema: "system",
                table: "WhispersTalks");

            migrationBuilder.DropIndex(
                name: "IX_WhispersTalks_ReceiverId",
                schema: "system",
                table: "WhispersTalks");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                schema: "system",
                table: "WhispersTalks");

            migrationBuilder.AlterColumn<Guid>(
                name: "WhisperId",
                schema: "system",
                table: "WhispersTalks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WhispersTalks_Whispers_WhisperId",
                schema: "system",
                table: "WhispersTalks",
                column: "WhisperId",
                principalSchema: "system",
                principalTable: "Whispers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhispersTalks_Whispers_WhisperId",
                schema: "system",
                table: "WhispersTalks");

            migrationBuilder.AlterColumn<Guid>(
                name: "WhisperId",
                schema: "system",
                table: "WhispersTalks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                schema: "system",
                table: "WhispersTalks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WhispersTalks_ReceiverId",
                schema: "system",
                table: "WhispersTalks",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_WhispersTalks_Users_ReceiverId",
                schema: "system",
                table: "WhispersTalks",
                column: "ReceiverId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WhispersTalks_Whispers_WhisperId",
                schema: "system",
                table: "WhispersTalks",
                column: "WhisperId",
                principalSchema: "system",
                principalTable: "Whispers",
                principalColumn: "Id");
        }
    }
}
