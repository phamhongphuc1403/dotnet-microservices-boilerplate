using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Sales.Domain.AccountAggregate.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore.EntityConfigurations.AccountConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(account => account.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(account => account.Email).IsUnique();
        builder.Property(account => account.Email)
            .IsUnicode(false)
            .HasColumnType("citext")
            .HasMaxLength(320);
    }
}