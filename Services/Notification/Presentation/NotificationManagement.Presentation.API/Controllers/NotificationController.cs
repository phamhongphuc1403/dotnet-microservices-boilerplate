using BuildingBlock.Core.Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.Core.Application.Notifications.CQRS.Commands.Requests;
using NotificationManagement.Core.Application.Notifications.DTOs;
using NotificationManagement.Infrastructure.Firebase.CQRS.Commands.Requests;
using NotificationManagement.Infrastructure.Firebase.DTOs;

namespace NotificationManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Notification.Push)]
    public async Task<ActionResult> SendAsync([FromBody] SendNotificationDto dto)
    {
        await _mediator.Send(new SendNotificationCommand(dto));

        return NoContent();
    }

    [HttpPost("device-tokens")]
    [Authorize]
    public async Task<ActionResult> RegisterDeviceTokenAsync([FromQuery] RegisterDeviceTokenDto dto)
    {
        await _mediator.Send(new RegisterDeviceTokenCommand(dto));

        return NoContent();
    }
}