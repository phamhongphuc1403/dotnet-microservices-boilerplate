using AutoMapper;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.PaginationHelper;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Modules.Lead.Services
{
    public class LeadService : ILeadService
    {
        private readonly IRepository<LeadEntity> _repository;
        private readonly IRepository<AccountEntity> _accountRepository;
        private readonly IRepository<DealEntity> _dealRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeadService(IRepository<LeadEntity> leadRepository, IRepository<AccountEntity> accountRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IRepository<DealEntity> dealRepository)
        {
            _repository = leadRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dealRepository = dealRepository;
        }

        public async Task<GetLeadDto> AddAsync(AddLeadDto dto)
        {
            await CheckValidOnAdd(dto);

            var lead = _mapper.Map<LeadEntity>(dto);

            lead.Status = LeadStatusEnum.Prospect;

            _repository.Add(lead);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetLeadDto>(lead);
        }

        public async Task DeleteAsync(Guid id)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);

            _repository.Delete(lead);

            await _unitOfWork.CommitAsync();
        }

        public async Task<GetLeadDto> GetByIdAsync(Guid id)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            return _mapper.Map<GetLeadDto>(lead);
        }

        public async Task<GetLeadDto> UpdateAsync(UpdateLeadDto dto, Guid id)
        {
            await CheckValidOnUpdate(dto, id);

            var updatedLead = _mapper.Map<LeadEntity>(dto);

            updatedLead.Id = id;

            _repository.Update(updatedLead);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetLeadDto>(updatedLead);
        }

        private async Task CheckValidOnAdd(AddLeadDto dto)
        {
            Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.CustomerId))
                .ThrowIfNotPresent(new NotFoundException("This account is not exist"));
        }

        private async Task CheckValidOnUpdate(UpdateLeadDto dto, Guid id)
        {
            if (dto.Status == LeadStatusEnum.Qualify || dto.Status == LeadStatusEnum.Disqualify)
            {
                throw new BadRequestException("Cannot set qualify or disqualify status");
            }

            Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.CustomerId))
                .ThrowIfNotPresent(new NotFoundException("This account is not exist"));

            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);
        }

        private static void CheckStatusOnUpdateOrDelete(LeadEntity lead)
        {
            if (lead.Status == LeadStatusEnum.Qualify || lead.Status == LeadStatusEnum.Disqualify)
            {
                throw new BadRequestException("Cannot update or delete qualified or disqualified lead");
            }
        }

        public async Task<PaginationResponse<GetLeadDto>> GetAllByCustomerIdAsync(Guid customerId, LeadQueryDTO query)
        {
            var (leads, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<LeadEntity>
                .Init(query)
                .AddContraints(entity => entity.CustomerId == customerId)
                .Build());

            return new PaginationResponse<GetLeadDto>(_mapper.Map<List<GetLeadDto>>(leads), query.Page, query.Take, totalCount);
        }

        public async Task<PaginationResponse<GetLeadDto>> GetAllAsync(LeadQueryDTO query)
        {
            var (leads, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<LeadEntity>
                .Init(query)
                .Build());

            return new PaginationResponse<GetLeadDto>(_mapper.Map<List<GetLeadDto>>(leads), query.Page, query.Take, totalCount);
        }

        public async Task<GetLeadDto> DisqualifyLeadAsync(Guid id, DisqualifyLeadDto dto)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);

            _mapper.Map(dto, lead);

            lead.Status = LeadStatusEnum.Disqualify;

            _repository.Update(lead);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetLeadDto>(lead);
        }

        public async Task<GetDealDto> QualifyLeadAsync(Guid id)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);

            lead.Status = LeadStatusEnum.Qualify;

            _repository.Update(lead);

            var deal = _mapper.Map<DealEntity>(lead);

            deal.Status = DealStatusEnum.Open;

            _dealRepository.Add(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDto>(deal);
        }
    }
}