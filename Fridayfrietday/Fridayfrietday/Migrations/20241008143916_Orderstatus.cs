using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridayfrietday.Migrations
{
    /// <inheritdoc />
    public partial class Orderstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2024, 10, 8, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9597), "Opgehaald" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2024, 10, 8, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9606), "Opgehaald" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Frikandel Speciaal");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 28, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 10, 3, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9914));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 8, 13, 43, 17, 119, DateTimeKind.Local).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 8, 13, 43, 17, 119, DateTimeKind.Local).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "FrikandelSpeciaal");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 28, 13, 43, 17, 119, DateTimeKind.Local).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 10, 3, 13, 43, 17, 119, DateTimeKind.Local).AddTicks(8104));
        }
    }
}
