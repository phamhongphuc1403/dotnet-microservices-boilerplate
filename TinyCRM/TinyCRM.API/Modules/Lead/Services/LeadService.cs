using AutoMapper;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;
using TinyCRM.Domain.Entities.Enums;
using TinyCRM.API.Modules.Deal.DTOs;

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

        public async Task<GetLeadDTO> AddAsync(AddLeadDTO dto)
        {
            await CheckValidOnAdd(dto);

            var lead = _mapper.Map<LeadEntity>(dto);

            lead.Status = LeadStatusEnum.Prospect;

            _repository.Add(lead);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetLeadDTO>(lead);
        }

        public async Task DeleteAsync(Guid id)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);

            _repository.Delete(lead);

            await _unitOfWork.CommitAsync();
        }

        public async Task<GetLeadDTO> GetByIdAsync(Guid id)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            return _mapper.Map<GetLeadDTO>(lead);
        }

        public async Task<GetLeadDTO> UpdateAsync(UpdateLeadDTO dto, Guid id)
        {
            await CheckValidOnUpdate(dto, id);

            var updatedLead = _mapper.Map<LeadEntity>(dto);

            updatedLead.Id = id;

            _repository.Update(updatedLead);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetLeadDTO>(updatedLead);
        }

        private async Task CheckValidOnAdd(AddLeadDTO dto)
        {
            Optional<AccountEntity>.Of(await _accountRepository.GetByIdAsync(dto.CustomerId))
                .ThrowIfNotPresent(new NotFoundException("This account is not exist"));
        }

        private async Task CheckValidOnUpdate(UpdateLeadDTO dto, Guid id)
        {
            if (dto.Status == LeadStatusEnum.Qualify || dto.Status == LeadStatusEnum.Disqualify)
            {
                throw new BadRequestException("Cannot set qualify or disqualify status");
            }

            Optional<AccountEntity>.Of(await _accountRepository.GetByIdAsync(dto.CustomerId)).ThrowIfNotPresent(new NotFoundException("This account is not exist"));

            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id)).ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);
        }

        private static void CheckStatusOnUpdateOrDelete(LeadEntity lead)
        {
            if (lead.Status == LeadStatusEnum.Qualify || lead.Status == LeadStatusEnum.Disqualify)
            {
                throw new BadRequestException("Cannot update or delete qualified or disqualified lead");
            }
        }

        public async Task<IList<GetLeadDTO>> GetAllByCustomerIdAsync(Guid customerId, int? skip, int? take, string? title, string? sortBy, bool? descending)
        {
            var leads = await _repository.GetPaginationAsync(skip, take, 
                entity => entity.CustomerId == customerId && entity.Title.Contains(title ?? ""), 
                sortBy, descending);

            return _mapper.Map<IList<GetLeadDTO>>(leads);
        }

        public async Task<IList<GetLeadDTO>> GetAllAsync(int? skip, int? take, string? title, string? sortBy, bool? descending)
        {
            var leads = await _repository.GetPaginationAsync(skip, take, entity => entity.Title.Contains(title ?? ""), sortBy, descending);

            return _mapper.Map<IList<GetLeadDTO>>(leads);
        }

        public async Task<GetLeadDTO> DisqualifyLeadAsync(Guid id, DisqualifyLeadDTO dto)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id)).ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);

            _mapper.Map(dto, lead);

            lead.Status = LeadStatusEnum.Disqualify;

            _repository.Update(lead);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetLeadDTO>(lead);
        }

        public async Task<GetDealDTO> QualifyLeadAsync(Guid id)
        {
            var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id)).ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

            CheckStatusOnUpdateOrDelete(lead);

            lead.Status = LeadStatusEnum.Qualify;

            _repository.Update(lead);

            var deal = _mapper.Map<DealEntity>(lead);
            
            deal.Status = DealStatusEnum.Open;

            _dealRepository.Add(deal);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetDealDTO>(deal);
        }
    }
}
