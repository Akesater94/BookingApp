using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateToBeBookedToLift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateToBeBooked",
                table: "Lifts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateToBeBooked", "Engine", "Name" },
                values: new object[] { new DateOnly(1, 1, 1), "Elektrisk", "Sax lift" });

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateToBeBooked", "Name" },
                values: new object[] { new DateOnly(1, 1, 1), "Bom lift" });

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateToBeBooked", "Engine", "Name" },
                values: new object[] { new DateOnly(1, 1, 1), "Elektrisk", "Bil lift" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateToBeBooked",
                table: "Lifts");

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Engine", "Name" },
                values: new object[] { "Electric", "Scissor lift" });

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Telescopic lift");

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Engine", "Name" },
                values: new object[] { "Electric", "Carmounted lift" });
        }
    }
}
