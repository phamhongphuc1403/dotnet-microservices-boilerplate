using BuildingBlock.Core.Application;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Infrastructure.EntityFrameworkCore;

public class ProductDbContext : BaseDbContext
{
    public ProductDbContext(DbContextOptions options, ICurrentUser currentUser, IMediator mediator) : base(options,
        currentUser, mediator)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}