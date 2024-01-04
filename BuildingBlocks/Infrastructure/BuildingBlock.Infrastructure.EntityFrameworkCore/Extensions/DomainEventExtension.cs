using BuildingBlock.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EntityFrameworkCore.Extensions;

public static class DomainEventExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker.Entries<AggregateRoot>().Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

        domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents) await mediator.Publish(domainEvent);
    }
}