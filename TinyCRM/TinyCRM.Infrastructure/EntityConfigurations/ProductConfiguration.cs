using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasIndex(a => a.StringId).IsUnique();
        }
    }
}
