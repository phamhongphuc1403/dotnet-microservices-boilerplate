using AutoMapper;
using BuildingBlock.Application;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Utils;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Handlers;

public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserDto>
{
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;
    private readonly IUserDomainService _userDomainService;

    public GetCurrentUserQueryHandler(IUserDomainService userDomainService, ICurrentUser currentUser, IMapper mapper)
    {
        _userDomainService = userDomainService;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = Optional<User>.Of(await _userDomainService.GetByIdAsync(_currentUser.Id))
            .ThrowIfNotPresent(new UserNotFoundException(_currentUser.Id)).Get();

        return _mapper.Map<UserDto>(user);
    }
}