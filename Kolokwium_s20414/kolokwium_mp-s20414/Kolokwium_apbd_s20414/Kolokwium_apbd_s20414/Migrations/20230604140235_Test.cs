using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium_apbd_s20414.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 4, 16, 2, 35, 399, DateTimeKind.Local).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FulfilledAt" },
                values: new object[] { new DateTime(2023, 6, 4, 16, 2, 35, 399, DateTimeKind.Local).AddTicks(2193), new DateTime(2023, 6, 4, 16, 2, 35, 399, DateTimeKind.Local).AddTicks(2195) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Created");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 4, 15, 31, 20, 470, DateTimeKind.Local).AddTicks(263));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FulfilledAt" },
                values: new object[] { new DateTime(2023, 6, 4, 15, 31, 20, 470, DateTimeKind.Local).AddTicks(310), new DateTime(2023, 6, 4, 15, 31, 20, 470, DateTimeKind.Local).AddTicks(312) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Status 1");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Status 2");
        }
    }
}
