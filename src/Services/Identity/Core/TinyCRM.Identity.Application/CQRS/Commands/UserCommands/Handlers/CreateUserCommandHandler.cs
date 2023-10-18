using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Handlers;

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
        if (request.Password != request.ConfirmPassword)
            throw new ValidationException("Password and confirmation password do not match");

        var user = await _userDomainService.CreateAsync(request.Email, request.Password);

        return _mapper.Map<UserDto>(user);
    }
}