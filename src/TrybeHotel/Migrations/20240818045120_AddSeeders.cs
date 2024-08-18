using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrybeHotel.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1,
                columns: new[] { "Address", "Image", "Name" },
                values: new object[] { "Largo do Arouche, 150", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/17623873.jpg?k=b952cb80dc3ecc4df3b662e9544c8ef0a2cfd480c92d3654d86c9254d7bcff4e&o=&hp=1;https://cf.bstatic.com/xdata/images/hotel/max1024x768/44459472.jpg?k=072ac9d3e1405cd3e8a922cddf710f955c912da68ac3433acfed8f73adc9c513&o=&hp=1", "San Raphael Hotel" });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2,
                columns: new[] { "Address", "Image", "Name" },
                values: new object[] { "Rua Senador Dantas, 25", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/5139993.jpg?k=19d18100c9cfd4fce562972ace815dbe9e6b91d88d400d10be3510a64ec616e8&o=&hp=1;https://cf.bstatic.com/xdata/images/hotel/max1024x768/46891345.jpg?k=b65862e317f0e83af7026b60d73c2b52925a098f1bfdbc2d4a4b5f24a0fff585&o=&hp=1", "Hotel Atlântico Business Centro" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "Image",
                value: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/360677173.jpg?k=c1083d43ddb2d9415b25304a6f7cd78100e7c737eaacd33273f67b3c904c4a5c&o=&hp=1;https://cf.bstatic.com/xdata/images/hotel/max1024x768/266289735.jpg?k=291d57f71acd1295c410553c4d052f0e299c0f2f48bb83595cf7bf2b97238513&o=&hp=1");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "Image",
                value: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/46891345.jpg?k=b65862e317f0e83af7026b60d73c2b52925a098f1bfdbc2d4a4b5f24a0fff585&o=&hp=1;https://cf.bstatic.com/xdata/images/hotel/max1024x768/46903249.jpg?k=f8fb07eb763acad38a4804b58bc05f3dc285d89338d1d6ac02b31e6f35bd5a1e&o=&hp=1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1,
                columns: new[] { "Address", "Image", "Name" },
                values: new object[] { "Endereço 1", "https://via.placeholder.com/150;https://via.placeholder.com/200", "Hotel 1" });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2,
                columns: new[] { "Address", "Image", "Name" },
                values: new object[] { "Endereço 2", "https://via.placeholder.com/150;https://via.placeholder.com/200", "Hotel 2" });

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
    }
}
