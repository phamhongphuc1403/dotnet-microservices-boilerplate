using BuildingBlock.Core.Application.CQRS;
using NotificationManagement.Core.Application.Notifications.DTOs;

namespace NotificationManagement.Core.Application.Notifications.CQRS.Commands.Requests;

public record SendNotificationCommand(SendNotificationDto Dto) : ICommand;