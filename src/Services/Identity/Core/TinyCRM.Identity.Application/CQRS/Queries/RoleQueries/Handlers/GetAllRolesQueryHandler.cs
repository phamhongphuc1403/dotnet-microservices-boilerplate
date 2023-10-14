using AutoMapper;
using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Queries.RoleQueries.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace TinyCRM.Identity.Application.CQRS.Queries.RoleQueries.Handlers;

public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public GetAllRolesQueryHandler(IMapper mapper, IRoleService roleService)
    {
        _mapper = mapper;
        _roleService = roleService;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}