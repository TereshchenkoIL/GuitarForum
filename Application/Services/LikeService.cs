﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.LikeExceptions;
using Domain.Exceptions.TopicExceptions;
using Domain.Exceptions.UserException;
using Domain.Repositories;

namespace Application.Services
{
    public class LikeService : ILikeService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
    
        public LikeService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<LikeDto> GetLike(string userId, Guid topicId,  CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByUserId(userId,  cancellationToken);

            if (user == null) throw new UserNotFound(userId);
            
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId,  cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);
            
            var like = await _unitOfWork.LikeRepository.GetLike(userId, topicId,  cancellationToken);

            if (like == null) throw new LikeNotFoundException(user.DisplayName, topic.Title);

            return _mapper.Map<LikeDto>(like);

        }

        public async Task<IEnumerable<LikeDto>> GetAllAsync( CancellationToken cancellationToken = default)
        {
            var likes = await _unitOfWork.LikeRepository.GetAllAsync( cancellationToken);

            return _mapper.Map<IEnumerable<LikeDto>>(likes);
        }

        public async Task CreateAsync(LikeDto likeForCreation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.LikeRepository.Create(_mapper.Map<Like>(likeForCreation));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new LikeCreateException("Failed to create like");
        }

        public async Task CreateAsync(Guid topicId, CancellationToken cancellationToken = default)
        {
            var username = _userAccessor.GetUsername();
            var user = await _unitOfWork.UserRepository.GetByUsername(username,  cancellationToken);
            if (user == null) throw new UserNotFound(username);

            var topic = _unitOfWork.TopicRepository.GetByIdAsync(topicId,  cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);

            var like = new LikeDto
            {
                AppUserId = user.Id,
                TopicId = topicId
            };

            await CreateAsync(like, cancellationToken);
        }

        public async Task DeleteAsync(LikeDto likeForDeletion, CancellationToken cancellationToken = default)
        {
            _unitOfWork.LikeRepository.Delete(_mapper.Map<Like>(likeForDeletion));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new LikeDeleteException("Failed to create like");
        }
    }
}