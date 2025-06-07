using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIt.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class AddReSignalContentToReSignalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReSignalContent",
                schema: "system",
                table: "PhantomResignals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReSignalContent",
                schema: "system",
                table: "PhantomResignals");
        }
    }
}
