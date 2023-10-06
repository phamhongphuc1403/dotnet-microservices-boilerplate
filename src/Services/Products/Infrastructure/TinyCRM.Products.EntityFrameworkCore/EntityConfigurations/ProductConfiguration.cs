using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.EntityFrameworkCore.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(a => a.Code).IsUnique();
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(256)
            .IsUnicode(false);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.Price)
            .IsRequired();
    }
}