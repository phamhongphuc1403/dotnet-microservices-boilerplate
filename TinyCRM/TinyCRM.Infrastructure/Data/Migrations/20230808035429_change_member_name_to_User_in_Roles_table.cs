using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_member_name_to_User_in_Roles_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "MEMBER" });
        }
    }
}
