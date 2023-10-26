using AutoMapper;
using BuildingBlock.Application;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Shared.Utils;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Handlers;

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