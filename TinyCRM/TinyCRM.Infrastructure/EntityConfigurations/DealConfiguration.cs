using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Domain.Entities.Configurations
{
    internal class DealConfiguration : IEntityTypeConfiguration<DealEntity>
    {
        public void Configure(EntityTypeBuilder<DealEntity> builder)
        {
            builder.HasOne(d => d.Lead)
                 .WithOne(l => l.Deal)
                 .HasForeignKey<DealEntity>(d => d.LeadId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.Status)
               .HasDefaultValue(DealStatusEnum.Open);
        }
    }
}
