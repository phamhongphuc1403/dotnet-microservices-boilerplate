using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserOperationRepository _userOperationRepository;

    public CreateUserCommandHandler(IUserDomainService userDomainService, IMapper mapper, IUnitOfWork unitOfWork,
        IUserOperationRepository userOperationRepository)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userOperationRepository = userOperationRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.CreateAsync(request.Dto.Email, request.Dto.Password,
            request.Dto.ConfirmPassword);

        await _userOperationRepository.CreateAsync(user, request.Dto.Password);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }
}