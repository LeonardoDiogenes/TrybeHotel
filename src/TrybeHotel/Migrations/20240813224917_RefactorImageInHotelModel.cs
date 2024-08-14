using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrybeHotel.Migrations
{
    /// <inheritdoc />
    public partial class RefactorImageInHotelModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 13, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3593), new DateTime(2024, 8, 14, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3622) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 13, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3628), new DateTime(2024, 8, 14, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3629) });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1,
                column: "Image",
                value: "https://via.placeholder.com/150;https://via.placeholder.com/200");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2,
                column: "Image",
                value: "https://via.placeholder.com/150;https://via.placeholder.com/200");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 2, 0, 11, 47, 797, DateTimeKind.Local).AddTicks(4323), new DateTime(2024, 8, 3, 0, 11, 47, 797, DateTimeKind.Local).AddTicks(4351) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 2, 0, 11, 47, 797, DateTimeKind.Local).AddTicks(4357), new DateTime(2024, 8, 3, 0, 11, 47, 797, DateTimeKind.Local).AddTicks(4358) });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1,
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2,
                column: "Image",
                value: null);
        }
    }
}
