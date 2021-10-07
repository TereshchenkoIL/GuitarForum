using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task<Comment> GetByIdAsync(Guid commentId, bool trackChanges, CancellationToken cancellationToken = default);
        Task<IEnumerable<Comment>> GetAllByTopicAsync(Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default);
    }
}