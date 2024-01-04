using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using BuildingBlock.Infrastructure.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;
    private readonly IMediator _mediator;

    protected BaseDbContext(DbContextOptions options, ICurrentUser currentUser, IMediator mediator) : base(options)
    {
        _currentUser = currentUser;
        _mediator = mediator;
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
        await _mediator.DispatchDomainEventsAsync(this);

        var auditedEntities = ChangeTracker.Entries()
            .Where(e => e is
                { Entity: IEntity, State: EntityState.Added or EntityState.Modified or EntityState.Deleted });

        auditedEntities.SetAuditProperties(_currentUser);

        return await base.SaveChangesAsync(cancellationToken);
    }
}