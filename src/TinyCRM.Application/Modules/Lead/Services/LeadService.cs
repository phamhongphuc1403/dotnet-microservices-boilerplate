using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.Lead.DTOs;
using TinyCRM.Application.Modules.Lead.Services.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Application.Modules.Lead.Services;

public class LeadService : ILeadService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;
    private readonly ILeadRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public LeadService(ILeadRepository leadRepository, IAccountRepository accountRepository,
        IUnitOfWork unitOfWork, IMapper mapper, IDealRepository dealRepository)
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

        lead.Status = LeadStatuses.Prospect;

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

    public async Task<PaginationResponseDto<GetLeadDto>> GetAllByCustomerIdAsync(Guid customerId, LeadQueryDto query)
    {
        var (leads, totalCount) = await _repository.GetPagedLeadsByCustomerIdAsync(query, customerId);

        return new PaginationResponseDto<GetLeadDto>(_mapper.Map<List<GetLeadDto>>(leads), query.Page, query.Take,
            totalCount);
    }

    public async Task<PaginationResponseDto<GetLeadDto>> GetAllAsync(LeadQueryDto query)
    {
        var (leads, totalCount) = await _repository.GetPagedLeadsAsync(query);

        return new PaginationResponseDto<GetLeadDto>(_mapper.Map<List<GetLeadDto>>(leads), query.Page, query.Take,
            totalCount);
    }

    public async Task<GetLeadDto> DisqualifyLeadAsync(Guid id, DisqualifyLeadDto dto)
    {
        var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
            .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

        CheckStatusOnUpdateOrDelete(lead);

        _mapper.Map(dto, lead);

        lead.Status = LeadStatuses.Disqualify;

        _repository.Update(lead);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<GetLeadDto>(lead);
    }

    public async Task<GetDealDto> QualifyLeadAsync(Guid id)
    {
        var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
            .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

        CheckStatusOnUpdateOrDelete(lead);

        lead.Status = LeadStatuses.Qualify;

        _repository.Update(lead);

        var deal = _mapper.Map<DealEntity>(lead);

        deal.Status = DealStatuses.Open;

        _dealRepository.Add(deal);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<GetDealDto>(deal);
    }

    private async Task CheckValidOnAdd(AddLeadDto dto)
    {
        Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.CustomerId))
            .ThrowIfNotPresent(new NotFoundException("This account is not exist"));
    }

    private async Task CheckValidOnUpdate(UpdateLeadDto dto, Guid id)
    {
        if (dto.Status is LeadStatuses.Qualify or LeadStatuses.Disqualify)
            throw new BadRequestException("Cannot set qualify or disqualify status");

        Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.CustomerId))
            .ThrowIfNotPresent(new NotFoundException("This account is not exist"));

        var lead = Optional<LeadEntity>.Of(await _repository.GetByIdAsync(id))
            .ThrowIfNotPresent(new NotFoundException("Lead not found")).Get();

        CheckStatusOnUpdateOrDelete(lead);
    }

    private static void CheckStatusOnUpdateOrDelete(LeadEntity lead)
    {
        if (lead.Status is LeadStatuses.Qualify or LeadStatuses.Disqualify)
            throw new BadRequestException("Cannot update or delete qualified or disqualified lead");
    }
}