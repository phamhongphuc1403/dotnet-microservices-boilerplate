using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TinyCRM.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class init_super_admin_and_user_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1", null, "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "80bee362-64ca-42cc-aeb2-444d5f61b008");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.View.Personal", "Can view personal user profile", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.View.All", "Can view all user profiles", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.View.All", "Can view all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.Personal", "Can update personal user profile", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.User.Update.Personal", "Can update personal user profile" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.Personal", "Can update personal user profile", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.All", "Can update all user profiles", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.All", "Can update all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Delete.All", "Can delete all user profiles", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.User.Delete.All", "Can delete all user profiles" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Create", "Can create user", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.User.Create", "Can create user" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.View", "Can view accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Account.View", "Can view accounts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.View", "Can view accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.Update", "Can update accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Account.Update", "Can update accounts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.Delete", "Can delete accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Account.Delete", "Can delete accounts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.Create", "Can create accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Account.Create", "Can create accounts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.View", "Can view contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.View", "Can view contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.View", "Can view contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.Update", "Can update contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Contact.Update", "Can update contacts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.Delete", "Can delete contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.Delete", "Can delete contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.Create", "Can create contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Contact.Create", "Can create contacts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Lead.View", "Can view leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" });

            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 34, "Permission.Lead.View", "Can view leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 35, "Permission.Lead.View", "Can view leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 37, "Permission.Lead.Update", "Can update leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 39, "Permission.Lead.Delete", "Can delete leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 41, "Permission.Lead.Create", "Can create leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 43, "Permission.Deal.View", "Can view deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 44, "Permission.Deal.View", "Can view deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 46, "Permission.Deal.Update", "Can update deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 48, "Permission.Deal.Delete", "Can delete deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 50, "Permission.Deal.Create", "Can create deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 52, "Permission.Product.View", "Can view products", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 53, "Permission.Product.View", "Can view products", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" },
                    { 55, "Permission.Product.Update", "Can update products", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 57, "Permission.Product.Delete", "Can delete products", "80bee362-64ca-42cc-aeb2-444d5f61b008" },
                    { 59, "Permission.Product.Create", "Can create products", "80bee362-64ca-42cc-aeb2-444d5f61b008" }
                });

            

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "830112ba-ed9f-4f19-873c-0e31ca3494a9", 0, "acd5f146-82a9-473b-bb8a-f6adfc17f505", "superadmin@123", false, false, null, "Super Admin", "SUPERADMIN@123", "SUPERADMIN@123", "AQAAAAIAAYagAAAAEJK/8yMYIxM3p+/ja/lTborkwRADLmeAbbjkxRACcShtni4oIXpmEIoKzsvRPrfNtw==", null, false, null, "9c9f10b3-342f-480e-bc5e-0a52619d165b", false, "superadmin@123" },
                    { "8d33cc0a-cd85-4546-9c15-bdcf027393b4", 0, "3189a9a2-3850-4d90-be84-e327b24fb3e3", "string@123", false, false, null, "User", "STRING@123", "STRING@123", "AQAAAAIAAYagAAAAEJK/8yMYIxM3p+/ja/lTborkwRADLmeAbbjkxRACcShtni4oIXpmEIoKzsvRPrfNtw==", null, false, null, "14475ac9-7417-4974-988f-dfd7d417859b", false, "string@123" }
                });

            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 36, "Permission.Lead.Update", "Can update leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 38, "Permission.Lead.Delete", "Can delete leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 40, "Permission.Lead.Create", "Can create leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 42, "Permission.Deal.View", "Can view deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 45, "Permission.Deal.Update", "Can update deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 47, "Permission.Deal.Delete", "Can delete deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 49, "Permission.Deal.Create", "Can create deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 51, "Permission.Product.View", "Can view products", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 54, "Permission.Product.Update", "Can update products", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 56, "Permission.Product.Delete", "Can delete products", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" },
                    { 58, "Permission.Product.Create", "Can create products", "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1", "830112ba-ed9f-4f19-873c-0e31ca3494a9" },
                    { "d8bc22dc-5c2d-41c7-bc22-6293121a1cef", "8d33cc0a-cd85-4546-9c15-bdcf027393b4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1", "830112ba-ed9f-4f19-873c-0e31ca3494a9" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d8bc22dc-5c2d-41c7-bc22-6293121a1cef", "8d33cc0a-cd85-4546-9c15-bdcf027393b4" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "830112ba-ed9f-4f19-873c-0e31ca3494a9");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8d33cc0a-cd85-4546-9c15-bdcf027393b4");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "80bee362-64ca-42cc-aeb2-444d5f61b008");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "d8bc22dc-5c2d-41c7-bc22-6293121a1cef");

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.View.All", "Can view all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.Personal", "Can update personal user profile", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.Personal", "Can update personal user profile", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Update.All", "Can update all user profiles", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.User.Delete.All", "Can delete all user profiles" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.User.Create", "Can create user", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.View", "Can view accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.View", "Can view accounts", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.Update", "Can update accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Account.Delete", "Can delete accounts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Account.Create", "Can create accounts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Contact.View", "Can view contacts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.View", "Can view contacts", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Contact.Update", "Can update contacts" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.Delete", "Can delete contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Contact.Create", "Can create contacts", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Lead.View", "Can view leads" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Lead.View", "Can view leads", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Lead.Update", "Can update leads" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Lead.Delete", "Can delete leads", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Lead.Create", "Can create leads" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Deal.View", "Can view deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Deal.View", "Can view deals", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Deal.Update", "Can update deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Deal.Delete", "Can delete deals", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Deal.Create", "Can create deals" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Product.View", "Can view products", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Product.View", "Can view products", "d8bc22dc-5c2d-41c7-bc22-6293121a1cef" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Product.Update", "Can update products", "80bee362-64ca-42cc-aeb2-444d5f61b008" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission.Product.Delete", "Can delete products" });

            migrationBuilder.UpdateData(
                table: "RoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { "Permission.Product.Create", "Can create products", "80bee362-64ca-42cc-aeb2-444d5f61b008" });
        }
    }
}
