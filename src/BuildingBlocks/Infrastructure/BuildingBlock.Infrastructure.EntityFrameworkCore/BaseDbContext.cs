using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;

    protected BaseDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditedEntities = ChangeTracker.Entries()
            .Where(e => e is
                { Entity: IEntity, State: EntityState.Added or EntityState.Modified or EntityState.Deleted });

        foreach (var auditableEntity in auditedEntities)
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