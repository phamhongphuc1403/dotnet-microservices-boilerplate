using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Sales.Domain.ProductAggregate.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore.EntityConfigurations.ProductConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(a => a.Code).IsUnique();
    }
}