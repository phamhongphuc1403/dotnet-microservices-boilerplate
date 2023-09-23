using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;
using TinyCRM.Sales.Domain.LeadAggregate.Entities.Enums;

namespace TinyCRM.Sales.EntityFrameworkCore.EntityConfigurations.LeadConfigurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.HasOne(l => l.Account)
            .WithMany(c => c.Leads)
            .HasForeignKey(l => l.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Status)
            .HasDefaultValue(LeadStatus.Prospect);
    }
}