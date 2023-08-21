using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyCRM.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_seeds_in_RoleClaims_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClaimType",
                value: "Permission.User.View.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClaimType",
                value: "Permission.User.View.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimType",
                value: "Permission.User.View.All");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClaimType",
                value: "Permission.User.Update.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClaimType",
                value: "Permission.User.Update.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClaimType",
                value: "Permission.User.Update.All");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClaimType",
                value: "Permission.User.Delete.All");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClaimType",
                value: "Permission.User.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "ClaimType",
                value: "Permission.Account.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "ClaimType",
                value: "Permission.Account.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "ClaimType",
                value: "Permission.Account.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "ClaimType",
                value: "Permission.Account.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "ClaimType",
                value: "Permission.Account.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "ClaimType",
                value: "Permission.Contact.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                column: "ClaimType",
                value: "Permission.Contact.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                column: "ClaimType",
                value: "Permission.Contact.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                column: "ClaimType",
                value: "Permission.Contact.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                column: "ClaimType",
                value: "Permission.Contact.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                column: "ClaimType",
                value: "Permission.Lead.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                column: "ClaimType",
                value: "Permission.Lead.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                column: "ClaimType",
                value: "Permission.Lead.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                column: "ClaimType",
                value: "Permission.Lead.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                column: "ClaimType",
                value: "Permission.Lead.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                column: "ClaimType",
                value: "Permission.Deal.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                column: "ClaimType",
                value: "Permission.Deal.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                column: "ClaimType",
                value: "Permission.Deal.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "ClaimType",
                value: "Permission.Deal.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "ClaimType",
                value: "Permission.Deal.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "ClaimType",
                value: "Permission.Product.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                column: "ClaimType",
                value: "Permission.Product.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                column: "ClaimType",
                value: "Permission.Product.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                column: "ClaimType",
                value: "Permission.Product.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                column: "ClaimType",
                value: "Permission.Product.Create");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClaimType",
                value: "User.View.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClaimType",
                value: "User.View.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimType",
                value: "User.View.All");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClaimType",
                value: "User.Update.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClaimType",
                value: "User.Update.Personal");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClaimType",
                value: "User.Update.All");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClaimType",
                value: "User.Delete.All");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClaimType",
                value: "User.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "ClaimType",
                value: "Account.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "ClaimType",
                value: "Account.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "ClaimType",
                value: "Account.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "ClaimType",
                value: "Account.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "ClaimType",
                value: "Account.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "ClaimType",
                value: "Contact.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                column: "ClaimType",
                value: "Contact.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                column: "ClaimType",
                value: "Contact.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                column: "ClaimType",
                value: "Contact.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                column: "ClaimType",
                value: "Contact.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                column: "ClaimType",
                value: "Lead.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                column: "ClaimType",
                value: "Lead.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                column: "ClaimType",
                value: "Lead.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                column: "ClaimType",
                value: "Lead.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                column: "ClaimType",
                value: "Lead.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                column: "ClaimType",
                value: "Deal.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                column: "ClaimType",
                value: "Deal.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                column: "ClaimType",
                value: "Deal.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "ClaimType",
                value: "Deal.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "ClaimType",
                value: "Deal.Create");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "ClaimType",
                value: "Product.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                column: "ClaimType",
                value: "Product.View");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                column: "ClaimType",
                value: "Product.Update");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                column: "ClaimType",
                value: "Product.Delete");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                column: "ClaimType",
                value: "Product.Create");
        }
    }
}
