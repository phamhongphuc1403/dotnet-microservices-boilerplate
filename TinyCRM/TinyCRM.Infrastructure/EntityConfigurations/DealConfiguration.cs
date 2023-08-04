using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Infrastructure.EntityConfigurations
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