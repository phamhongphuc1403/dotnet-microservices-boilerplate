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
        private readonly IRepository<AccountEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IRepository<AccountEntity> accountRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = accountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAccountDTO> AddAsync(AddOrUpdateAccountDTO dto)
        {
            await CheckValidOnAdd(dto);

            var account = _mapper.Map<AccountEntity>(dto);

            _repository.Add(account);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetAccountDTO>(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = Optional<AccountEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Account not found")).Get();

            _repository.Delete(account);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginationResponseDTO<GetAccountDTO>> GetAllAsync(AccountQueryDTO query)
        {
            var (accounts, totalCount) = await _repository.GetPaginationAsync(
                PaginationBuilder<AccountEntity>
                    .Init(query)
                    .Build());

            return new PaginationResponseDTO<GetAccountDTO>(_mapper.Map<List<GetAccountDTO>>(accounts), query.Page, query.Take, totalCount);
        }

        public async Task<GetAccountDTO> GetByIdAsync(Guid id)
        {
            var account = Optional<AccountEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Account not found")).Get();

            return _mapper.Map<GetAccountDTO>(account);
        }

        public async Task<GetAccountDTO> UpdateAsync(AddOrUpdateAccountDTO dto, Guid id)
        {
            await GetByIdAsync(id);

            await CheckValidOnUpdate(dto, id);

            var updatedAccount = _mapper.Map<AccountEntity>(dto);

            updatedAccount.Id = id;

            _repository.Update(updatedAccount);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetAccountDTO>(updatedAccount);
        }

        private async Task CheckValidOnAdd(AddOrUpdateAccountDTO dto)
        {
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.Email == dto.Email))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.PhoneNumber == dto.PhoneNumber))
                    .ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }
        }

        private async Task CheckValidOnUpdate(AddOrUpdateAccountDTO dto, Guid id)
        {
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.Email == dto.Email && entity.Id != id))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.PhoneNumber == dto.PhoneNumber && entity.Id != id))
                    .ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }
        }
    }
}