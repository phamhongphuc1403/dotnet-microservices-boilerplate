using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleManagement.Core.Domain.DealAggregate.Entities;

namespace SaleManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations.DealConfigurations;

public class DealLineConfiguration : IEntityTypeConfiguration<DealLine>
{
    public void Configure(EntityTypeBuilder<DealLine> builder)
    {
        builder.Property(dealLine => dealLine.DealId)
            .IsRequired();

        builder.Property(dealLine => dealLine.ProductId)
            .IsRequired();

        builder.Property(dealLine => dealLine.Quantity)
            .IsRequired();

        builder.Property(dealLine => dealLine.PricePerUnit)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(dealLine => dealLine.Deal)
            .WithMany(deal => deal.DealLines)
            .HasForeignKey(dealLine => dealLine.DealId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dealLine => dealLine.Product)
            .WithMany(product => product.DealLines)
            .HasForeignKey(dealLine => dealLine.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("DealLines");
    }
}