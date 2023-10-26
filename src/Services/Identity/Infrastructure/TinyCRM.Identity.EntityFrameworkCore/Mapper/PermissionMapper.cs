using AutoMapper;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class PermissionMapper : Profile
{
    public PermissionMapper()
    {
        CreateMap<ApplicationPermission, Permission>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClaimType))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ClaimValue));

        CreateMap<Permission, ApplicationPermission>()
            .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Description));
    }
}