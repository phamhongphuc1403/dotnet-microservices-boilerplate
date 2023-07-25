using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Domain.Entities.Configurations
{
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
}