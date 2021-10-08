﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Exceptions.ProfileExceptions;
using Domain.Exceptions.UserException;
using Domain.Repositories;
using Profile = Contracts.Profile;

namespace Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<Profile> GetDetails(string username, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(username,  cancellationToken);

            if (user == null) throw new UserNotFound(username);

            return _mapper.Map<Profile>(user);
        }

        public async Task<IEnumerable<TopicDto>> GetTopics(string username, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(username,  cancellationToken);

            if (user == null) throw new UserNotFound(username);

            var topics = _unitOfWork.TopicRepository.GetAllByCreatorIdAsync(user.Id,  cancellationToken);

            return _mapper.Map<IEnumerable<TopicDto>>(topics);
        }

        public async Task UpdateAsync(string displayName, string bio, CancellationToken cancellationToken = default)
        {
            string username = _userAccessor.GetUsername();

            await UpdateAsync(username, displayName, bio, cancellationToken);

        }

        public async Task UpdateAsync(string username, string displayName, string bio, CancellationToken cancellationToken = default)
        {
           
            var user = await _unitOfWork.UserRepository.GetByUsername(username, cancellationToken = default);

            if (!string.IsNullOrEmpty(displayName))
            {
                user.DisplayName = displayName;
            }

            if (!string.IsNullOrEmpty(bio))
            {
                user.Bio = bio;
            }

            var result = await _unitOfWork.SaveChangesAsync();

            if (!result) throw new ProfileUpdateException("Problem updating user");
        }
    }
}