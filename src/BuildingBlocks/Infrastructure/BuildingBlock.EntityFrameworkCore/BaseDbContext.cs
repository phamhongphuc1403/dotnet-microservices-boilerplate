using BuildingBlock.Application;
using BuildingBlock.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

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
            .Where(e => e.Entity is Entity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in auditedEntities)
            if (entity.State == EntityState.Added)
            {
                ((Entity)entity.Entity).CreatedAt = DateTime.UtcNow;
                ((Entity)entity.Entity).CreatedBy = _currentUser.Email ?? "guest";
            }
            else
            {
                ((Entity)entity.Entity).UpdatedAt = DateTime.UtcNow;
                ((Entity)entity.Entity).UpdatedBy = _currentUser.Email ?? "guest";
            }

        var deletedEntities = ChangeTracker.Entries()
            .Where(e => e.Entity is Entity && e.State == EntityState.Deleted);

        foreach (var entity in deletedEntities)
        {
            entity.State = EntityState.Detached;
            ((Entity)entity.Entity).DeletedAt = DateTime.UtcNow;
            ((Entity)entity.Entity).DeletedBy = _currentUser.Email ?? "guest";
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}