using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Application.Users.IntegrationEvents.Events;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using MediatR;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserSummaryDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public RegisterCommandHandler(IEventBus eventBus, IMapper mapper, IUserDomainService userDomainService,
        IUserReadOnlyRepository userReadOnlyRepository, IMediator mediator)
    {
        _eventBus = eventBus;
        _mapper = mapper;
        _userDomainService = userDomainService;
        _userReadOnlyRepository = userReadOnlyRepository;
        _mediator = mediator;
    }

    public async Task<UserSummaryDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.CreateAsync(request.Dto.Email, request.Dto.Name);

        user = await _mediator.Send(new CreateUserCommand(user, request.Dto.Password), cancellationToken);

        _eventBus.Publish(new UserCreatedIntegrationEvent(user.Id, user.Name, user.AvatarUrl, user.CoverUrl,
            user.CreatedAt, user.CreatedBy));

        return _mapper.Map<UserSummaryDto>(user);
    }
}