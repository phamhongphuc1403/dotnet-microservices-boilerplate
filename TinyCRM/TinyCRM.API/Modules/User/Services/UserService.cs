﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.API.Common.Constants;
using TinyCRM.API.Modules.User.DTOs;
using TinyCRM.API.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.API.Modules.User.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, UserManager<UserEntity> userManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserDTO> CreateAsync(CreateOrEditUserDTO model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException("Password and confirm password do not match");
            }

            try
            {
                var user = _mapper.Map<UserEntity>(model);

                await _unitOfWork.BeginTransactionAsync();

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    throw new BadRequestException(result.Errors.First().Description);
                }

                await _userManager.AddToRoleAsync(user, Role.Member);

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<GetUserDTO> GetByIdAsync(Guid id)
        {
            var user = Optional<UserEntity>.Of(await _userManager.FindByIdAsync(id.ToString()))
                .ThrowIfNotPresent(new NotFoundException("User not found")).Get();

            return _mapper.Map<GetUserDTO>(user);
        }

        public Task<GetUserDTO> UpdateAsync(Guid id, CreateOrEditUserDTO model)
        {
            throw new NotImplementedException();
        }
    }
}