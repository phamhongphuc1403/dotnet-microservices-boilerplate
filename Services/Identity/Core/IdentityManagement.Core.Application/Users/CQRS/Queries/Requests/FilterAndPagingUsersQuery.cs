using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using IdentityManagement.Core.Application.Users.DTOs;

namespace IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;

public record FilterAndPagingUsersQuery(FilterAndPagingUsersDto Dto) : IQuery<FilterAndPagingResultDto<UserDetailDto>>;