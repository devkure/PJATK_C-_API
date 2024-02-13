using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kolokwium_apbd_s20414.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "Doe" },
                    { 2, "Jane", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Product 1", 9.9900000000000002 },
                    { 2, "Product 2", 19.989999999999998 }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Status 1" },
                    { 2, "Status 2" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "Client_ID", "CreatedAt", "FulfilledAt", "Status_ID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 4, 15, 31, 20, 470, DateTimeKind.Local).AddTicks(263), null, 1 },
                    { 2, 2, new DateTime(2023, 6, 4, 15, 31, 20, 470, DateTimeKind.Local).AddTicks(310), new DateTime(2023, 6, 4, 15, 31, 20, 470, DateTimeKind.Local).AddTicks(312), 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductOrders",
                columns: new[] { "Order_ID", "Product_ID", "Amount" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 1, 3 },
                    { 1, 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductOrders",
                keyColumns: new[] { "Order_ID", "Product_ID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOrders",
                keyColumns: new[] { "Order_ID", "Product_ID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOrders",
                keyColumns: new[] { "Order_ID", "Product_ID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
