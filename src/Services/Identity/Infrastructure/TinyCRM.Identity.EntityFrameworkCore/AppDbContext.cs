using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;
using TinyCRM.Identity.Identity.RoleAggregate.Entities;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApplicationRefreshToken> ApplicationRefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet")) entityType.SetTableName(tableName[6..]);
        }

        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}