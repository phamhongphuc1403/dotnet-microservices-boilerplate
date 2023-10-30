using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Handlers;

public class
    FilterAndPagingUsersQueryHandler : IQueryHandler<FilterAndPagingUsersQuery, FilterAndPagingResultDto<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;


    public FilterAndPagingUsersQueryHandler(IMapper mapper, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<FilterAndPagingResultDto<UserDto>> Handle(FilterAndPagingUsersQuery request,
        CancellationToken cancellationToken)
    {
        var (users, totalCount) =
            await _userReadOnlyRepository.FilterAndPagingUsers(request.Keyword, request.Sort, request.PageIndex,
                request.PageSize);

        return new FilterAndPagingResultDto<UserDto>(_mapper.Map<IEnumerable<UserDto>>(users),
            request.PageIndex, request.PageSize, totalCount);
    }
}