using AutoMapper;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Application.Modules.User.Services.Interfaces;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.Application.Modules.User.Services;

public class UserService : IUserService
{
    private readonly IIdentityAuthService _identityAuthService;
    private readonly IIdentityRoleService _identityRoleService;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IIdentityService identityService,
        IIdentityRoleService identityRoleService,
        IIdentityAuthService identityAuthService,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _identityRoleService = identityRoleService;
        _identityAuthService = identityAuthService;
    }

    public async Task<GetUserDto> CreateAsync(CreateOrEditUserDto dto)
    {
        CheckPasswordMatching(dto);

        try
        {
            var user = _mapper.Map<UserEntity>(dto);

            await _unitOfWork.BeginTransactionAsync();

            var id = await _identityService.CreateAsync(user);

            await _identityRoleService.AddToRoleAsync(id, Domain.Constants.Role.User);

            await _unitOfWork.CommitTransactionAsync();

            user.Id = new Guid(id);

            return _mapper.Map<GetUserDto>(user);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<GetUserDto> GetByIdAsync(string id)
    {
        var user = await _identityService.GetByIdAsync(id);

        return _mapper.Map<GetUserDto>(user);
    }

    public async Task<GetUserDto> UpdateAsync(string id, CreateOrEditUserDto dto)
    {
        CheckPasswordMatching(dto);

        var user = await _identityService.GetByIdAsync(id);

        _mapper.Map(dto, user);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _identityService.UpdateAsync(user);

            await _identityAuthService.UpdatePasswordAsync(id, dto.Password);

            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<GetUserDto>(user);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<PaginationResponseDto<GetUserDto>> GetAllAsync(UserQueryDto query)
    {
        var (users, totalCount) = await _identityService.GetAllAsync(query);

        return new PaginationResponseDto<GetUserDto>(_mapper.Map<List<GetUserDto>>(users), query.Page, query.Take,
            totalCount);
    }

    private static void CheckPasswordMatching(CreateOrEditUserDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            throw new BadRequestException("Password and confirm password do not match");
    }
}