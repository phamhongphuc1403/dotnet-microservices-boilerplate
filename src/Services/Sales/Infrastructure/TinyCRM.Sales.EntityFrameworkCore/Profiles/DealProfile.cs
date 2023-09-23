using AutoMapper;
using TinyCRM.Sales.Application.DTOs.DealDTO;
using TinyCRM.Sales.Domain.DealAggregate.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore.Profiles;

public class DealProfile : Profile
{
    public DealProfile()
    {
        CreateMap<Deal, DealDto>();
    }
}