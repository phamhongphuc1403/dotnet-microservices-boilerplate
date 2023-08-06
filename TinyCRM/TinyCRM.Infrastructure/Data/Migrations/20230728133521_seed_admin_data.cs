using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_admin_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d28888e9-2ba9-473a-a40f-e38cb54f9b35", 0, "c8defe23-c7e2-40d0-aae2-b6c1b14f3dca", "admin@123", false, false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEOTJb6l8HOHh1wHnpiRDTaZCCyavpjEt27SSXd4toN9W1yY+1fx37d8AhWk3lyYcYg==", null, false, null, "d354dab7-835f-46b1-b87a-d50d51f88817", false, null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "80bee362-64ca-42cc-aeb2-444d5f61b008", "d28888e9-2ba9-473a-a40f-e38cb54f9b35" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "80bee362-64ca-42cc-aeb2-444d5f61b008", "d28888e9-2ba9-473a-a40f-e38cb54f9b35" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35");
        }
    }
}