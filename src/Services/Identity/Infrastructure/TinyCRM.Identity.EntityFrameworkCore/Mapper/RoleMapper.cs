using AutoMapper;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class RoleMapper : Profile
{
    public RoleMapper()
    {
        CreateMap<ApplicationRole, Role>();
        CreateMap<Role, ApplicationRole>()
            .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()))
            .ForMember(dest => dest.Permissions, opt => opt.Ignore());

        CreateMap<Role, RoleDto>();

        CreateMap<ApplicationUserRole, UserRole>();
        CreateMap<UserRole, ApplicationUserRole>();
    }
}