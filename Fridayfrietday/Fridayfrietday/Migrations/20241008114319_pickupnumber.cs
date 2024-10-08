using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridayfrietday.Migrations
{
    /// <inheritdoc />
    public partial class pickupnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickupNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "PickupNumber" },
                values: new object[] { new DateTime(2024, 10, 8, 13, 43, 17, 119, DateTimeKind.Local).AddTicks(7865), 1 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "PickupNumber" },
                values: new object[] { new DateTime(2024, 10, 8, 13, 43, 17, 119, DateTimeKind.Local).AddTicks(7874), 2 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupNumber",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 7, 12, 29, 19, 636, DateTimeKind.Local).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 7, 12, 29, 19, 636, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 27, 12, 29, 19, 636, DateTimeKind.Local).AddTicks(9246));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 10, 2, 12, 29, 19, 636, DateTimeKind.Local).AddTicks(9253));
        }
    }
}
