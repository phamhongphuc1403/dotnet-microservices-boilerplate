using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;

public record FilterAndPagingUsersQuery(FilterAndPagingUsersDto Dto) : IQuery<FilterAndPagingResultDto<UserDto>>;