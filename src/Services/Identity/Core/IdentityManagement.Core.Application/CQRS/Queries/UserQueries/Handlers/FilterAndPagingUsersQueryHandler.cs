using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Handlers;

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
            await _userReadOnlyRepository.FilterAndPagingUsers(request.Dto.Keyword, request.Dto.Sort,
                request.Dto.PageIndex,
                request.Dto.PageSize);

        return new FilterAndPagingResultDto<UserDto>(_mapper.Map<IEnumerable<UserDto>>(users),
            request.Dto.PageIndex, request.Dto.PageSize, totalCount);
    }
}