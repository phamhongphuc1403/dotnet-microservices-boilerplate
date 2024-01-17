using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Application.Users.IntegrationEvents.Events;
using IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Specifications;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserSummaryDto>
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

    public RegisterCommandHandler(IEventBus eventBus, IMapper mapper, IRoleDomainService roleDomainService,
        IRoleOperationRepository roleOperationRepository, IRoleReadOnlyRepository roleReadOnlyRepository,
        IUnitOfWork unitOfWork, IUserDomainService userDomainService, IUserOperationRepository userOperationRepository,
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        _eventBus = eventBus;
        _mapper = mapper;
        _roleDomainService = roleDomainService;
        _roleOperationRepository = roleOperationRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _userDomainService = userDomainService;
        _userOperationRepository = userOperationRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<UserSummaryDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.CreateAsync(request.Dto.Email, request.Dto.Name, request.Dto.Password,
            request.Dto.ConfirmPassword);

        await _userOperationRepository.CreateAsync(user, request.Dto.Password);

        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification("user");

        var role = Optional<Role>.Of(await _roleReadOnlyRepository.GetAnyAsync(roleNameExactMatchSpecification))
            .ThrowIfNotExist(new RoleNotFoundException("user")).Get();

        await _roleDomainService.AddUserAsync(role, user);

        await _roleOperationRepository.UpdateAsync(role);

        await _unitOfWork.SaveChangesAsync();

        var userIdSpecification = new EntityIdSpecification<User>(user.Id);

        var userCreationDto = await _userReadOnlyRepository.GetAnyAsync<UserCreationDto>(userIdSpecification);

        _eventBus.Publish(new UserCreatedIntegrationEvent(user.Id, user.Name, user.AvatarUrl, user.CoverUrl,
            userCreationDto!.CreatedAt, userCreationDto.CreatedBy));

        return _mapper.Map<UserSummaryDto>(user);
    }
}