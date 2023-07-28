using AutoMapper;
using TinyCRM.API.Modules.Product.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.PaginationHelper;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Modules.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IRepository<ProductEntity> productRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProductDTO> AddAsync(AddOrUpdateProductDTO dto)
        {
            await CheckValidOnAdd(dto);

            var product = _mapper.Map<ProductEntity>(dto);

            _repository.Add(product);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetProductDTO>(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = Optional<ProductEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Product not found")).Get();

            _repository.Delete(product);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginationResponse<GetProductDTO>> GetAllAsync(ProductQueryDTO query)
        {
            var (products, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<ProductEntity>
                .Init(query).Build());

            return new PaginationResponse<GetProductDTO>(_mapper.Map<List<GetProductDTO>>(products), query.Page, query.Take, totalCount);
        }

        public async Task<GetProductDTO> GetByIdAsync(Guid id)
        {
            var product = Optional<ProductEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Product not found")).Get();

            return _mapper.Map<GetProductDTO>(product);
        }

        public async Task<GetProductDTO> UpdateAsync(AddOrUpdateProductDTO dto, Guid id)
        {
            await GetByIdAsync(id);

            await CheckValidOnUpdate(dto, id);

            var updatedProduct = _mapper.Map<ProductEntity>(dto);

            updatedProduct.Id = id;

            _repository.Update(updatedProduct);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetProductDTO>(updatedProduct);
        }

        private async Task CheckValidOnAdd(AddOrUpdateProductDTO dto)
        {
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.StringId == dto.StringId))
                .ThrowIfPresent(new DuplicateException("This string Id already exist"));
        }

        private async Task CheckValidOnUpdate(AddOrUpdateProductDTO dto, Guid id)
        {
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.StringId == dto.StringId && entity.Id != id))
                .ThrowIfPresent(new DuplicateException("This string Id already exist"));
        }
    }
}