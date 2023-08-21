using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities;

namespace TinyCRM.EntityFrameworkCore.EntityConfigurations;

public class DealProductConfiguration : IEntityTypeConfiguration<DealProductEntity>
{
    public void Configure(EntityTypeBuilder<DealProductEntity> builder)
    {
        builder.HasOne(dp => dp.Deal)
            .WithMany(d => d.DealsProducts)
            .HasForeignKey(d => d.DealId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dp => dp.Product)
            .WithMany(d => d.DealsProducts)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}