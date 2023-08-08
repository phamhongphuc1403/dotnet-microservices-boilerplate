using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_admin_seed_in_Users_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                columns: new[] { "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "ADMIN@123", "ADMIN@123", "admin@123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                columns: new[] { "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { null, null, null });
        }
    }
}
