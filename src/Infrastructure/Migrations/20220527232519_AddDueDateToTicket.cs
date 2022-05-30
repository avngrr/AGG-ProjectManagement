using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDueDateToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a1d1340-9b7a-4856-9adb-40fb69c3c230", "AQAAAAEAACcQAAAAEOmSy/G6YquSPM9ys+2aaHrzR74NB030pMst5nmNc8NT0NoseEj0WDvK7Uc0Hnq8ag==", "5dacef09-6451-428c-ba21-1ec5cfaff567" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5109f67-a095-4ce8-844f-911d16b25c73", "AQAAAAEAACcQAAAAEBYsW84eQ6WtOjRTkZThk1Q+bNXZozR8fsWrGhsSKfSHmJVWB5plK76pkQTKsqjP7Q==", "68d4e9aa-c200-425d-8ec9-09cecb72d0d6" });
        }
    }
}
