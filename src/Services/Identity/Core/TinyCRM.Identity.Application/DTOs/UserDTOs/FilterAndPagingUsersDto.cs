using BuildingBlock.Application.DTOs;
using TinyCRM.Identity.Application.DTOs.UserDTOs.Enums;

namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class FilterAndPagingUsersDto : FilterAndPagingDto<UserSortProperty>
{
    public FilterAndPagingUsersDto()
    {
    }

    protected FilterAndPagingUsersDto(FilterAndPagingUsersDto dto) : base(dto)
    {
    }
}