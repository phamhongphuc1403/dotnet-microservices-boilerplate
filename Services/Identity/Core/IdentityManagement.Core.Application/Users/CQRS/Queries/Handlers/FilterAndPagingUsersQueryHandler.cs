using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Specifications;

namespace IdentityManagement.Core.Application.Users.CQRS.Queries.Handlers;

public class
    FilterAndPagingUsersQueryHandler : IQueryHandler<FilterAndPagingUsersQuery, FilterAndPagingResultDto<UserDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public FilterAndPagingUsersQueryHandler(IMapper mapper, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<FilterAndPagingResultDto<UserDetailDto>> Handle(FilterAndPagingUsersQuery request,
        CancellationToken cancellationToken)
    {
        var userNamePartialMatchSpecification = new UserNamePartialMatchSpecification(request.Dto.Keyword);

        var userEmailPartialMatchSpecification = new UserEmailPartialMatchSpecification(request.Dto.Keyword);

        var specification = userEmailPartialMatchSpecification.Or(userNamePartialMatchSpecification);

        var (users, totalCount) = await _userReadOnlyRepository.GetFilterAndPagingAsync(specification, request.Dto.Sort,
            request.Dto.PageIndex, request.Dto.PageSize, null, true);

        return new FilterAndPagingResultDto<UserDetailDto>(_mapper.Map<IEnumerable<UserDetailDto>>(users),
            request.Dto.PageIndex, request.Dto.PageSize, totalCount);
    }
}