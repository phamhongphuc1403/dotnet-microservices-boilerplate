using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Users_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1415161f-ae5c-46a2-929a-60edda6cba35", "29bf979f-d7b1-4d7d-b6ac-1a9191759f5c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1415161f-ae5c-46a2-929a-60edda6cba85", "29bf979f-d7b1-4d7d-b6ac-1a919175925c" });
        }
    }
}
