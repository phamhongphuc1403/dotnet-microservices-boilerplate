using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using BuildingBlock.Infrastructure.EntityFrameworkCore.Extensions;
using IdentityManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore;

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