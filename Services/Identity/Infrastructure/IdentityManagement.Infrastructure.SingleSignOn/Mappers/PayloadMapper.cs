using AutoMapper;
using Google.Apis.Auth;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.Google.Mappers;

public class PayloadMapper : Profile
{
    public PayloadMapper()
    {
        CreateMap<GoogleJsonWebSignature.Payload, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GivenName + " " + src.FamilyName))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Picture));
    }
}