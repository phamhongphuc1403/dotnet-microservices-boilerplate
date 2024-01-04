using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Application.Users.IntegrationEvents.Events;
using IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDetailDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IRoleOperationRepository _roleOperationRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public CreateUserCommandHandler(IUserDomainService userDomainService, IMapper mapper, IUnitOfWork unitOfWork,
        IUserOperationRepository userOperationRepository, IRoleDomainService roleDomainService,
        IRoleOperationRepository roleOperationRepository, IRoleReadOnlyRepository roleReadOnlyRepository,
        IEventBus eventBus, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userOperationRepository = userOperationRepository;
        _roleDomainService = roleDomainService;
        _roleOperationRepository = roleOperationRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _eventBus = eventBus;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<UserDetailDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.CreateAsync(request.Dto.Email, request.Dto.Name, request.Dto.Password,
            request.Dto.ConfirmPassword);

        await _userOperationRepository.CreateAsync(user, request.Dto.Password);

        var role = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("user"))
            .ThrowIfNotExist(new RoleNotFoundException("user")).Get();

        await _roleDomainService.AddUserAsync(role, user);

        await _roleOperationRepository.UpdateAsync(role);

        await _unitOfWork.SaveChangesAsync();

        var userCreationDto = await _userReadOnlyRepository.GetByIdAsync<UserCreationDto>(user.Id);

        _eventBus.Publish(new UserCreatedIntegrationEvent(user.Id, user.Name, user.AvatarUrl, user.CoverUrl,
            userCreationDto!.CreatedAt, userCreationDto.CreatedBy));

        var userDetailDto = _mapper.Map<UserDetailDto>(user);

        _mapper.Map(userCreationDto, userDetailDto);

        return userDetailDto;
    }
}