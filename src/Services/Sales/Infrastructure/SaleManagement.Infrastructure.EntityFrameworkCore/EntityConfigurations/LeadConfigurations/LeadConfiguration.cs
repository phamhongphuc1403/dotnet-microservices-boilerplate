using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleManagement.Core.Domain.LeadAggregate.Entities;
using SaleManagement.Core.Domain.LeadAggregate.Entities.Enums;

namespace SaleManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations.LeadConfigurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.Property(lead => lead.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(lead => lead.AccountId)
            .IsRequired();

        builder.Property(lead => lead.Source)
            .IsRequired();

        builder.Property(lead => lead.EstimatedRevenue)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.Property(lead => lead.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(lead => lead.Status)
            .HasDefaultValue(LeadStatus.Prospect)
            .IsRequired();

        builder.Property(lead => lead.DisqualificationReason)
            .IsRequired();

        builder.Property(lead => lead.DisqualificationDescription)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(lead => lead.Account)
            .WithMany(account => account.Leads)
            .HasForeignKey(lead => lead.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}