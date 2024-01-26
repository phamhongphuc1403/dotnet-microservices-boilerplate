using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using BuildingBlock.Presentation.API.Extensions;
using NotificationManagement.Core.Application.Notifications;
using NotificationManagement.Infrastructure.EntityFrameworkCore;
using NotificationManagement.Infrastructure.Firebase;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Presentation.API.Extensions;

public static class NotificationExtensions
{
    public static IServiceCollection AddNotificationExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcAuthentication(configuration);
        services.AddGrpcAuthorization();

        services.AddFirebase();

        services.AddScoped<INotificationService, FirebaseCloudMessagingNotificationService>();

        services.AddScoped<IReadOnlyRepository<DeviceToken>, ReadOnlyRepository<NotificationDbContext, DeviceToken>>();
        services
            .AddScoped<IOperationRepository<DeviceToken>, OperationRepository<NotificationDbContext, DeviceToken>>();


        return services;
    }
}