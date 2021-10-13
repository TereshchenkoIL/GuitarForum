using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
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
        private readonly IUserAccessor _userAccessor;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<IEnumerable<TopicDto>> GetAllByCreatorIdAsync(string creatorId, CancellationToken cancellationToken = default)
        {
            var creator = await _unitOfWork.UserRepository.GetByUserIdAsync(creatorId,  cancellationToken);

            if (creator == null) throw new UserNotFound(creatorId);
            
            var topics = await _unitOfWork.TopicRepository.GetAllByCreatorIdAsync(creatorId,  cancellationToken);
            
            var topicsDto =_mapper.Map<IEnumerable<TopicDto>>(topics);

            await SetIsLiked( topicsDto, topics, cancellationToken);

            return topicsDto;
        }

      
        public async Task<PagedList<TopicDto>> GetAllByCategoryIdAsync(Guid categoryId, PagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId,  cancellationToken);

            if (category == null) throw new CategoryNotFoundException(categoryId);


            var topics = await _unitOfWork.TopicRepository.GetAllByCategoryIdAsync(categoryId,  cancellationToken);

            var topicsDto = _mapper.Map<IEnumerable<TopicDto>>(topics);
            
            await SetIsLiked( topicsDto, topics, cancellationToken);

            return  PagedList<TopicDto>.Create(topicsDto, pagingParams.PageNumber, pagingParams.PageSize);
        }

        private async Task SetIsLiked( IEnumerable<TopicDto> topicsDto, IEnumerable<Topic> topics, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByUsernameAsync(_userAccessor.GetUsername(), cancellationToken);

            foreach (var topic in topicsDto)
            {
                if (currentUser != null)
                {
                    topic.IsLiked = topics.First(x => x.Id == topic.Id).Likes.Any(l => l.AppUserId == currentUser.Id);
                }
            }
        }

        public async Task<TopicDto> GetByIdAsync(Guid topicId, CancellationToken cancellationToken = default)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId,  cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);
            
            var topicDto = _mapper.Map<TopicDto>(topic);
            var currentUser = await _unitOfWork.UserRepository.GetByUsernameAsync(_userAccessor.GetUsername(), cancellationToken);
            if(currentUser != null)
                topicDto.IsLiked = topic.Likes.Any(l => l.AppUserId == currentUser.Id);
            
            return topicDto;
        }

        public async Task<PagedList<TopicDto>> GetAllAsync(PagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var topics = await _unitOfWork.TopicRepository.GetAllAsync(cancellationToken);
            

            var topicsDto = _mapper.Map<IEnumerable<TopicDto>>(topics);
            
            await SetIsLiked(topicsDto, topics, cancellationToken );

            return PagedList<TopicDto>.Create(topicsDto, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task CreateAsync(TopicDto topicForCreation, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(_userAccessor.GetUsername(), cancellationToken);
            if (user == null) throw new UserNotFound(_userAccessor.GetUsername());
            
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(topicForCreation.Category.Id,cancellationToken);
            if (category == null) throw new CategoryNotFoundException(topicForCreation.Category.Id);
            
            var topic = _mapper.Map<Topic>(topicForCreation);
            topic.Creator = user;
            topic.CreatedAt = DateTime.UtcNow;
            topic.Category = category;
           _unitOfWork.TopicRepository.Create(topic);

           var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

           if (!result) throw new TopicCreateException("Failed to create topic");
        }

        public async Task DeleteAsync(Guid topicId, CancellationToken cancellationToken = default)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId, cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);
             _unitOfWork.TopicRepository.Delete(topic);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new TopicDeleteException("Failed to delete topic");
        }

        public async Task UpdateAsync(TopicDto topicForUpdation, CancellationToken cancellationToken = default)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicForUpdation.Id, cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicForUpdation.Id);

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(topicForUpdation.Category.Id, cancellationToken);
            if (category == null) throw new CategoryNotFoundException(topicForUpdation.Category.Id);
            
            topic.Body = topicForUpdation.Body;
            topic.Title = topicForUpdation.Title;
            topic.Category = category;
            
            

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new TopicUpdateException("Failed to update topic");
        }
    }
}