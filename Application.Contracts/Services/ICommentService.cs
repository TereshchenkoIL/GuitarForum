using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface ICommentService : IService<CommentDto>
    {
       Task<CommentDto> GetByIdAsync(Guid commentId, bool trackChanges, CancellationToken cancellationToken = default);
    }
}