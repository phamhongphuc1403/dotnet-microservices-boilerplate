using BuildingBlock.Core.Application;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Infrastructure.EntityFrameworkCore;

public class SaleDbContext : BaseDbContext
{
    public SaleDbContext(DbContextOptions options, ICurrentUser currentUser, IMediator mediator) : base(options,
        currentUser, mediator)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }
}