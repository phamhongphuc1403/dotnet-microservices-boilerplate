using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Infrastructure.Identity.Entities;

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
                UserName = "admin@123",
                NormalizedEmail = "ADMIN@123",
                NormalizedUserName = "ADMIN@123",
                PasswordHash = "AQAAAAIAAYagAAAAEJK/8yMYIxM3p+/ja/lTborkwRADLmeAbbjkxRACcShtni4oIXpmEIoKzsvRPrfNtw==",
                ConcurrencyStamp = "1415161f-ae5c-46a2-929a-60edda6cba35",
                SecurityStamp = "29bf979f-d7b1-4d7d-b6ac-1a9191759f5c"
            });
        }
    }
}