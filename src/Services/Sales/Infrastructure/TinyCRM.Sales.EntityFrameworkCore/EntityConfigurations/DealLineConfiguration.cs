using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore.EntityConfigurations;

public class DealLineConfiguration: IEntityTypeConfiguration<DealLine>
{
    public void Configure(EntityTypeBuilder<DealLine> builder)
    {
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