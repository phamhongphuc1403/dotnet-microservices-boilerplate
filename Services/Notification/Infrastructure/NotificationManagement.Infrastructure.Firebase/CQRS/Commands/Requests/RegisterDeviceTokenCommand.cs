using BuildingBlock.Core.Application.CQRS;
using NotificationManagement.Infrastructure.Firebase.DTOs;

namespace NotificationManagement.Infrastructure.Firebase.CQRS.Commands.Requests;

public record RegisterDeviceTokenCommand(RegisterDeviceTokenDto Dto) : ICommand;