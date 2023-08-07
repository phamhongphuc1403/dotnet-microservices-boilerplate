using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.DealProduct.DTOs;
using TinyCRM.Application.Modules.DealProduct.Services.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Application.Modules.DealProduct.Services
{
    public class DealProductService : IDealProductService
    {
        private readonly IDealProductRepository _repository;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IDealRepository _dealRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealProductService(IDealProductRepository dealProductRepository,
            IRepository<ProductEntity> productRepository,
            IDealRepository dealRepository,
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
            Optional<bool>.Of(await _dealRepository.CheckIfIdExistAsync(dealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found"));

            Optional<bool>.Of(await _dealRepository.CheckIfOpenDealByIdExist(dealId))
                .ThrowIfNotPresent(new BadRequestException("Cannot add product to won or lost deal"));

            Optional<bool>.Of(await _productRepository.CheckIfIdExistAsync(dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found"));
        }

        public async Task<PaginationResponseDto<GetDealProductDto>> GetAllAsync(Guid id, DealProductDto query)
        {
            var (deals, totalCount) = await _repository.GetPagedProductsByDealAsync(query, id);

            return new PaginationResponseDto<GetDealProductDto>(_mapper.Map<List<GetDealProductDto>>(deals), query.Page, query.Take, totalCount);
        }

        public async Task<GetDealProductDto> UpdateAsync(AddOrUpdateProductToDealDto dto, Guid dealId, Guid id)
        {
            var dealProduct = Optional<DealProductEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal product not found")).Get();

            await CheckValidOnUpdate(dealId, dto);

            _mapper.Map(dto, dealProduct);

            _repository.Update(dealProduct);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealProductDto>(dealProduct);
        }

        private async Task CheckValidOnUpdate(Guid dealId, AddOrUpdateProductToDealDto dto)
        {
            Optional<bool>.Of(await _dealRepository.CheckIfOpenDealByIdExist(dealId))
                .ThrowIfNotPresent(new BadRequestException("Cannot update product in won or lost deal"));

            Optional<bool>.Of(await _productRepository.CheckIfIdExistAsync(dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found"));

            Optional<bool>.Of(await _repository.CheckIfProductInDeal(dealId, dto.ProductId))
                .ThrowIfNotPresent(new NotFoundException("Product not found in this deal")).Get();
        }

        public async Task<GetDealProductDto> GetByIdAsync(Guid dealId, Guid id)
        {
            Optional<bool>.Of(await _dealRepository.CheckIfIdExistAsync(dealId))
                .ThrowIfNotPresent(new NotFoundException("Deal not found"));

            var dealProduct = Optional<DealProductEntity>.Of(await _repository.GetProductInDeal(dealId, id))
                .ThrowIfNotPresent(new NotFoundException("Product not found in this deal")).Get();

            return _mapper.Map<GetDealProductDto>(dealProduct);
        }
    }
}