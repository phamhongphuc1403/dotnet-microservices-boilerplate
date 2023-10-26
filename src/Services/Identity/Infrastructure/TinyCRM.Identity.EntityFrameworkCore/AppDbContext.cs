using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>,
    ApplicationUserRole, IdentityUserLogin<Guid>, ApplicationPermission, IdentityUserToken<Guid>>
{
    private readonly ICurrentUser _currentUser;

    public AppDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<ApplicationRefreshToken> ApplicationRefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet")) entityType.SetTableName(tableName[6..]);

            if (typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                builder.SetSoftDeleteFilter(entityType.ClrType);
        }

        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var auditableEntities = ChangeTracker.Entries()
            .Where(e => e is
                { Entity: IEntity, State: EntityState.Added or EntityState.Modified or EntityState.Deleted });

        foreach (var auditableEntity in auditableEntities)
        {
            var iEntity = (IEntity)auditableEntity.Entity;
            var utcNow = DateTime.UtcNow;
            var email = _currentUser.Email ?? "guest";

            switch (auditableEntity.State)
            {
                case EntityState.Added:
                    iEntity.CreatedAt = utcNow;
                    iEntity.CreatedBy = email;
                    break;
                case EntityState.Modified:
                    iEntity.UpdatedAt = utcNow;
                    iEntity.UpdatedBy = email;
                    break;
                case EntityState.Deleted:
                    iEntity.DeletedAt = utcNow;
                    iEntity.DeletedBy = email;

                    auditableEntity.State = EntityState.Modified;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}