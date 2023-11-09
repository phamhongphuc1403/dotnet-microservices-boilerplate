using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.DTOs.UserDTOs;

namespace IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Requests;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;