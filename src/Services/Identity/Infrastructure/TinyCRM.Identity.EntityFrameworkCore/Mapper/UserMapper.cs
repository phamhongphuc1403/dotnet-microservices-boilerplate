using AutoMapper;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<ApplicationUser, User>();
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => 0));

        CreateMap<RefreshToken, ApplicationRefreshToken>()
            .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => src.User));

        CreateMap<User, UserDto>();
    }
}