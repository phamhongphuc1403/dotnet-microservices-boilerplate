using BuildingBlock.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.EntityFrameworkCore;

public class ProductDbContext : BaseDbContext
{
    public ProductDbContext(DbContextOptions options, IHttpContextAccessor iHttpContextAccessor) : base(options,
        iHttpContextAccessor)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}