using BuildingBlock.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TinyCRM.Identities.EntityFrameworkCore.Entities;

namespace TinyCRM.Identities.EntityFrameworkCore;

public class IdentitySeeder : IDataSeeder
{
    private readonly ILogger<IdentitySeeder> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentitySeeder(UserManager<ApplicationUser> userManager, ILogger<IdentitySeeder> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SeedDataAsync()
    {
        if (await _userManager.Users.AnyAsync())
        {
            _logger.LogInformation("Identity data already seeded!");
            return;
        }

        var user = new ApplicationUser
        {
            UserName = "admin@123",
            Email = "admin@123"
        };

        var result = await _userManager.CreateAsync(user, "Admin@123");

        _logger.LogInformation("Identity data seeded successfully!");
    }
}