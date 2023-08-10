using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Product.DTOs;
using TinyCRM.Application.Modules.Product.Services.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Application.Modules.Product.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = productRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetProductDto> AddAsync(AddOrUpdateProductDto dto)
    {
        await CheckValidOnAdd(dto);

        var product = _mapper.Map<ProductEntity>(dto);

        _repository.Add(product);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<GetProductDto>(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = Optional<ProductEntity>.Of(await _repository.GetByIdAsync(id))
            .ThrowIfNotPresent(new NotFoundException("Product not found")).Get();

        _repository.Delete(product);

        await _unitOfWork.CommitAsync();
    }

    public async Task<PaginationResponseDto<GetProductDto>> GetAllAsync(ProductQueryDto query)
    {
        var (products, totalCount) = await _repository.GetPagedProductsAsync(query);

        return new PaginationResponseDto<GetProductDto>(_mapper.Map<List<GetProductDto>>(products), query.Page,
            query.Take, totalCount);
    }

    public async Task<GetProductDto> GetByIdAsync(Guid id)
    {
        var product = Optional<ProductEntity>.Of(await _repository.GetByIdAsync(id))
            .ThrowIfNotPresent(new NotFoundException("Product not found")).Get();

        return _mapper.Map<GetProductDto>(product);
    }

    public async Task<GetProductDto> UpdateAsync(AddOrUpdateProductDto dto, Guid id)
    {
        await GetByIdAsync(id);

        await CheckValidOnUpdate(dto, id);

        var updatedProduct = _mapper.Map<ProductEntity>(dto);

        updatedProduct.Id = id;

        _repository.Update(updatedProduct);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<GetProductDto>(updatedProduct);
    }

    private async Task CheckValidOnAdd(AddOrUpdateProductDto dto)
    {
        Optional<bool>.Of(await _repository.CheckIfStringIdExist(dto.StringId))
            .ThrowIfPresent(new DuplicateException("This string Id already exist"));
    }

    private async Task CheckValidOnUpdate(AddOrUpdateProductDto dto, Guid id)
    {
        Optional<bool>.Of(await _repository.CheckIfStringIdExist(dto.StringId, id))
            .ThrowIfPresent(new DuplicateException("This string Id already exist"));
    }
}