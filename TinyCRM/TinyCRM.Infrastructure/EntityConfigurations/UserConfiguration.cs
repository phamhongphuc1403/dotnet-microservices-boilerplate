using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.PasswordHash).IsRequired();

            builder.HasData(new UserEntity
            {
                Id = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                Name = "Admin",
                Email = "admin@123",
                PasswordHash = "AQAAAAIAAYagAAAAEOTJb6l8HOHh1wHnpiRDTaZCCyavpjEt27SSXd4toN9W1yY+1fx37d8AhWk3lyYcYg=="
            });
        }
    }
}