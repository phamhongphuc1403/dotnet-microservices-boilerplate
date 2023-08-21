using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TinyCRM.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class init_role_permissions_in_RoleClaims_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 60, "Permission.Role.View", "Can view roles", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 61, "Permission.Role.View", "Can view roles", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 62, "Permission.Role.Update", "Can update roles", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 62);
        }
    }
}
