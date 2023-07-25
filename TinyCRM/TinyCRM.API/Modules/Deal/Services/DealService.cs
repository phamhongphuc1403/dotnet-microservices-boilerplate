using AutoMapper;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.PaginationHelper;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Modules.Deal.Services
{
    public class DealService : IDealService
    {
        private readonly IRepository<DealEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealService(IRepository<DealEntity> productRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResponse<GetAllDealsDto>> GetAllAsync(DealQueryDTO query)
        {
            var (deals, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<DealEntity>
                .Init(query)
                .JoinTable("Lead.Customer")
                .Build());

            return new PaginationResponse<GetAllDealsDto>(_mapper.Map<List<GetAllDealsDto>>(deals), query.Page, query.Take, totalCount);
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
            if (deal.Status == DealStatusEnum.Won || deal.Status == DealStatusEnum.Lost)
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
            return await CloseAsAsync(id, DealStatusEnum.Won);
        }

        public async Task<GetDealDto> CloseAsLostAsync(Guid id)
        {
            return await CloseAsAsync(id, DealStatusEnum.Lost);
        }

        private async Task<GetDealDto> CloseAsAsync(Guid id, DealStatusEnum status)
        {
            var deal = Optional<DealEntity>.Of(await _repository.GetByIdAsync(id))
                 .ThrowIfNotPresent(new NotFoundException("Deal not found")).Get();

            CheckStatusOnUpdateOrDelete(deal);

            CheckValidStatusOnClose(status);

            deal.Status = status;

            _repository.Update(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDto>(deal);
        }

        private static void CheckValidStatusOnClose(DealStatusEnum status)
        {
            if (status == DealStatusEnum.Open)
            {
                throw new BadRequestException("Cannot close deal as open");
            }
        }

        public async Task<PaginationResponse<GetAllDealsDto>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDTO query)
        {
            var (deals, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<DealEntity>
                .Init(query)
                .AddContraints(entity => entity.Lead.CustomerId == customerId)
                .JoinTable("Lead.Customer")
                .Build());

            return new PaginationResponse<GetAllDealsDto>(_mapper.Map<List<GetAllDealsDto>>(deals), query.Page, query.Take, totalCount);
        }
    }
}