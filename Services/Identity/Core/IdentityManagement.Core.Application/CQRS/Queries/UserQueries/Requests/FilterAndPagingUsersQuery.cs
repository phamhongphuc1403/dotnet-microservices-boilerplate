using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using IdentityManagement.Core.Application.DTOs.UserDTOs;

namespace IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Requests;

public record FilterAndPagingUsersQuery(FilterAndPagingUsersDto Dto) : IQuery<FilterAndPagingResultDto<UserDto>>;