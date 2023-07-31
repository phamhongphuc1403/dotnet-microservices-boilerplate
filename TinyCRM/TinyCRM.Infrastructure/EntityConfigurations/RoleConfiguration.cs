using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "80bee362-64ca-42cc-aeb2-444d5f61b008",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }
            );
        }
    }
}