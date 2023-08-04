﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Common.Constants;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Application.Modules.User.Services.Interfaces;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.Application.Modules.User.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService _identityService;
        private readonly IIdentityAuthService _identityAuthService;
        private readonly IIdentityRoleService _identityRoleService;
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

        public async Task<GetUserDTO> CreateAsync(CreateOrEditUserDTO dto)
        {
            CheckPasswordMatching(dto);

            try
            {
                var user = _mapper.Map<UserEntity>(dto);

                await _unitOfWork.BeginTransactionAsync();

                var id = await _identityService.CreateAsync(user);

                await _identityRoleService.AddToRoleAsync(id, Role.Member);

                await _unitOfWork.CommitTransactionAsync();

                user.Id = new Guid(id);

                return _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<GetUserDTO> GetByIdAsync(string id)
        {
            var user = await _identityService.GetByIdAsync(id);

            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task<GetUserDTO> UpdateAsync(string id, CreateOrEditUserDTO dto)
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

                return _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private void CheckPasswordMatching(CreateOrEditUserDTO dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new BadRequestException("Password and confirm password do not match");
            }
        }
    }
}