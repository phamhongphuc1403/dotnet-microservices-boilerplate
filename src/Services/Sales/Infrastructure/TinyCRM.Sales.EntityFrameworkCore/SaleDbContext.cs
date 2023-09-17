using BuildingBlock.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore;

public class SaleDbContext : BaseDbContext
{
    public DbSet<Deal> Deals { get; set; } = null!;
    public DbSet<Lead> Leads { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    public SaleDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }
}