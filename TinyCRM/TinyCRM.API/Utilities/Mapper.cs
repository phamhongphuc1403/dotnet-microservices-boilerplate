using AutoMapper;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.Account.Model;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Modules.Product.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Utilities
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<AddOrUpdateAccountDTO, AccountEntity>();
            CreateMap<AccountEntity, GetAccountDTO>();

            CreateMap<ContactEntity, GetContactDTO>();
            CreateMap<AddOrUpdateContactDTO, ContactEntity>();

            CreateMap<AddLeadDTO, LeadEntity>();
            CreateMap<UpdateLeadDTO, LeadEntity>();
            CreateMap<LeadEntity, GetLeadDTO>();
            CreateMap<DisqualifyLeadDTO, LeadEntity>();

            CreateMap<AddOrUpdateProductDTO, ProductEntity>();
            CreateMap<ProductEntity, GetProductDTO>();

            CreateMap<DealEntity, GetDealDTO>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Lead.Customer.Name))
                .ForMember(dest => dest.OriginatingLead, opt => opt.MapFrom(src => src.Lead.Title))
                .ForMember(dest => dest.EstimatedRevenue, opt => opt.MapFrom(src => src.Lead.EstimatedRevenue));
            CreateMap<DealEntity, GetAllDealsDTO>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Lead.Customer.Name));
            CreateMap<LeadEntity, DealEntity>()
                .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore());
            CreateMap<UpdateDealDTO, DealEntity>();

            CreateMap<DealProductEntity, GetDealProductDTO>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.PricePerUnit))
                .ForMember(dest => dest.StringId, opt => opt.MapFrom(src => src.Quantity * src.PricePerUnit));
            CreateMap<AddOrUpdateProductToDealDTO, DealProductEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));

        }
    }
}
