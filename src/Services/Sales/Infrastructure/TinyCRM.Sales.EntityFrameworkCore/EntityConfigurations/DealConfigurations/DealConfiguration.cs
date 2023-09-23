using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Sales.Domain.DealAggregate.Entities;
using TinyCRM.Sales.Domain.DealAggregate.Entities.Enums;

namespace TinyCRM.Sales.EntityFrameworkCore.EntityConfigurations.DealConfigurations;

public class DealConfiguration : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
        builder.Property(deal => deal.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(deal => deal.LeadId)
            .IsRequired();

        builder.Property(deal => deal.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(deal => deal.Status)
            .IsRequired()
            .HasDefaultValue(DealStatus.Open);

        builder.HasOne(deal => deal.Lead)
            .WithOne(deal => deal.Deal)
            .HasForeignKey<Deal>(deal => deal.LeadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}