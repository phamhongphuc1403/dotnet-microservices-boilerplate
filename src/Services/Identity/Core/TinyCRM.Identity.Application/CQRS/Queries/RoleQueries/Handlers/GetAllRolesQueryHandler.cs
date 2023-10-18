using AutoMapper;
using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Queries.RoleQueries.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices;

namespace TinyCRM.Identity.Application.CQRS.Queries.RoleQueries.Handlers;

public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRoleDomainService _roleDomainService;

    public GetAllRolesQueryHandler(IMapper mapper, IRoleDomainService roleDomainService)
    {
        _mapper = mapper;
        _roleDomainService = roleDomainService;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleDomainService.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}