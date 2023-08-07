using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.Deal.Services.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Application.Modules.Deal.Services
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealService(IDealRepository productRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResponseDto<GetAllDealsDto>> GetAllAsync(DealQueryDto query)
        {
            var (deals, totalCount) = await _repository.GetPagedDealsAsync(query);

            return new PaginationResponseDto<GetAllDealsDto>(_mapper.Map<List<GetAllDealsDto>>(deals), query.Page, query.Take, totalCount);
        }

        public async Task<GetDealDto> UpdateAsync(Guid id, UpdateDealDto dto)
        {
            var deal = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            CheckStatusOnUpdateOrDelete(deal);

            _mapper.Map(dto, deal);

            _repository.Update(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDto>(deal);
        }

        private static void CheckStatusOnUpdateOrDelete(DealEntity deal)
        {
            if (deal.Status != DealStatuses.Open)
            {
                throw new BadRequestException("Cannot update won or lost deal");
            }
        }

        public async Task<GetDealDto> GetByIdAsync(Guid id)
        {
            var lead = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id, "Lead.Customer"))
               .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            return _mapper.Map<GetDealDto>(lead);
        }

        public async Task<GetDealDto> CloseAsWonAsync(Guid id)
        {
            return await CloseAsAsync(id, DealStatuses.Won);
        }

        public async Task<GetDealDto> CloseAsLostAsync(Guid id)
        {
            return await CloseAsAsync(id, DealStatuses.Lost);
        }

        private async Task<GetDealDto> CloseAsAsync(Guid id, DealStatuses status)
        {
            var deal = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id))
                 .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            CheckStatusOnUpdateOrDelete(deal);

            deal.Status = status;

            _repository.Update(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDto>(deal);
        }

        public async Task<PaginationResponseDto<GetAllDealsDto>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDto query)
        {
            var (deals, totalCount) = await _repository.GetPagedDealsByCustomerIdAsync(query, customerId);

            return new PaginationResponseDto<GetAllDealsDto>(_mapper.Map<List<GetAllDealsDto>>(deals), query.Page, query.Take, totalCount);
        }
    }
}