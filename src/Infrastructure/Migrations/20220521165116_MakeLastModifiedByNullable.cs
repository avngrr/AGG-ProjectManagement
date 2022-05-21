using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MakeLastModifiedByNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf9a60df-bcc3-401c-97bb-3a1be8c1ac4c", "admin@gmail.com", "Admin", "AQAAAAEAACcQAAAAEHcsfmTwB7/+D2wPJG11lhKUtOh47yB9lS0IJWYi5aKpGVSr/bTYXlFBtuVlyEgVEg==", "bc95f36e-90ae-49f6-9b79-6162f352829d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ee368f2-f9e0-44fa-96e4-0afaeb1689cf", null, null, "AQAAAAEAACcQAAAAENEV/kdm4IQdeEbhQ1V6vzS9vrrvWVQtkCg7SyP9DwxhC3xZcBT8ibA5aFZk1Ylz9Q==", "9e0e2462-371a-41a8-9d96-a8bafa7af174" });
        }
    }
}
