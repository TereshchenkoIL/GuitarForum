using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.CommentExceptions;
using Domain.Exceptions.TopicExceptions;
using Domain.Repositories;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommentDto> GetByIdAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId,  cancellationToken);

            if (comment == null) throw new CommentNotFoundException(commentId.ToString());

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync( cancellationToken);

            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<IEnumerable<CommentDto>> GetAllByTopicAsync(Guid topicId, CancellationToken cancellationToken = default)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId,  cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);

            var comments = await _unitOfWork.CommentRepository.GetAllByTopicAsync(topicId,  cancellationToken);

            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto> CreateAsync(CommentCreateDto commentForCreation, CancellationToken cancellationToken = default)
        {
            var comment = _mapper.Map<Comment>(commentForCreation);

            

            var user = await _unitOfWork.UserRepository.GetByUsername(commentForCreation.Username, cancellationToken);
           
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(commentForCreation.TopicId, cancellationToken);
            if (topic == null) throw new TopicNotFoundException(commentForCreation.TopicId);
             comment.Author = user;
             comment.Topic = topic;
            _unitOfWork.CommentRepository.Create(comment);

            var result = await  _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentCreateException("Failed to create comment");

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task DeleteAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId, cancellationToken);
            _unitOfWork.CommentRepository.Delete(comment);

            var result = await  _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentDeleteException("Failed to delete comment");
        }

        public async Task UpdateAsync(CommentUpdateDto commentForUpdation, CancellationToken cancellationToken = default)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentForUpdation.Id, cancellationToken);

            if (comment == null) throw new CommentNotFoundException("unknown", commentForUpdation.Body);

            _mapper.Map(commentForUpdation, comment);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentUpdateException("Failed to update comment");
        }
    }
}