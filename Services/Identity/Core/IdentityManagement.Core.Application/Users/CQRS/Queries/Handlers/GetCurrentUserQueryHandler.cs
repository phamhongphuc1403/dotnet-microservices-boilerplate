using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Queries.Handlers;

public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserSummaryDto>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GetCurrentUserQueryHandler(ICurrentUser currentUser, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _currentUser = currentUser;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<UserSummaryDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userIdSpecification = new EntityIdSpecification<User>(_currentUser.Id);

        return Optional<UserSummaryDto>
            .Of(await _userReadOnlyRepository.GetAnyAsync<UserSummaryDto>(userIdSpecification))
            .ThrowIfNotExist(new UserNotFoundException(_currentUser.Id)).Get();
    }
}