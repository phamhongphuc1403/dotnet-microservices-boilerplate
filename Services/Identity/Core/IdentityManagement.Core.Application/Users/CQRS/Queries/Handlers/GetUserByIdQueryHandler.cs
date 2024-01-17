using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Queries.Handlers;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailDto>
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GetUserByIdQueryHandler(IUserReadOnlyRepository userReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<UserDetailDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userIdSpecification = new EntityIdSpecification<User>(request.UserId);

        return Optional<UserDetailDto>.Of(await _userReadOnlyRepository.GetAnyAsync<UserDetailDto>(userIdSpecification))
            .ThrowIfNotExist(new UserNotFoundException(request.UserId)).Get();
    }
}