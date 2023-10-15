using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;

public class GetUserByIdQuery : IQuery<UserDto>
{
    public GetUserByIdQuery(Guid id)
    {
        UserId = id;
    }

    public Guid UserId { get; }
}