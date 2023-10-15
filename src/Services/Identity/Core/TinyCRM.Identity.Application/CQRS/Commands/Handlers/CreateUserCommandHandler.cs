using AutoMapper;
using BuildingBlock.Application.CQRS;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserDomainService _userDomainService;

    public CreateUserCommandHandler(IUserDomainService userDomainService, IMapper mapper)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.CreateAsync(request.Email, request.Password);

        return _mapper.Map<UserDto>(user);
    }
}