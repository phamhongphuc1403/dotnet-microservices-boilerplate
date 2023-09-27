using AutoMapper;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.EntityFrameworkCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductDto>();
    }
}