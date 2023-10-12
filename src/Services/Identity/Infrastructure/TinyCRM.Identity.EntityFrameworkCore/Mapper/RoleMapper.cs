using AutoMapper;
using TinyCRM.Identities.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class RoleMapper : Profile
{
    public RoleMapper()
    {
        CreateMap<ApplicationRole, Role>();
        CreateMap<Role, ApplicationRole>();

        CreateMap<Role, RoleDto>();
    }
}