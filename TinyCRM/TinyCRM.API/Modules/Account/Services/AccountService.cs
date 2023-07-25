using AutoMapper;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.PaginationHelper;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Modules.Account.Services
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

        public async Task<PaginationResponse<GetAccountDto>> GetAllAsync(AccountQueryDto query)
        {
            var (accounts, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<AccountEntity>
                .Init(query).Build());

            return new PaginationResponse<GetAccountDto>(_mapper.Map<List<GetAccountDto>>(accounts), query.Page, query.Take, totalCount);
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
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.Email == dto.Email))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.PhoneNumber == dto.PhoneNumber))
                    .ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }
        }

        private async Task CheckValidOnUpdate(AddOrUpdateAccountDto dto, Guid id)
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