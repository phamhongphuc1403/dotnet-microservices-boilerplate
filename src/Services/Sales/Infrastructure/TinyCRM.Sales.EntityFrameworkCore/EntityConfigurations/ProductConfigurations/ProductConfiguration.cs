using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Sales.Domain.ProductAggregate.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore.EntityConfigurations.ProductConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(product => product.Code).IsUnique();
        builder.Property(product => product.Code)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnType("citext");
    }
}