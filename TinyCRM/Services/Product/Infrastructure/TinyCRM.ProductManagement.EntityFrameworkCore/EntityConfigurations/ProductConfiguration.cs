using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.EntityFrameworkCore.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(a => a.Code).IsUnique();
    }
}