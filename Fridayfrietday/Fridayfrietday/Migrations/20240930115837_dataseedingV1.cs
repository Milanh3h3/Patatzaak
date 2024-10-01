using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fridayfrietday.Migrations
{
    /// <inheritdoc />
    public partial class dataseedingV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Stars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Picture" },
                values: new object[,]
                {
                    { 1, "Frituren", "frituren.jpg" },
                    { 2, "Snacks", "snacks.jpg" },
                    { 3, "Dranken", "dranken.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email" },
                values: new object[,]
                {
                    { 1, "customer1@example.com" },
                    { 2, "customer2@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Date", "Description", "Name", "Stars" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 20, 13, 58, 35, 676, DateTimeKind.Local).AddTicks(1030), "Heerlijke frietjes!", "John Doe", 4.5m },
                    { 2, new DateTime(2024, 9, 25, 13, 58, 35, 676, DateTimeKind.Local).AddTicks(1119), "Snacks waren goed, maar had liever meer saus.", "Jane Smith", 3.5m }
                });

            migrationBuilder.InsertData(
                table: "Sauces",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Mayonaise", 0.5 },
                    { 2, "Ketchup", 0.29999999999999999 },
                    { 3, "Curry", 0.40000000000000002 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 1, 9.5 },
                    { 2, 2, 6.0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowsSauces", "CategoryId", "ImageLink", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, 1, "friet_groot.jpg", "Friet Groot", 3.5 },
                    { 2, false, 2, "bitterballen.jpg", "Bitterballen", 4.0 },
                    { 3, false, 3, "cola.jpg", "Cola", 2.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 1, 3, 1 },
                    { 3, 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetailSauces",
                columns: new[] { "Id", "OrderDetailId", "SauceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DeleteData(
                table: "OrderDetailSauces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetailSauces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
