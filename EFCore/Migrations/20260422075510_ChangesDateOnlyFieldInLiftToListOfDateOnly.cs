using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ChangesDateOnlyFieldInLiftToListOfDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateToBeBooked",
                table: "Lifts");

            migrationBuilder.AddColumn<string>(
                name: "DatesToBeBooked",
                table: "Lifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatesToBeBooked",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DatesToBeBooked",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatesToBeBooked",
                value: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatesToBeBooked",
                table: "Lifts");

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
                column: "DateToBeBooked",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateToBeBooked",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateToBeBooked",
                value: new DateOnly(1, 1, 1));
        }
    }
}
