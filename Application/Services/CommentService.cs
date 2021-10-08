using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
using Contracts.Paging;
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
        private readonly IUserAccessor _userAccessor;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<CommentDto> GetByIdAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId,  cancellationToken);

            if (comment == null) throw new CommentNotFoundException(commentId.ToString());

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var coments = await _unitOfWork.CommentRepository.GetAllAsync( cancellationToken);

            return _mapper.Map<IEnumerable<CommentDto>>(coments);
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

            var username = _userAccessor.GetUsername();

            var user = await _unitOfWork.UserRepository.GetByUsername(username, cancellationToken);
            comment.Author = user;
            _unitOfWork.CommentRepository.Create(comment);

            var result = await  _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentCreateException("Failed to create comment");

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task DeleteAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CommentRepository.Delete(new Comment{Id = commentId});

            var result = await  _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentDeleteException("Failed to delete comment");
        }

        public async Task UpdateAsync(CommentUpdateDto commentForUpdation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(commentForUpdation));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentUpdateException("Failed to update comment");
        }
    }
}