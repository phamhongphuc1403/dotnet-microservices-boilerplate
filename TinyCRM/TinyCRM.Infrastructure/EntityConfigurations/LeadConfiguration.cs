using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Domain.Entities.Configurations
{
    public class LeadConfiguration : IEntityTypeConfiguration<LeadEntity>
    {
        public void Configure(EntityTypeBuilder<LeadEntity> builder)
        {
            builder.HasOne(l => l.Customer)
                 .WithMany(c => c.Leads)
                 .HasForeignKey(l => l.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.Status)
                .HasDefaultValue(LeadStatusEnum.Prospect);
        }
    }
}