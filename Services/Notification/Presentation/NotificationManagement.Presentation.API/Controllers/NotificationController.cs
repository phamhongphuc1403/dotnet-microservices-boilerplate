using BuildingBlock.Core.Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.Core.Application.Notifications.CQRS.Commands.Requests;
using NotificationManagement.Core.Application.Notifications.DTOs;

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
}