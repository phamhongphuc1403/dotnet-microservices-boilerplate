using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Utils;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Handlers;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserDomainService _userDomainService;

    public GetUserByIdQueryHandler(IUserDomainService userDomainService, IMapper mapper)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = Optional<User>.Of(await _userDomainService.GetByIdAsync(request.UserId))
            .ThrowIfNotPresent(new UserNotFoundException(request.UserId)).Get();

        return _mapper.Map<UserDto>(user);
    }
}