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

        public async Task<PaginationResponseDTO<GetAllDealsDTO>> GetAllAsync(DealQueryDTO query)
        {
            var (deals, totalCount) = await _repository.GetPaginationAsync(
                PaginationBuilder<DealEntity>
                    .Init(query)
                    .JoinTable("Lead.Customer")
                    .Build());

            return new PaginationResponseDTO<GetAllDealsDTO>(_mapper.Map<List<GetAllDealsDTO>>(deals), query.Page, query.Take, totalCount);
        }

        public async Task<GetDealDTO> UpdateAsync(Guid id, UpdateDealDTO dto)
        {
            var deal = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            CheckStatusOnUpdateOrDelete(deal);

            _mapper.Map(dto, deal);

            _repository.Update(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDTO>(deal);
        }

        private static void CheckStatusOnUpdateOrDelete(DealEntity deal)
        {
            if (deal.Status == DealStatusEnum.Won || deal.Status == DealStatusEnum.Lost)
            {
                throw new BadRequestException("Cannot update won or lost deal");
            }
        }

        public async Task<GetDealDTO> GetByIdAsync(Guid id)
        {
            var lead = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id, "Lead.Customer"))
               .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            return _mapper.Map<GetDealDTO>(lead);
        }

        public async Task<GetDealDTO> CloseAsWonAsync(Guid id)
        {
            return await CloseAsAsync(id, DealStatusEnum.Won);
        }

        public async Task<GetDealDTO> CloseAsLostAsync(Guid id)
        {
            return await CloseAsAsync(id, DealStatusEnum.Lost);
        }

        private async Task<GetDealDTO> CloseAsAsync(Guid id, DealStatusEnum status)
        {
            var deal = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id))
                 .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            CheckStatusOnUpdateOrDelete(deal);

            CheckValidStatusOnClose(status);

            deal.Status = status;

            _repository.Update(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDTO>(deal);
        }

        private static void CheckValidStatusOnClose(DealStatusEnum status)
        {
            if (status == DealStatusEnum.Open)
            {
                throw new BadRequestException("Cannot close deal as open");
            }
        }

        public async Task<PaginationResponseDTO<GetAllDealsDTO>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDTO query)
        {
            var (deals, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<DealEntity>
                .Init(query)
                .AddConstraint(entity => entity.Lead.CustomerId == customerId)
                .JoinTable("Lead.Customer")
                .Build());

            return new PaginationResponseDTO<GetAllDealsDTO>(_mapper.Map<List<GetAllDealsDTO>>(deals), query.Page, query.Take, totalCount);
        }
    }
}