using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Infrastructure.EntityFrameworkCore.EntitiyConfigurations.DeviceTokenAggregate;

public class DeviceTokenConfiguration : IEntityTypeConfiguration<DeviceToken>
{
    public void Configure(EntityTypeBuilder<DeviceToken> builder)
    {
        builder.HasIndex(deviceToken => new { deviceToken.Token, deviceToken.UserId })
            .IsUnique()
            .HasFilter("\"DeletedAt\" IS NULL");
    }
}