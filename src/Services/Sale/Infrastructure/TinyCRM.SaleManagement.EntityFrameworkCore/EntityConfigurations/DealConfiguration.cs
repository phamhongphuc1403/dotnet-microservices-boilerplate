using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Entities.Enums;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.EntityConfigurations;

public class DealConfiguration : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
        builder.HasOne(d => d.Lead)
            .WithOne(l => l.Deal)
            .HasForeignKey<Deal>(d => d.LeadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(d => d.Status)
            .HasDefaultValue(DealStatuses.Open);
    }
}