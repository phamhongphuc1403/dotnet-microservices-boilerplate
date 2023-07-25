using AutoMapper;
using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.PaginationHelper;
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

        public async Task<GetDealProductDto> AddAsync(AddOrUpdateProductToDealDto dto, Guid id)
        {
            await CheckValidOnAdd(dto, id);

            var dealProduct = _mapper.Map<DealProductEntity>(dto);

            dealProduct.DealId = id;

            _repository.Add(dealProduct);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealProductDto>(dealProduct);
        }

        private async Task CheckValidOnAdd(AddOrUpdateProductToDealDto dto, Guid dealId)
        {
            var deal = Optional<DealEntity>.Of(await _dealRepository.GetByIdAsync(dealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            if (deal.Status != DealStatusEnum.Open)
            {
                throw new BadRequestException("Cannot add product to won or lost deal");
            }

            Optional<bool>.Of(await _productRepository.CheckIfIdExistAsync(dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found"));

            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.ProductId == dto.ProductId && dealId == entity.DealId))
                .ThrowIfPresent(new BadRequestException("Product already added to this deal"));
        }

        public async Task<PaginationResponse<GetDealProductDto>> GetAllAsync(Guid id, DealProductDTO query)
        {
            var (deals, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<DealProductEntity>
                .Init(query)
                .AddContraints(entity => entity.DealId == id)
                .JoinTable("Product")
                .Build());

            return new PaginationResponse<GetDealProductDto>(_mapper.Map<List<GetDealProductDto>>(deals), query.Page, query.Take, totalCount);
        }

        public async Task<GetDealProductDto> UpdateAsync(AddOrUpdateProductToDealDto dto, Guid dealId, Guid id)
        {
            var dealProduct = Optional<DealProductEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal product not found")).Get();

            await CheckValidOnUpdate(dealId, dto, dealProduct);

            _mapper.Map(dto, dealProduct);

            _repository.Update(dealProduct);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealProductDto>(dealProduct);
        }

        private async Task CheckValidOnUpdate(Guid dealId, AddOrUpdateProductToDealDto dto, DealProductEntity dealProduct)
        {
            var deal = Optional<DealEntity>.Of(await _dealRepository.GetByIdAsync(dealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            if (deal.Status != DealStatusEnum.Open)
            {
                throw new BadRequestException("Cannot update product in won or lost deal");
            }

            Optional<bool>.Of(await _productRepository.CheckIfIdExistAsync(dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found"));

            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.DealId == dealId && entity.ProductId == dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found in this deal")).Get();
        }

        public async Task<GetDealProductDto> GetByIdAsync(Guid dealId, Guid id)
        {
            Optional<bool>.Of(await _dealRepository.CheckIfIdExistAsync(dealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found"));

            var dealProduct = Optional<DealProductEntity>.Of(await _repository.GetAnyAsync(entity => entity.Id == id && entity.DealId == dealId))
                .ThrowIfNotPresent(new NotFoundException("Product not found in this deal")).Get();

            return _mapper.Map<GetDealProductDto>(dealProduct);
        }
    }
}