using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Users.DTOs;

namespace IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserDetailDto>;