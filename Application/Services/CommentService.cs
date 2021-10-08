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
            var coments = await _unitOfWork.CommentRepository.GetAllAsync( cancellationToken);

            return _mapper.Map<IEnumerable<CommentDto>>(coments);
        }

        public async Task<PagedList<CommentDto>> GetAllByTopicAsync(Guid topicId, PagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId,  cancellationToken);

            if (topic == null) throw new TopicNotFoundException(topicId);

            var comments = await _unitOfWork.CommentRepository.GetAllByTopicAsync(topicId,  cancellationToken);

            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return new PagedList<CommentDto>(commentsDto, commentsDto.Count(), pagingParams.PageNumber,
                pagingParams.PageSize);
        }

        public async Task CreateAsync(CommentDto commentForCreation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CommentRepository.Create(_mapper.Map<Comment>(commentForCreation));

            var result = await  _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentCreateException("Failed to create comment");
        }

        public async Task DeleteAsync(CommentDto commentForDeletion, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CommentRepository.Delete(_mapper.Map<Comment>(commentForDeletion));

            var result = await  _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentDeleteException("Failed to delete comment");
        }

        public async Task UpdateAsync(CommentDto commentForUpdation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(commentForUpdation));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CommentUpdateException("Failed to update comment");
        }
    }
}