using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Entities.Enums;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.EntityConfigurations;

public class LeadConfiguration: IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        // builder.HasOne(l => l.Customer)
        //     .WithMany(c => c.Leads)
        //     .HasForeignKey(l => l.CustomerId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Status)
            .HasDefaultValue(LeadStatuses.Prospect);
    }
}