using AutoMapper;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.Account.Model;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
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

        public async Task<IList<GetAccountDTO>> GetAllAsync(int? skip, int? take, string? name, string? sortBy, bool? descending)
        {
            var accounts = await _repository.GetPaginationAsync(skip, take, entity => entity.Name.Contains(name ?? ""), sortBy, descending);

            return _mapper.Map<IList<GetAccountDTO>>(accounts);
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
            Optional<AccountEntity>.Of(await _repository.GetAnyAsync(entity => entity.Email == dto.Email)).ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<AccountEntity>.Of(await _repository.GetAnyAsync(entity => entity.PhoneNumber == dto.PhoneNumber)).ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }
        }

        private async Task CheckValidOnUpdate(AddOrUpdateAccountDTO dto, Guid id)
        {
            var accountByEmail = await _repository.GetAnyAsync(entity => entity.Email == dto.Email);

            if (accountByEmail?.Id != id)
            {
                throw new DuplicateException("This email already exist");
            }


            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                var accountByPhoneNumber = await _repository.GetAnyAsync(entity => entity.PhoneNumber == dto.PhoneNumber);

                if (accountByPhoneNumber?.Id != id)
                {
                    throw new DuplicateException("This phone number already exist");
                }
            }
        }
    }
}
