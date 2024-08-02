using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrybeHotel.Migrations
{
    /// <inheritdoc />
    public partial class AddImgHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Hotels");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 1, 16, 8, 59, 559, DateTimeKind.Local).AddTicks(925), new DateTime(2024, 8, 2, 16, 8, 59, 559, DateTimeKind.Local).AddTicks(951) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut" },
                values: new object[] { new DateTime(2024, 8, 1, 16, 8, 59, 559, DateTimeKind.Local).AddTicks(957), new DateTime(2024, 8, 2, 16, 8, 59, 559, DateTimeKind.Local).AddTicks(958) });
        }
    }
}
