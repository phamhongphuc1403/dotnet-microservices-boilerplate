using AutoMapper;
using SaleManagement.Core.Application.DTOs.DealDTO;
using SaleManagement.Core.Domain.DealAggregate.Entities;

namespace SaleManagement.Infrastructure.EntityFrameworkCore.Profiles;

public class DealProfile : Profile
{
    public DealProfile()
    {
        CreateMap<Deal, DealDto>();
    }
}