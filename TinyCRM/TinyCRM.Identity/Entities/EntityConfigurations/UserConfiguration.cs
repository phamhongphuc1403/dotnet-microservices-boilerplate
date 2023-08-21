using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.Entities.Constants;

namespace TinyCRM.Identity.Entities.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(a => a.Email).IsRequired();
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.PasswordHash).IsRequired();

        builder.HasData(
            new ApplicationUser
            {
                Id = UserId.Admin,
                Name = "Admin",
                Email = "admin@123",
                UserName = "admin@123",
                NormalizedEmail = "ADMIN@123",
                NormalizedUserName = "ADMIN@123",
                PasswordHash = "AQAAAAIAAYagAAAAEJK/8yMYIxM3p+/ja/lTborkwRADLmeAbbjkxRACcShtni4oIXpmEIoKzsvRPrfNtw==",
                ConcurrencyStamp = "1415161f-ae5c-46a2-929a-60edda6cba35",
                SecurityStamp = "29bf979f-d7b1-4d7d-b6ac-1a9191759f5c"
            },
            new ApplicationUser
            {
                Id = UserId.SuperAdmin,
                Name = "Super Admin",
                Email = "superadmin@123",
                UserName = "superadmin@123",
                NormalizedEmail = "SUPERADMIN@123",
                NormalizedUserName = "SUPERADMIN@123",
                PasswordHash = "AQAAAAIAAYagAAAAEJK/8yMYIxM3p+/ja/lTborkwRADLmeAbbjkxRACcShtni4oIXpmEIoKzsvRPrfNtw==",
                ConcurrencyStamp = "acd5f146-82a9-473b-bb8a-f6adfc17f505",
                SecurityStamp = "9c9f10b3-342f-480e-bc5e-0a52619d165b"
            },
            new ApplicationUser
            {
                Id = UserId.User,
                Name = "User",
                Email = "string@123",
                UserName = "string@123",
                NormalizedEmail = "STRING@123",
                NormalizedUserName = "STRING@123",
                PasswordHash = "AQAAAAIAAYagAAAAEJK/8yMYIxM3p+/ja/lTborkwRADLmeAbbjkxRACcShtni4oIXpmEIoKzsvRPrfNtw==",
                ConcurrencyStamp = "3189a9a2-3850-4d90-be84-e327b24fb3e3",
                SecurityStamp = "14475ac9-7417-4974-988f-dfd7d417859b"
            }
        );
    }
}