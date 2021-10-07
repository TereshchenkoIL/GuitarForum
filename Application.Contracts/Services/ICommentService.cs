using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Contracts.Services
{
    public interface ICommentService
    {
       Task<CommentDto> GetByIdAsync(Guid commentId, bool trackChanges, CancellationToken cancellationToken = default);
    
       Task<IEnumerable<CommentDto>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);

       Task CreateAsync(CommentDto commentForCreation, CancellationToken cancellationToken = default);
       
       Task DeleteAsync(CommentDto commentForDeletion, CancellationToken cancellationToken = default);
       
       Task UpdateAsync(CommentDto commentForUpdation, CancellationToken cancellationToken = default);
    }
}