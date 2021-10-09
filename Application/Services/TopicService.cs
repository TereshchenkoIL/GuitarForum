using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Paging;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.CategoryExceptions;
using Domain.Exceptions.TopicExceptions;
using Domain.Exceptions.UserException;
using Domain.Repositories;

namespace Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopicDto>> GetAllByCreatorIdAsync(string creatorId, CancellationToken cancellationToken = default)
        {
            var creator = await _unitOfWork.UserRepository.GetByUserId(creatorId,  cancellationToken);

            if (creator == null) throw new UserNotFound(creatorId);
            
            var topics = await _unitOfWork.TopicRepository.GetAllByCreatorIdAsync(creatorId,  cancellationToken);
            return _mapper.Map<IEnumerable<TopicDto>>(topics);
        }

        public async Task<IEnumerable<TopicDto>> GetAllByCreatorUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(username,  cancellationToken);

            if (user == null) throw new UserNotFound(username);

            return await GetAllByCreatorIdAsync(user.Id,  cancellationToken);

        }

        public async Task<PagedList<TopicDto>> GetAllByCategoryIdAsync(Guid categoryId, PagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId,  cancellationToken);

            if (category == null) throw new CategoryNotFoundException(categoryId);


            var topics = await _unitOfWork.TopicRepository.GetAllByCategoryIdAsync(categoryId,  cancellationToken);

            var topicsDto = _mapper.Map<IEnumerable<TopicDto>>(topics);

            return  PagedList<TopicDto>.Create(topicsDto, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<TopicDto> GetByIdAsync(Guid topicId, CancellationToken cancellationToken = default)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId,  cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);
            
            return _mapper.Map<TopicDto>(topic);
        }

        public async Task<PagedList<TopicDto>> GetAllAsync(PagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var topics = await _unitOfWork.TopicRepository.GetAllAsync(cancellationToken);

            var topicsDto = _mapper.Map<IEnumerable<TopicDto>>(topics);

            return PagedList<TopicDto>.Create(topicsDto, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task CreateAsync(TopicDto topicForCreation, CancellationToken cancellationToken = default)
        {
           _unitOfWork.TopicRepository.Create(entity: _mapper.Map<Topic>(topicForCreation));

           var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

           if (!result) throw new TopicCreateException("Failed to create topic");
        }

        public async Task DeleteAsync(TopicDto topicForDeletion, CancellationToken cancellationToken = default)
        {
            _unitOfWork.TopicRepository.Delete(entity: _mapper.Map<Topic>(topicForDeletion));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new TopicDeleteException("Failed to delete topic");
        }

        public async Task UpdateAsync(TopicDto topicForUpdation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.TopicRepository.Create(entity: _mapper.Map<Topic>(topicForUpdation));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new TopicUpdateException("Failed to update topic");
        }
    }
}