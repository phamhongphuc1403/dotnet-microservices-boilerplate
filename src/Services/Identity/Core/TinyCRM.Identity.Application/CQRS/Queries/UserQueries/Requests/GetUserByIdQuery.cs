using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;