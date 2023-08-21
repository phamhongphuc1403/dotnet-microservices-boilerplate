using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Identity.Entities;

namespace TinyCRM.Identity;

public class IdentityMapper : Profile
{
    public IdentityMapper()
    {
        // USER
        CreateMap<UserEntity, GetUserDto>();
        CreateMap<CreateOrEditUserDto, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<UserEntity, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<ApplicationUser, GetUserDto>();
        CreateMap<ApplicationUser, UserEntity>();

        // ROLE
        CreateMap<ApplicationRole, RoleEntity>();

        // PERMISSION
        CreateMap<Claim, PermissionEntity>();
        CreateMap<PermissionEntity, IdentityRoleClaim<string>>()
            .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Value));
    }
}