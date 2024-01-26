using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using NotificationManagement.Core.Application.Notifications;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Specifications;

namespace NotificationManagement.Infrastructure.Firebase;

public class FirebaseCloudMessagingNotificationService : INotificationService
{
    private readonly IOperationRepository<DeviceToken> _deviceTokenOperationRepository;
    private readonly IReadOnlyRepository<DeviceToken> _deviceTokenReadOnlyRepository;
    private readonly ILogger<FirebaseCloudMessagingNotificationService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public FirebaseCloudMessagingNotificationService(IReadOnlyRepository<DeviceToken> deviceTokenReadOnlyRepository,
        IOperationRepository<DeviceToken> deviceTokenOperationRepository,
        ILogger<FirebaseCloudMessagingNotificationService> logger, IUnitOfWork unitOfWork)
    {
        _deviceTokenReadOnlyRepository = deviceTokenReadOnlyRepository;
        _deviceTokenOperationRepository = deviceTokenOperationRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task SendAsync(Guid userId, NotificationMessage notification)
    {
        var deviceTokens = await GetDeviceTokensAsync(userId);

        await SendAsync(deviceTokens, notification);
    }

    public async Task SendAsync(IEnumerable<Guid> userIds, NotificationMessage notification)
    {
        var deviceTokens = await GetDeviceTokensAsync(userIds);

        await SendAsync(deviceTokens, notification);
    }

    private async Task SendAsync(IReadOnlyList<DeviceToken> deviceTokens, NotificationMessage notification)
    {
        IReadOnlyList<string> tokens = deviceTokens.Select(x => x.Token).ToList();

        var message = new MulticastMessage
        {
            Notification = new Notification
            {
                Title = notification.Title,
                Body = notification.Body
            },
            Tokens = tokens
        };

        var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);

        if (response.FailureCount > 0) await HandleFailureDeviceTokens(response.Responses, deviceTokens);
    }

    private async Task HandleFailureDeviceTokens(IReadOnlyList<SendResponse> responses,
        IReadOnlyList<DeviceToken> deviceTokens)
    {
        var failedTokens = new List<string>();

        for (var i = 0; i < responses.Count; i++)
        {
            if (responses[i].IsSuccess) continue;

            failedTokens.Add(deviceTokens[i].Token);

            _deviceTokenOperationRepository.Delete(deviceTokens[i]);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogError($"List of tokens that caused failures: {string.Join(", ", failedTokens)}");
    }

    private Task<List<DeviceToken>> GetDeviceTokensAsync(Guid userId)
    {
        var deviceTokenUserIdSpecification = new DeviceTokenUserIdSpecification(userId);

        return _deviceTokenReadOnlyRepository.GetAllAsync(deviceTokenUserIdSpecification);
    }

    private Task<List<DeviceToken>> GetDeviceTokensAsync(IEnumerable<Guid> userIds)
    {
        Specification<DeviceToken>? specification = null;

        foreach (var userId in userIds)
        {
            var deviceTokenUserIdSpecification = new DeviceTokenUserIdSpecification(userId);

            specification = specification is null
                ? deviceTokenUserIdSpecification
                : specification.Or(deviceTokenUserIdSpecification);
        }


        return _deviceTokenReadOnlyRepository.GetAllAsync(specification);
    }
}