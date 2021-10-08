using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.DTO;
using Contracts.Paging;

namespace Contracts.Services
{
    public interface ICommentService
    {
       Task<CommentDto> GetByIdAsync(Guid commentId,  CancellationToken cancellationToken = default);
    
       Task<IEnumerable<CommentDto>> GetAllAsync( CancellationToken cancellationToken = default);
       Task<IEnumerable<CommentDto>> GetAllByTopicAsync(Guid topicId,  CancellationToken cancellationToken = default);

       Task<CommentDto> CreateAsync(CommentCreateDto commentForCreation, CancellationToken cancellationToken = default);
       
       Task DeleteAsync(Guid commentId, CancellationToken cancellationToken = default);
       
       Task UpdateAsync(CommentUpdateDto commentForUpdation, CancellationToken cancellationToken = default);
    }
}