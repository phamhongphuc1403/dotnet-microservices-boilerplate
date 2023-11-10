using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations.ProductConfigurations;

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