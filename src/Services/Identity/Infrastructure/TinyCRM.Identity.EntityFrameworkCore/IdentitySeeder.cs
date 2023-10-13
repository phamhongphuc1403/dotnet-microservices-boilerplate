using BuildingBlock.Application;
using BuildingBlock.Domain.Constants.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public class IdentitySeeder : IDataSeeder
{
    private readonly ILogger<IdentitySeeder> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentitySeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager,
        ILogger<IdentitySeeder> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await SeedUserAsync();
            await SeedRoleAsync();
            await SeedUserRoleAsync();
            await SeedRolePermissionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
        }
    }

    private async Task SeedRolePermissionAsync()
    {
        _logger.LogInformation("Start seeding permission data");

        var (adminRole, userRole) = await GetRoleSeed();

        _logger.LogInformation("removing role claims");

        await RemoveRoleClaims(adminRole);
        await RemoveRoleClaims(userRole);

        _logger.LogInformation("Generating role claims");

        await GenerateRoleClaims(adminRole, userRole);

        _logger.LogInformation("Role claim data seeded successfully!");
    }

    private async Task GenerateRoleClaims(ApplicationRole adminRole, ApplicationRole userRole)
    {
        adminRole.Claims = new List<IdentityRoleClaim<string>>();
        userRole.Claims = new List<IdentityRoleClaim<string>>();

        foreach (var permission in Permissions.PermissionsList)
        {
            var roleClaim = new IdentityRoleClaim<string>
            {
                ClaimType = permission.Type,
                ClaimValue = permission.Value
            };

            if (permission.Type is Permissions.User.ViewAll or Permissions.User.EditAll or Permissions.User.DeleteAll
                or Permissions.User.Create)
                adminRole.Claims.Add(roleClaim);
            else
                userRole.Claims.Add(roleClaim);
        }

        await _roleManager.UpdateAsync(adminRole);
        await _roleManager.UpdateAsync(userRole);
    }

    private async Task RemoveRoleClaims(ApplicationRole role)
    {
        var roleClaims = await _roleManager.GetClaimsAsync(role);

        foreach (var claim in roleClaims) await _roleManager.RemoveClaimAsync(role, claim);
    }

    private async Task SeedUserAsync()
    {
        _logger.LogInformation("Start seeding user data");

        await ThrowIfUserExist();

        var admin = new ApplicationUser
        {
            UserName = "admin@123",
            Email = "admin@123"
        };

        await _userManager.CreateAsync(admin, "Admin@123");

        var user = new ApplicationUser
        {
            UserName = "user@123",
            Email = "user@123"
        };

        await _userManager.CreateAsync(user, "User@123");

        _logger.LogInformation("User data seeded successfully!");
    }

    private async Task ThrowIfUserExist()
    {
        var existingAdmin = await _userManager.FindByEmailAsync("admin@123");

        var existingUser = await _userManager.FindByEmailAsync("user@123");

        if (existingAdmin != null || existingUser != null) throw new Exception("User data already seeded!");
    }

    private async Task SeedRoleAsync()
    {
        _logger.LogInformation("Start seeding role data");

        await ThrowIfRoleExist();

        var adminRole = new ApplicationRole("admin");

        await _roleManager.CreateAsync(adminRole);

        var userRole = new ApplicationRole("user");

        await _roleManager.CreateAsync(userRole);

        _logger.LogInformation("Role data seeded successfully!");
    }

    private async Task ThrowIfRoleExist()
    {
        var existingAdminRole = await _roleManager.FindByNameAsync("admin");

        var existingUserRole = await _roleManager.FindByNameAsync("user");

        if (existingAdminRole != null || existingUserRole != null) throw new Exception("Role data already seeded!");
    }

    private async Task SeedUserRoleAsync()
    {
        _logger.LogInformation("Start seeding user role data");

        var (admin, user) = await GetUserSeed();
        var (adminRole, userRole) = await GetRoleSeed();

        if (await UserRoleExist(admin))
            _logger.LogInformation("admin role data already seeded!");
        else
            await _userManager.AddToRoleAsync(admin, adminRole.Name!);

        if (await UserRoleExist(user))
            _logger.LogInformation("User role data already seeded!");
        else
            await _userManager.AddToRoleAsync(user, userRole.Name!);

        _logger.LogInformation("User role data seeded successfully!");
    }

    private async Task<(ApplicationUser, ApplicationUser)> GetUserSeed()
    {
        var admin = await _userManager.FindByEmailAsync("admin@123");

        var user = await _userManager.FindByEmailAsync("user@123");

        if (admin == null || user == null) throw new Exception("User data not seeded!");

        return (admin, user);
    }

    private async Task<(ApplicationRole, ApplicationRole)> GetRoleSeed()
    {
        var adminRole = await _roleManager.FindByNameAsync("admin");

        var userRole = await _roleManager.FindByNameAsync("user");

        if (adminRole == null || userRole == null) throw new Exception("Role data not seeded!");

        return (adminRole, userRole);
    }

    private async Task<bool> UserRoleExist(ApplicationUser user)
    {
        return (await _userManager.GetRolesAsync(user)).Any();
    }
}