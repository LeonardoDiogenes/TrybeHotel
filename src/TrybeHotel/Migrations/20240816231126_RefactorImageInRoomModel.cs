using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrybeHotel.Migrations
{
    /// <inheritdoc />
    public partial class RefactorImageInRoomModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 16, 20, 11, 26, 234, DateTimeKind.Local).AddTicks(624), new DateTime(2024, 8, 17, 20, 11, 26, 234, DateTimeKind.Local).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 16, 20, 11, 26, 234, DateTimeKind.Local).AddTicks(668), new DateTime(2024, 8, 17, 20, 11, 26, 234, DateTimeKind.Local).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "Image",
                value: "https://via.placeholder.com/150;https://via.placeholder.com/200");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
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
                values: new object[] { new DateTime(2024, 8, 13, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3593), new DateTime(2024, 8, 14, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3622) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 13, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3628), new DateTime(2024, 8, 14, 19, 49, 17, 511, DateTimeKind.Local).AddTicks(3629) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "Image",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "Image",
                value: "https://via.placeholder.com/150");
        }
    }
}
