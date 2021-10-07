using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Paging;

namespace Contracts.Services
{
    public interface ICommentService
    {
       Task<CommentDto> GetByIdAsync(Guid commentId,  CancellationToken cancellationToken = default);
    
       Task<IEnumerable<CommentDto>> GetAllAsync( CancellationToken cancellationToken = default);
       Task<PagedList<CommentDto>> GetAllByTopicAsync(Guid topicId, PagingParams pagingParams, CancellationToken cancellationToken = default);

       Task CreateAsync(CommentDto commentForCreation, CancellationToken cancellationToken = default);
       
       Task DeleteAsync(CommentDto commentForDeletion, CancellationToken cancellationToken = default);
       
       Task UpdateAsync(CommentDto commentForUpdation, CancellationToken cancellationToken = default);
    }
}