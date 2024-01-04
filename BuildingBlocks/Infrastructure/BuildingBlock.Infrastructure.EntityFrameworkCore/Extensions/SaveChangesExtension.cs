using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlock.Infrastructure.EntityFrameworkCore.Extensions;

public static class SaveChangesExtension
{
    public static IEnumerable<EntityEntry> SetAuditProperties(this IEnumerable<EntityEntry> auditedEntities,
        ICurrentUser currentUser)
    {
        var auditableEntities = auditedEntities.ToList();

        foreach (var auditableEntity in auditableEntities)
        {
            var iEntity = (IEntity)auditableEntity.Entity;
            var utcNow = DateTime.UtcNow;
            var email = currentUser.Email ?? "guest";

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

        return auditableEntities;
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