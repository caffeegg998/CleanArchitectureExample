﻿using AutoMapper;
using CleanArchitectureExample.Application.DTOs.Account;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitectureExample.Application.Features.Commands.UserCommands;

namespace CleanArchitectureExample.Application.Features.Handlers
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand, Guid>, IRequestHandler<GetUserByIdCommand, UserProfileDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserProfileDTO
            {
                UserId = request.userProfileDTO.UserId,
                FullName = request.userProfileDTO.FullName,
                DateOfBirth = request.userProfileDTO.DateOfBirth,
                DepartmentId = request.userProfileDTO.DepartmentId,
                MaNhanVien = request.userProfileDTO.MaNhanVien,
                MagnetCode = request.userProfileDTO.MagnetCode,
                CreateAt = DateTime.Now.ToUniversalTime()

            };

            UserProfile userProfile = _mapper.Map<UserProfile>(user); // Chuyển đổi User sang UserDTO
            await _unitOfWork.UserProfileRepository.SaveUser(userProfile);
            await _unitOfWork.CompleteIdentityAsync();

            return Guid.Parse(user.UserId);
            
        }

        public async Task<UserProfileDTO> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {

            UserProfile? userProfile = await _unitOfWork.UserProfileRepository.GetUser(request.Id);

            if (userProfile == null)
            {
                return new UserProfileDTO();
            }

            UserProfileDTO userProfileDTO = _mapper.Map<UserProfileDTO>(userProfile);
            return userProfileDTO;
        }
        
    }
}
