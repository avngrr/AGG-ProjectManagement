using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedDueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01a7d7cd-a324-4691-9fb1-2bdf9b590ab3", "AQAAAAEAACcQAAAAEPDn7YNvTeBXln+Rp/bKnrdZEOGMXUdS2UrCsbAaIbBbqEz+7CyPGxIhKsYk0K+UUw==", "7bd4ebf5-c503-4edd-9851-20000586f338" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a1d1340-9b7a-4856-9adb-40fb69c3c230", "AQAAAAEAACcQAAAAEOmSy/G6YquSPM9ys+2aaHrzR74NB030pMst5nmNc8NT0NoseEj0WDvK7Uc0Hnq8ag==", "5dacef09-6451-428c-ba21-1ec5cfaff567" });
        }
    }
}
