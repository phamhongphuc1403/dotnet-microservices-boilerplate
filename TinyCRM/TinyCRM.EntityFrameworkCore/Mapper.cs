using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Modules.Account.DTOs;
using TinyCRM.Application.Modules.Contact.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.DealProduct.DTOs;
using TinyCRM.Application.Modules.Lead.DTOs;
using TinyCRM.Application.Modules.Product.DTOs;
using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Identity.Entities;

namespace TinyCRM.EntityFrameworkCore;

public class Mapper : Profile
{
    public Mapper()
    {
        // ACCOUNT
        CreateMap<AddOrUpdateAccountDto, AccountEntity>();
        CreateMap<AccountEntity, GetAccountDto>();

        // CONTACT
        CreateMap<ContactEntity, GetContactDto>();
        CreateMap<AddOrUpdateContactDto, ContactEntity>();

        // LEAD
        CreateMap<AddLeadDto, LeadEntity>();
        CreateMap<UpdateLeadDto, LeadEntity>();
        CreateMap<LeadEntity, GetLeadDto>();
        CreateMap<DisqualifyLeadDto, LeadEntity>();

        // PRODUCT
        CreateMap<AddOrUpdateProductDto, ProductEntity>();
        CreateMap<ProductEntity, GetProductDto>();

        // DEAL
        CreateMap<DealEntity, GetDealDto>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Lead.Customer.Name))
            .ForMember(dest => dest.OriginatingLead, opt => opt.MapFrom(src => src.Lead.Title))
            .ForMember(dest => dest.EstimatedRevenue, opt => opt.MapFrom(src => src.Lead.EstimatedRevenue));
        CreateMap<DealEntity, GetAllDealsDto>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Lead.Customer.Name));
        CreateMap<LeadEntity, DealEntity>()
            .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore());
        CreateMap<UpdateDealDto, DealEntity>();

        // DEAL PRODUCT
        CreateMap<DealProductEntity, GetDealProductDto>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.PricePerUnit))
            .ForMember(dest => dest.StringId, opt => opt.MapFrom(src => src.Quantity * src.PricePerUnit));
        CreateMap<AddOrUpdateProductToDealDto, DealProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
    }
}