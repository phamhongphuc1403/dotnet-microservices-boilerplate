using AutoMapper;
using TinyCRM.Identities.Domain.Entities;
using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Mapper;

public class IdentityMapper : Profile
{
    public IdentityMapper()
    {
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            // .ForMember(dest => dest.Id, opt => opt.Ignore())
            // .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            ;
        CreateMap<ApplicationUser, User>();
    }
}