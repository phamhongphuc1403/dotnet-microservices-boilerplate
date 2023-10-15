using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;

public class FilterAndPagingUsersQuery : FilterAndPagingUsersDto, IQuery<FilterAndPagingResultDto<UserDto>>
{
    public FilterAndPagingUsersQuery(FilterAndPagingUsersDto dto)
    {
        Keyword = dto.Keyword;
        PageSize = dto.PageSize;
        PageIndex = dto.PageIndex;
        Sort = dto.ConvertSort();
    }

    public string Sort { get; }
}