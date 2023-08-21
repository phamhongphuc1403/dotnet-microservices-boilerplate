using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.EntityFrameworkCore.Identity.Entities;

namespace TinyCRM.EntityFrameworkCore.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole
            {
                Id = "80bee362-64ca-42cc-aeb2-444d5f61b008",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new ApplicationRole
            {
                Id = "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                Name = "User",
                NormalizedName = "USER"
            },
            new ApplicationRole
            {
                Id = "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1",
                Name = "Super Administrator",
                NormalizedName = "SUPER ADMINISTRATOR"
            }
        );

        builder.HasMany(r => r.Claims)
            .WithOne()
            .HasForeignKey(r => r.RoleId);
    }
}