using BuildingBlock.Core.Application;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Infrastructure.EntityFrameworkCore;

public class NotificationDbContext : BaseDbContext
{
    public NotificationDbContext(DbContextOptions options, ICurrentUser currentUser, IMediator mediator) : base(options, currentUser, mediator)
    {
    }

    public DbSet<DeviceToken> DeviceTokens { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}