using BuildingBlock.Core.Application.CQRS;
using NotificationManagement.Core.Application.Notifications.CQRS.Commands.Requests;

namespace NotificationManagement.Core.Application.Notifications.CQRS.Commands.Handlers;

public class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    private readonly INotificationService _notificationService;

    public SendNotificationCommandHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        await _notificationService.SendAsync(request.Dto.UserIds, request.Dto.Message);
    }
}