using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Specifications;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, User>
{
    private readonly IMapper _mapper;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IRoleOperationRepository _roleOperationRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserOperationRepository userOperationRepository,
        IRoleDomainService roleDomainService, IRoleOperationRepository roleOperationRepository,
        IRoleReadOnlyRepository roleReadOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userOperationRepository = userOperationRepository;
        _roleDomainService = roleDomainService;
        _roleOperationRepository = roleOperationRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _userOperationRepository.CreateAsync(request.User, request.Password);

        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification("user");

        var role = Optional<Role>.Of(await _roleReadOnlyRepository.GetAnyAsync(roleNameExactMatchSpecification))
            .ThrowIfNotExist(new RoleNotFoundException("user")).Get();

        await _roleDomainService.AddUserAsync(role, request.User);

        await _roleOperationRepository.UpdateAsync(role);

        await _unitOfWork.SaveChangesAsync();

        var userIdSpecification = new EntityIdSpecification<User>(request.User.Id);

        var userCreationDto = await _userReadOnlyRepository.GetAnyAsync<UserCreationDto>(userIdSpecification);

        return _mapper.Map(userCreationDto, request.User);
    }
}