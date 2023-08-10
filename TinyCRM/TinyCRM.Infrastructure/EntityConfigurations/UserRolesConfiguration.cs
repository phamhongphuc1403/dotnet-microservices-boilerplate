using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Infrastructure.EntityConfigurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "80bee362-64ca-42cc-aeb2-444d5f61b008",
                UserId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35"
            },
            new IdentityUserRole<string>
            {
                RoleId = "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                UserId = "8d33cc0a-cd85-4546-9c15-bdcf027393b4"
            },
            new IdentityUserRole<string>
            {
                RoleId = "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1",
                UserId = "830112ba-ed9f-4f19-873c-0e31ca3494a9"
            }
        );
    }
}