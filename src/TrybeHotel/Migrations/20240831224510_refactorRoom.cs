using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrybeHotel.Migrations
{
    /// <inheritdoc />
    public partial class refactorRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KingSizeBeds",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SingleSizeBeds",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 31, 19, 45, 10, 155, DateTimeKind.Local).AddTicks(9844), new DateTime(2024, 9, 1, 19, 45, 10, 155, DateTimeKind.Local).AddTicks(9873) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 31, 19, 45, 10, 155, DateTimeKind.Local).AddTicks(9878), new DateTime(2024, 9, 1, 19, 45, 10, 155, DateTimeKind.Local).AddTicks(9879) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                columns: new[] { "KingSizeBeds", "SingleSizeBeds" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                columns: new[] { "KingSizeBeds", "SingleSizeBeds" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KingSizeBeds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "SingleSizeBeds",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 18, 1, 51, 20, 654, DateTimeKind.Local).AddTicks(1106), new DateTime(2024, 8, 19, 1, 51, 20, 654, DateTimeKind.Local).AddTicks(1134) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 18, 1, 51, 20, 654, DateTimeKind.Local).AddTicks(1140), new DateTime(2024, 8, 19, 1, 51, 20, 654, DateTimeKind.Local).AddTicks(1141) });
        }
    }
}
