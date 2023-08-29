using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyCRM.ProductManagement.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class RenameStringIdColumnToCodeInProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StringId",
                table: "Products",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Products_StringId",
                table: "Products",
                newName: "IX_Products_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Products",
                newName: "StringId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Code",
                table: "Products",
                newName: "IX_Products_StringId");
        }
    }
}
