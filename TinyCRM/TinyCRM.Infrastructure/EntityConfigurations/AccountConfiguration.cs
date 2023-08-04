using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasIndex(a => a.Email).IsUnique();
            builder.HasIndex(a => a.PhoneNumber).IsUnique();

            builder.Property(a => a.ToSales)
                .HasDefaultValue(Convert.ToDouble(0));
        }
    }
}