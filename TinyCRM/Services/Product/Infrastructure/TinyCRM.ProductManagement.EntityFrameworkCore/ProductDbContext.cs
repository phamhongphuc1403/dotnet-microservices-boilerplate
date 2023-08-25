using Microsoft.EntityFrameworkCore;
using TinyCRM.EntityFrameworkCore;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.EntityFrameworkCore;

public class ProductDbContext : BaseAppDbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}