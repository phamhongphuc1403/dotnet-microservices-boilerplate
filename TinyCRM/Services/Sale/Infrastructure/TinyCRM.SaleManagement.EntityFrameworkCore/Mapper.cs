using AutoMapper;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.EntityFrameworkCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Deal, DealDto>();
    }
}