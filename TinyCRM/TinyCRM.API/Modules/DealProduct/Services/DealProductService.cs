using AutoMapper;
using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Modules.DealProduct.Services
{
    public class DealProductService : IDealProductService
    {
        private readonly IRepository<DealProductEntity> _repository;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<DealEntity> _dealRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DealProductService(IRepository<DealProductEntity> dealProductRepository,
            IRepository<ProductEntity> productRepository,
            IRepository<DealEntity> dealRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = dealProductRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = productRepository;
            _dealRepository = dealRepository;
        }

        public async Task<GetDealProductDTO> AddAsync(AddOrUpdateProductToDealDTO dto, Guid id)
        {
            await CkeckValidOnAdd(dto, id);

            var dealProduct = _mapper.Map<DealProductEntity>(dto);

            dealProduct.DealId = id;

            _repository.Add(dealProduct);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealProductDTO>(dealProduct);
        }

        private async Task CkeckValidOnAdd(AddOrUpdateProductToDealDTO dto, Guid id)
        {
            var deal = Optional<DealEntity>.Of(await _dealRepository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            if (deal.Status != DealStatusEnum.Open)
            {
                throw new BadRequestException("Cannot add product to won or lost deal");
            }

            Optional<ProductEntity>.Of(await _productRepository.GetAnyAsync(product => product.Id == dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found"));

            Optional<DealProductEntity>.Of(await _repository.GetAnyAsync(entity => entity.ProductId == dto.ProductId))
                .ThrowIfPresent(new BadRequestException("Product already added to this deal"));
        }

        public async Task<IList<GetDealProductDTO>> GetAllAsync(Guid id, int? skip, int? take, string? name, string? sortBy, bool? descending)
        {
            var leads = await _repository.GetPaginationAsync(skip, take, entity => entity.DealId == id, sortBy, descending, "Product");

            return _mapper.Map<IList<GetDealProductDTO>>(leads);
        }

        public async Task<GetDealProductDTO> UpdateAsync(AddOrUpdateProductToDealDTO dto, Guid id)
        {
            var dealProduct = Optional<DealProductEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal product not found")).Get();

            await CheckValidOnUpdate(dto, dealProduct);

            _mapper.Map(dto, dealProduct);

            _repository.Update(dealProduct);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealProductDTO>(dealProduct);
        }

        private async Task CheckValidOnUpdate(AddOrUpdateProductToDealDTO dto, DealProductEntity dealProduct)
        {
            var deal = Optional<DealEntity>.Of(await _dealRepository.GetByIdAsync(dealProduct.DealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            if (deal.Status != DealStatusEnum.Open)
            {
                throw new BadRequestException("Cannot update product in won or lost deal");
            }

            Optional<ProductEntity>.Of(await _productRepository.GetAnyAsync(product => product.Id == dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found"));

            Optional<DealProductEntity>.Of(await _repository.GetAnyAsync(entity => entity.ProductId == dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found in this deal"));
        }

        public async Task<GetDealProductDTO> GetByIdAsync(Guid dealId, Guid id)
        {
            var deal = Optional<DealEntity>.Of(await _dealRepository.GetByIdAsync(dealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            var dealProduct = Optional<DealProductEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal product not found"));

            if (deal.Id != dealProduct.DealId)
            {
                throw new BadRequestException("Deal product not found in this deal");
            })
        }
    }
}
