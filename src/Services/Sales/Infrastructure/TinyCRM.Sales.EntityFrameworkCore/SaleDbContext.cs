using BuildingBlock.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Sales.Domain.DealAggregate.Entities;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;
using TinyCRM.Sales.Domain.ProductAggregate.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore;

public class SaleDbContext : BaseDbContext
{
    public SaleDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options,
        httpContextAccessor)
    {
    }

    public DbSet<Deal> Deals { get; set; } = null!;
    public DbSet<Lead> Leads { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }
}