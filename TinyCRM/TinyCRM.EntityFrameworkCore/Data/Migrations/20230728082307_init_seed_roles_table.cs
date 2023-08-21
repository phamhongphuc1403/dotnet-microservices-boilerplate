using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TinyCRM.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class init_seed_roles_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "80bee362-64ca-42cc-aeb2-444d5f61b008", null, "Administrator", "ADMINISTRATOR" },
                    { "d8bc22dc-5c2d-41c7-bc22-6293121a1cef", null, "User", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "80bee362-64ca-42cc-aeb2-444d5f61b008");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d8bc22dc-5c2d-41c7-bc22-6293121a1cef");
        }
    }
}