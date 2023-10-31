using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;

public class FilterAndPagingUsersQuery : FilterAndPagingUsersDto, IQuery<FilterAndPagingResultDto<UserDto>>
{
    public FilterAndPagingUsersQuery(FilterAndPagingUsersDto dto) : base(dto)
    {
        Sort = dto.ConvertSort();
    }

    public string Sort { get; }
}