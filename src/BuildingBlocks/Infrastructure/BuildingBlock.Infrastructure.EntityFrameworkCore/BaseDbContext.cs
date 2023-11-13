using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using BuildingBlock.Infrastructure.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlock.Infrastructure.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;

    protected BaseDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
            if (typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                builder.SetSoftDeleteFilter(entityType.ClrType);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditedEntities = ChangeTracker.Entries()
            .Where(e => e is
                { Entity: IEntity, State: EntityState.Added or EntityState.Modified or EntityState.Deleted });

        SetAuditProperties(auditedEntities);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditProperties(IEnumerable<EntityEntry> auditedEntities)
    {
        foreach (var auditableEntity in auditedEntities)
        {
            var iEntity = (IEntity)auditableEntity.Entity;
            var utcNow = DateTime.UtcNow;
            var email = _currentUser.Email ?? "guest";

            switch (auditableEntity.State)
            {
                case EntityState.Added:
                    SetCreatedProperties(ref iEntity, utcNow, email);
                    break;
                case EntityState.Modified:
                    SetModifiedProperties(ref iEntity, utcNow, email);
                    break;
                case EntityState.Deleted:
                    SetDeletedProperties(ref iEntity, utcNow, email, auditableEntity);
                    break;
            }
        }
    }

    private static void SetDeletedProperties(ref IEntity iEntity, DateTime utcNow, string email,
        EntityEntry auditableEntity)
    {
        iEntity.DeletedAt ??= utcNow;
        iEntity.DeletedBy ??= email;

        auditableEntity.State = EntityState.Modified;
    }

    private static void SetModifiedProperties(ref IEntity iEntity, DateTime utcNow, string email)
    {
        iEntity.UpdatedAt ??= utcNow;
        iEntity.UpdatedBy ??= email;
    }

    private static void SetCreatedProperties(ref IEntity iEntity, DateTime utcNow, string email)
    {
        if (iEntity.CreatedAt == DateTime.MinValue) iEntity.CreatedAt = utcNow;
        if (string.IsNullOrEmpty(iEntity.CreatedBy)) iEntity.CreatedBy = email;
    }
}