using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.HasOne(c => c.Account)
            .WithMany(a => a.Contacts)
            .HasForeignKey(c => c.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.Email).IsUnique();
        builder.HasIndex(c => c.PhoneNumber).IsUnique();
    }
}