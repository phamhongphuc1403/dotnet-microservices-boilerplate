using AutoMapper;
using IdentityManagement.Core.Application.DTOs.RoleDTOs;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Mapper;

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