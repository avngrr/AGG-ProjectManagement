using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddStatusToTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5109f67-a095-4ce8-844f-911d16b25c73", "AQAAAAEAACcQAAAAEBYsW84eQ6WtOjRTkZThk1Q+bNXZozR8fsWrGhsSKfSHmJVWB5plK76pkQTKsqjP7Q==", "68d4e9aa-c200-425d-8ec9-09cecb72d0d6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caf0ad52-69f8-4a45-b93f-6267fafb9e99", "AQAAAAEAACcQAAAAEForTzOJen3ja7gWMCA3FQdlMXcNdL0StzdtXMDoUSvitnIiaSNztObIDWxNVTBQ2w==", "20ee3375-b6a0-40ec-a33d-f49d83438fdd" });
        }
    }
}
