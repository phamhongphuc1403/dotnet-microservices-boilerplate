using BuildingBlock.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected BaseDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Get all the entities that inherit from AuditableEntity
        // and have a state of Added or Modified
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
            if (entityEntry.State == EntityState.Added)
            {
                ((Entity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                ((Entity)entityEntry.Entity).CreatedBy =
                    _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "guest";
            }
            else
            {
                ((Entity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;
                ((Entity)entityEntry.Entity).UpdatedBy =
                    _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "guest";
            }

        return await base.SaveChangesAsync(cancellationToken);
    }
}