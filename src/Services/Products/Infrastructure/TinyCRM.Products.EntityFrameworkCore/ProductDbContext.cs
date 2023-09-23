using BuildingBlock.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Products.Domain.Entities;

namespace TinyCRM.Products.EntityFrameworkCore;

public class ProductDbContext : BaseDbContext
{
    public ProductDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}