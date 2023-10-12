using AutoMapper;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            // .ForMember(dest => dest.Id, opt => opt.Ignore())
            // .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            ;
        CreateMap<ApplicationUser, User>();
    }
}