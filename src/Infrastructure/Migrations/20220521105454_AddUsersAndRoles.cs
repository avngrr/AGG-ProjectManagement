using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16cc9f16-3765-45b0-ba3d-eb5cecd51fed", "3", "Basic", "Basic" },
                    { "50df68f2-75fc-4773-aec0-e1b7fd0749ff", "2", "Developer", "Developer" },
                    { "f073ed4d-6b92-411d-bcca-1911b4ccd365", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastModifiedBy", "LastModifiedOn", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0b700e63-780c-488a-bd56-de61403d5a0f", 0, "4ee368f2-f9e0-44fa-96e4-0afaeb1689cf", null, null, "admin@gmail.com", true, null, true, null, null, null, false, null, null, null, "AQAAAAEAACcQAAAAENEV/kdm4IQdeEbhQ1V6vzS9vrrvWVQtkCg7SyP9DwxhC3xZcBT8ibA5aFZk1Ylz9Q==", null, false, "9e0e2462-371a-41a8-9d96-a8bafa7af174", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 13, "Permission", "Permissions.Users.Create", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 14, "Permission", "Permissions.Users.View", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 15, "Permission", "Permissions.Users.Edit", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 16, "Permission", "Permissions.Users.Delete", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 17, "Permission", "Permissions.Roles.Create", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 18, "Permission", "Permissions.Roles.View", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 19, "Permission", "Permissions.Roles.Edit", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 20, "Permission", "Permissions.Roles.Delete", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 21, "Permission", "Permissions.Projects.Create", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 22, "Permission", "Permissions.Projects.View", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 23, "Permission", "Permissions.Projects.Edit", "f073ed4d-6b92-411d-bcca-1911b4ccd365" },
                    { 24, "Permission", "Permissions.Projects.Delete", "f073ed4d-6b92-411d-bcca-1911b4ccd365" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f073ed4d-6b92-411d-bcca-1911b4ccd365", "0b700e63-780c-488a-bd56-de61403d5a0f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16cc9f16-3765-45b0-ba3d-eb5cecd51fed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50df68f2-75fc-4773-aec0-e1b7fd0749ff");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f073ed4d-6b92-411d-bcca-1911b4ccd365", "0b700e63-780c-488a-bd56-de61403d5a0f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f073ed4d-6b92-411d-bcca-1911b4ccd365");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b700e63-780c-488a-bd56-de61403d5a0f");
        }
    }
}
