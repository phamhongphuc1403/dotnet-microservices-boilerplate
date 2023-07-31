using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "80bee362-64ca-42cc-aeb2-444d5f61b008",
                    UserId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35"
                }
            );
        }
    }
}