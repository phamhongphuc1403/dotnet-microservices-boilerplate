using BuildingBlock.Core.Application;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Infrastructure.EntityFrameworkCore;

public class SaleDbContext : BaseDbContext
{
    public SaleDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options, currentUser)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }
}