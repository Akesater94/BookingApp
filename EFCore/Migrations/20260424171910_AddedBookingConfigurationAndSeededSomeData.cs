using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookingConfigurationAndSeededSomeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookedDate", "CustomerId", "LiftId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 6, 2), 1, 1 },
                    { 2, new DateOnly(2026, 6, 10), 1, 2 },
                    { 3, new DateOnly(2026, 6, 12), 2, 1 },
                    { 4, new DateOnly(2026, 5, 31), 2, 1 },
                    { 5, new DateOnly(2026, 6, 29), 2, 3 },
                    { 6, new DateOnly(2026, 5, 20), 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
