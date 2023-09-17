using AutoMapper;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.EntityFrameworkCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Deal, DealDto>();
    }
}