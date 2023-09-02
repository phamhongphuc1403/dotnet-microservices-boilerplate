using Microsoft.EntityFrameworkCore;
using TinyCRM.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.EntityFrameworkCore;

public class SaleDbContext : BaseAppDbContext
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