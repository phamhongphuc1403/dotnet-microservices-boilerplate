using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Identities.EntityFrameworkCore.Entities;
using TinyCRM.Identities.EntityFrameworkCore.EntityConfigurations;

namespace TinyCRM.Identities.EntityFrameworkCore;

public class IdentityAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IdentityAppDbContext(DbContextOptions options) : base(options)
    {
    }

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