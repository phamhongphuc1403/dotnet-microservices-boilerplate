using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class ApplyFilterConstraintToIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaims_RoleId_ClaimType",
                table: "RoleClaims");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId_ClaimType",
                table: "RoleClaims",
                columns: new[] { "RoleId", "ClaimType" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaims_RoleId_ClaimType",
                table: "RoleClaims");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId_ClaimType",
                table: "RoleClaims",
                columns: new[] { "RoleId", "ClaimType" },
                unique: true);
        }
    }
}
