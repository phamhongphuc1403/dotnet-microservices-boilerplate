using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Handlers;

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