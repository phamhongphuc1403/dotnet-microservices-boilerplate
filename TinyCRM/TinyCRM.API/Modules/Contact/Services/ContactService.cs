using AutoMapper;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Modules.Contact.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<ContactEntity> _repository;
        private readonly IRepository<AccountEntity> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(
            IRepository<ContactEntity> contactRepository, 
            IMapper mapper, IUnitOfWork unitOfWork,
            IRepository<AccountEntity> accountRepository)
        {
            _repository = contactRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }
        public async Task<GetContactDTO> AddAsync(AddOrUpdateContactDTO dto)
        {
            await CheckValidOnAdd(dto);

            var contact = _mapper.Map<ContactEntity>(dto);

            _repository.Add(contact);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetContactDTO>(contact);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = Optional<ContactEntity>.Of(await _repository.GetByIdAsync(id)).ThrowIfNotPresent(new NotFoundException("Contact not found")).Get(); ;

            _repository.Delete(contact);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IList<GetContactDTO>> GetAllAsync(int? skip, int? take, string? name, string? sortBy, bool? descending)
        {
            var contacts = await _repository.GetPaginationAsync(skip, take, entity => entity.Name.Contains(name ?? ""), sortBy, descending);

            return _mapper.Map<IList<GetContactDTO>>(contacts);
        }

        public async Task<GetContactDTO> GetByIdAsync(Guid id)
        {
            var contact = Optional<ContactEntity>.Of(await _repository.GetByIdAsync(id)).ThrowIfNotPresent(new NotFoundException("Contact not found")).Get();
        
            return _mapper.Map<GetContactDTO>(contact);
        }

        public async Task<GetContactDTO> UpdateAsync(AddOrUpdateContactDTO dto, Guid id)
        {
            await GetByIdAsync(id);

            await CheckValidOnUpdate(dto, id);

            var updatedContact = _mapper.Map<ContactEntity>(dto);

            updatedContact.Id = id;

            _repository.Update(updatedContact);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetContactDTO>(updatedContact);
        }

        private async Task CheckValidOnAdd(AddOrUpdateContactDTO dto)
        {
            Optional<ContactEntity>.Of(await _repository.GetAnyAsync(entity => entity.Email == dto.Email)).ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<ContactEntity>.Of(await _repository.GetAnyAsync(entity => entity.PhoneNumber == dto.PhoneNumber)).ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }

            if (dto.AccountId != null)
            {
                Optional<AccountEntity>.Of(await _accountRepository.GetByIdAsync(dto.AccountId ?? Guid.Empty)).ThrowIfNotPresent(new NotFoundException("This account is not exist"));
            }
        }

        private async Task CheckValidOnUpdate(AddOrUpdateContactDTO dto, Guid id)
        {
            var contactByEmail = await _repository.GetAnyAsync(entity => entity.Email == dto.Email);

            if (contactByEmail?.Id != id)
            {
                throw new DuplicateException("This email already exist");
            }


            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                var contactByPhoneNumber = await _repository.GetAnyAsync(entity => entity.PhoneNumber == dto.PhoneNumber);

                if (contactByPhoneNumber?.Id != id)
                {
                    throw new DuplicateException("This phone number already exist");
                }
            }

            if (dto.AccountId != null)
            {
                Optional<AccountEntity>.Of(await _accountRepository.GetByIdAsync(dto.AccountId ?? Guid.Empty)).ThrowIfNotPresent(new NotFoundException("This account is not exist"));
            }
        }

        public async Task<IList<GetContactDTO>> GetAllByAccountIdAsync(Guid accountId, int? skip, int? take, string? name, string? sortBy, bool? descending)
        {
            var contacts = await _repository.GetPaginationAsync(skip, take, entity => entity.AccountId == accountId, sortBy, descending);

            return _mapper.Map<IList<GetContactDTO>>(contacts);
        }
    }
}
