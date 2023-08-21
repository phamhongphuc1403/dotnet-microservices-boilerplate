using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TinyCRM.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class init_seeds_and_constraints_to_RoleClaims_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "RoleClaims",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "User.View.Personal", "Can view personal user profile", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 2, "User.View.Personal", "Can view personal user profile", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 3, "User.View.All", "Can view all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 4, "User.Update.Personal", "Can update personal user profile", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 5, "User.Update.Personal", "Can update personal user profile", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 6, "User.Update.All", "Can update all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 7, "User.Delete.All", "Can delete all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 8, "User.Create", "Can create user", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 9, "Account.View", "Can view accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 10, "Account.View", "Can view accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 11, "Account.Update", "Can update accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 12, "Account.Delete", "Can delete accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 13, "Account.Create", "Can create accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 14, "Contact.View", "Can view contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 15, "Contact.View", "Can view contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 16, "Contact.Update", "Can update contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 17, "Contact.Delete", "Can delete contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 18, "Contact.Create", "Can create contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 19, "Lead.View", "Can view leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 20, "Lead.View", "Can view leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 21, "Lead.Update", "Can update leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 22, "Lead.Delete", "Can delete leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 23, "Lead.Create", "Can create leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 24, "Deal.View", "Can view deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 25, "Deal.View", "Can view deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 26, "Deal.Update", "Can update deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 27, "Deal.Delete", "Can delete deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 28, "Deal.Create", "Can create deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 29, "Product.View", "Can view products", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 30, "Product.View", "Can view products", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 31, "Product.Update", "Can update products", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 32, "Product.Delete", "Can delete products", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 33, "Product.Create", "Can create products", "80bee362-64ca-42cc-aeb2-444d5f61b008" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1415161f-ae5c-46a2-929a-60edda6cba85", "29bf979f-d7b1-4d7d-b6ac-1a919175925c" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId_ClaimType",
                table: "RoleClaims",
                columns: new[] { "RoleId", "ClaimType" },
                unique: true,
                filter: "[ClaimType] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleClaims_RoleId_ClaimType",
                table: "RoleClaims");

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "RoleClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c8defe23-c7e2-40d0-aae2-b6c1b14f3dca", "d354dab7-835f-46b1-b87a-d50d51f88817" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");
        }
    }
}
