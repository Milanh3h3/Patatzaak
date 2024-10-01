using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fridayfrietday.Migrations
{
    /// <inheritdoc />
    public partial class dataseedingV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.AlterColumn<decimal>(
                name: "Stars",
                table: "Reviews",
                type: "decimal(2,1)",
                precision: 2,
                scale: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowsSauces", "CategoryId", "ImageLink", "Name", "Price" },
                values: new object[,]
                {
                    { 4, true, 2, "bitterballen.png", "Bitterballen", 4.0 },
                    { 5, true, 2, "frikandel.png", "Frikandel", 2.0 },
                    { 6, true, 2, "frikandel_xxl.png", "Frikandel XXL", 5.0 },
                    { 7, false, 3, "cola.png", "Cola", 3.0 },
                    { 8, false, 3, "fanta.png", "Fanta", 3.0 },
                    { 9, false, 3, "cola_light.png", "Cola Light", 3.0 },
                    { 10, false, 3, "cola_zero.png", "Cola Zero", 3.0 }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Picture" },
                values: new object[] { "Frieten", "frieten.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Picture",
                value: "snacks.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Picture",
                value: "dranken.png");

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalPrice",
                value: 11.0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalPrice",
                value: 3.5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageLink",
                value: "friet_groot.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowsSauces", "CategoryId", "ImageLink", "Name", "Price" },
                values: new object[] { true, 1, "friet_medium.png", "Friet Medium", 3.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AllowsSauces", "CategoryId", "ImageLink", "Name", "Price" },
                values: new object[] { true, 1, "friet_klein.png", "Friet Klein", 2.5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 21, 12, 7, 40, 238, DateTimeKind.Local).AddTicks(3132));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 9, 26, 12, 7, 40, 238, DateTimeKind.Local).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0.5);

            migrationBuilder.UpdateData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 0.5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.AlterColumn<decimal>(
                name: "Stars",
                table: "Review",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldPrecision: 2,
                oldScale: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Picture" },
                values: new object[] { "Frituren", "frituren.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Picture",
                value: "snacks.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Picture",
                value: "dranken.jpg");

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalPrice",
                value: 9.5);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalPrice",
                value: 6.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageLink",
                value: "friet_groot.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowsSauces", "CategoryId", "ImageLink", "Name", "Price" },
                values: new object[] { false, 2, "bitterballen.jpg", "Bitterballen", 4.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AllowsSauces", "CategoryId", "ImageLink", "Name", "Price" },
                values: new object[] { false, 3, "cola.jpg", "Cola", 2.0 });

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 20, 13, 58, 35, 676, DateTimeKind.Local).AddTicks(1030));

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 9, 25, 13, 58, 35, 676, DateTimeKind.Local).AddTicks(1119));

            migrationBuilder.UpdateData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0.29999999999999999);

            migrationBuilder.UpdateData(
                table: "Sauces",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 0.40000000000000002);
        }
    }
}
