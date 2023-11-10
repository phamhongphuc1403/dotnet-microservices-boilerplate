using AutoMapper;
using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
using Identitymanagement.Core.Domain.UserAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Exceptions;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Handlers;

public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserDto>
{
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GetCurrentUserQueryHandler(ICurrentUser currentUser, IMapper mapper,
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        _currentUser = currentUser;
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = Optional<User>.Of(await _userReadOnlyRepository.GetByIdAsync(_currentUser.Id))
            .ThrowIfNotPresent(new UserNotFoundException(_currentUser.Id)).Get();

        return _mapper.Map<UserDto>(user);
    }
}