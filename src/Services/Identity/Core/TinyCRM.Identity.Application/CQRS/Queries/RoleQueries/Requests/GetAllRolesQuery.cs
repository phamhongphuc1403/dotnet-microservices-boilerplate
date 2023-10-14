using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace TinyCRM.Identity.Application.CQRS.Queries.RoleQueries.Requests;

public class GetAllRolesQuery : IQuery<IEnumerable<RoleDto>>
{
}