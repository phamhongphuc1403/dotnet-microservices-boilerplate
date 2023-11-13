using AutoMapper;
using SaleManagement.Core.Application.DTOs.ProductDTOs;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Infrastructure.EntityFrameworkCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductSummaryDto>();
    }
}