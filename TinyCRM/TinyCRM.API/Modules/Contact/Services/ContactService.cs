using AutoMapper;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.PaginationHelper;
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

        public async Task<GetContactDto> AddAsync(AddOrUpdateContactDto dto)
        {
            await CheckValidOnAdd(dto);

            var contact = _mapper.Map<ContactEntity>(dto);

            _repository.Add(contact);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetContactDto>(contact);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = Optional<ContactEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Contact not found")).Get(); ;

            _repository.Delete(contact);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginationResponse<GetContactDto>> GetAllAsync(ContactQueryDTO query)
        {
            var (contacts, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<ContactEntity>
                .Init(query).Build());

            return new PaginationResponse<GetContactDto>(_mapper.Map<List<GetContactDto>>(contacts), query.Page, query.Take, totalCount);
        }

        public async Task<GetContactDto> GetByIdAsync(Guid id)
        {
            var contact = Optional<ContactEntity>.Of(await _repository.GetByIdAsync(id))
                .ThrowIfNotPresent(new NotFoundException("Contact not found")).Get();

            return _mapper.Map<GetContactDto>(contact);
        }

        public async Task<GetContactDto> UpdateAsync(AddOrUpdateContactDto dto, Guid id)
        {
            await GetByIdAsync(id);

            await CheckValidOnUpdate(dto, id);

            var updatedContact = _mapper.Map<ContactEntity>(dto);

            updatedContact.Id = id;

            _repository.Update(updatedContact);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetContactDto>(updatedContact);
        }

        private async Task CheckValidOnAdd(AddOrUpdateContactDto dto)
        {
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.Email == dto.Email))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.PhoneNumber == dto.PhoneNumber))
                    .ThrowIfPresent(new DuplicateException("This phone number already exist"));
            }

            if (dto.AccountId != null)
            {
                Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.AccountId ?? Guid.Empty))
                    .ThrowIfNotPresent(new NotFoundException("This account is not exist"));
            }
        }

        private async Task CheckValidOnUpdate(AddOrUpdateContactDto dto, Guid id)
        {
            Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.Email == dto.Email && entity.Id != id))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfExistAsync(entity => entity.PhoneNumber == dto.PhoneNumber && entity.Id != id))
                    .ThrowIfPresent(new DuplicateException("This email already exist"));
            }

            if (dto.AccountId != null)
            {
                Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.AccountId ?? Guid.Empty))
                    .ThrowIfNotPresent(new NotFoundException("This account is not exist"));
            }
        }

        public async Task<PaginationResponse<GetContactDto>> GetAllByAccountIdAsync(Guid id, ContactQueryDTO query)
        {
            var (contacts, totalCount) = await _repository.GetPaginationAsync(PaginationBuilder<ContactEntity>
                .Init(query)
                .AddContraints(entity => entity.AccountId == id)
                .Build());

            return new PaginationResponse<GetContactDto>(_mapper.Map<List<GetContactDto>>(contacts), query.Page, query.Take, totalCount);
        }
    }
}