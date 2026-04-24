using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniquePropertyToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_LiftId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LiftId_CustomerId_BookedDate",
                table: "Bookings",
                columns: new[] { "LiftId", "CustomerId", "BookedDate" },
                unique: true,
                filter: "[BookedDate] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_LiftId_CustomerId_BookedDate",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LiftId",
                table: "Bookings",
                column: "LiftId");
        }
    }
}
