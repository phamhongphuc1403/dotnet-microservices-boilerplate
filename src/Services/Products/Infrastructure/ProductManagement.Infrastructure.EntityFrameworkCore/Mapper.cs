using AutoMapper;
using ProductManagement.Core.Application.DTOs.ProductDTOs;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Infrastructure.EntityFrameworkCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductSummaryDto>();
        CreateMap<Product, ProductDetailDto>();
    }
}