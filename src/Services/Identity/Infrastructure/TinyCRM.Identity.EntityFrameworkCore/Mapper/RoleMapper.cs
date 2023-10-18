using AutoMapper;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Identity.RoleAggregate.Entities;

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