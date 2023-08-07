using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Account.DTOs;
using TinyCRM.Application.Modules.Account.Services.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Application.Modules.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = accountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAccountDto> AddAsync(AddOrUpdateAccountDto dto)
        {
            await CheckValidOnAdd(dto);

            var account = _mapper.Map<AccountEntity>(dto);

            _repository.Add(account);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetAccountDto>(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = Optional<AccountEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Account not found")).Get();

            _repository.Delete(account);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginationResponseDto<GetAccountDto>> GetAllAsync(AccountQueryDto query)
        {
            var (accounts, totalCount) = await _repository.GetPagedAccountsAsync(query);

            return new PaginationResponseDto<GetAccountDto>(_mapper.Map<List<GetAccountDto>>(accounts), query.Page, query.Take, totalCount);
        }

        public async Task<GetAccountDto> GetByIdAsync(Guid id)
        {
            var account = Optional<AccountEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Account not found")).Get();

            return _mapper.Map<GetAccountDto>(account);
        }

        public async Task<GetAccountDto> UpdateAsync(AddOrUpdateAccountDto dto, Guid id)
        {
            await GetByIdAsync(id);

            await CheckValidOnUpdate(dto, id);

            var updatedAccount = _mapper.Map<AccountEntity>(dto);

            updatedAccount.Id = id;

            _repository.Update(updatedAccount);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetAccountDto>(updatedAccount);
        }

        private async Task CheckValidOnAdd(AddOrUpdateAccountDto dto)
        {
            Optional<bool>.Of(await _repository.CheckIfEmailExistAsync(dto.Email))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfPhoneNumberExistAsync(dto.PhoneNumber))
                    .ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }
        }

        private async Task CheckValidOnUpdate(AddOrUpdateAccountDto dto, Guid id)
        {
            Optional<bool>.Of(await _repository.CheckIfEmailExistAsync(dto.Email, id))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfPhoneNumberExistAsync(dto.PhoneNumber, id))
                    .ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }
        }
    }
}