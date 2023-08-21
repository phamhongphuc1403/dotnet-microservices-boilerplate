using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities;
using TinyCRM.Identity.Entities;
using TinyCRM.Identity.Entities.EntityConfigurations;

namespace TinyCRM.EntityFrameworkCore.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<DealEntity> Deals { get; set; }
    public DbSet<DealProductEntity> DealsProducts { get; set; }
    public DbSet<LeadEntity> Leads { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet")) entityType.SetTableName(tableName.Substring(6));
        }
    }
}