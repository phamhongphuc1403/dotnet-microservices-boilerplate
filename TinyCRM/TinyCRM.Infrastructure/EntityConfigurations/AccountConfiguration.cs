using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TinyCRM.Domain.Entities.Configurations
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
