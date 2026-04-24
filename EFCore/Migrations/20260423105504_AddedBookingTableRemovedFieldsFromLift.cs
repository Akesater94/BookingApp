using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookingTableRemovedFieldsFromLift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booked",
                table: "Lifts");

            migrationBuilder.DropColumn(
                name: "DatesToBeBooked",
                table: "Lifts");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LiftId = table.Column<int>(type: "int", nullable: false),
                    BookedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Lifts_LiftId",
                        column: x => x.LiftId,
                        principalTable: "Lifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LiftId",
                table: "Bookings",
                column: "LiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "Booked",
                table: "Lifts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DatesToBeBooked",
                table: "Lifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Booked", "DatesToBeBooked" },
                values: new object[] { false, "[]" });

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Booked", "DatesToBeBooked" },
                values: new object[] { false, "[]" });

            migrationBuilder.UpdateData(
                table: "Lifts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Booked", "DatesToBeBooked" },
                values: new object[] { false, "[]" });
        }
    }
}
