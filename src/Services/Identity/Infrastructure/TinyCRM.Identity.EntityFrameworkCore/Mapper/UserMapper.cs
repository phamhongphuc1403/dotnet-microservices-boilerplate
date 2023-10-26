using AutoMapper;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<ApplicationRefreshToken, RefreshToken>();
        CreateMap<RefreshToken, ApplicationRefreshToken>();

        CreateMap<ApplicationUser, User>();
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()));

        CreateMap<User, UserDto>();
    }
}