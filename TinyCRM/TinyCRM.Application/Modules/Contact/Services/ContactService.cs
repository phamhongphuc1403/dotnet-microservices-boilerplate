using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Contact.DTOs;
using TinyCRM.Application.Modules.Contact.Services.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Application.Modules.Contact.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IRepository<AccountEntity> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(
            IContactRepository contactRepository,
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
                .ThrowIfNotPresent(new NotFoundException("Contact not found")).Get();

            _repository.Delete(contact);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginationResponseDto<GetContactDto>> GetAllAsync(ContactQueryDto query)
        {
            var (contacts, totalCount) = await _repository.GetPagedContactsAsync(query);

            return new PaginationResponseDto<GetContactDto>(_mapper.Map<List<GetContactDto>>(contacts), query.Page, query.Take, totalCount);
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
            Optional<bool>.Of(await _repository.CheckIfEmailExistAsync(dto.Email))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfPhoneNumberExistAsync(dto.PhoneNumber))
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
            Optional<bool>.Of(await _repository.CheckIfEmailExistAsync(dto.Email, id))
                .ThrowIfPresent(new DuplicateException("This email already exist"));

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                Optional<bool>.Of(await _repository.CheckIfPhoneNumberExistAsync(dto.PhoneNumber, id))
                    .ThrowIfPresent(new DuplicateException("This email already exist"));
            }

            if (dto.AccountId != null)
            {
                Optional<bool>.Of(await _accountRepository.CheckIfIdExistAsync(dto.AccountId ?? Guid.Empty))
                    .ThrowIfNotPresent(new NotFoundException("This account is not exist"));
            }
        }

        public async Task<PaginationResponseDto<GetContactDto>> GetAllByAccountIdAsync(Guid id, ContactQueryDto query)
        {
            var (contacts, totalCount) = await _repository.GetPagedContactsByAccountIdAsync(query, id);

            return new PaginationResponseDto<GetContactDto>(_mapper.Map<List<GetContactDto>>(contacts), query.Page, query.Take, totalCount);
        }
    }
}