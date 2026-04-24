using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceToLift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Lifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 400);

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 300);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Lifts");
        }
    }
}
