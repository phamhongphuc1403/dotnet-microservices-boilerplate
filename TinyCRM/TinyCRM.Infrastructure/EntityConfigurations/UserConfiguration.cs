using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Infrastructure.Identity;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.PasswordHash).IsRequired();

            builder.HasData(new ApplicationUser
            {
                Id = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                Name = "Admin",
                Email = "admin@123",
                PasswordHash = "AQAAAAIAAYagAAAAEOTJb6l8HOHh1wHnpiRDTaZCCyavpjEt27SSXd4toN9W1yY+1fx37d8AhWk3lyYcYg=="
            });
        }
    }
}