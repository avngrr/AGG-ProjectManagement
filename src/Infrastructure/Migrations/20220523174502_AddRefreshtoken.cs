using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddRefreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caf0ad52-69f8-4a45-b93f-6267fafb9e99", "AQAAAAEAACcQAAAAEForTzOJen3ja7gWMCA3FQdlMXcNdL0StzdtXMDoUSvitnIiaSNztObIDWxNVTBQ2w==", "20ee3375-b6a0-40ec-a33d-f49d83438fdd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf9a60df-bcc3-401c-97bb-3a1be8c1ac4c", "AQAAAAEAACcQAAAAEHcsfmTwB7/+D2wPJG11lhKUtOh47yB9lS0IJWYi5aKpGVSr/bTYXlFBtuVlyEgVEg==", "bc95f36e-90ae-49f6-9b79-6162f352829d" });
        }
    }
}
