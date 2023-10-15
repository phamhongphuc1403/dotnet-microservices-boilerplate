using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Handlers;

public class
    FilterAndPagingUsersQueryHandler : IQueryHandler<FilterAndPagingUsersQuery, FilterAndPagingResultDto<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserDomainService _userDomainService;

    public FilterAndPagingUsersQueryHandler(IUserDomainService userDomainService, IMapper mapper)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<UserDto>> Handle(FilterAndPagingUsersQuery request,
        CancellationToken cancellationToken)
    {
        var (users, totalCount) =
            await _userDomainService.FilterAndPagingUsers(request.Keyword, request.Sort, request.PageIndex,
                request.PageSize);

        return new FilterAndPagingResultDto<UserDto>(_mapper.Map<IEnumerable<UserDto>>(users),
            request.PageIndex, request.PageSize, totalCount);
    }
}