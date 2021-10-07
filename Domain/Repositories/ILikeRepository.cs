using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ILikeRepository : IRepositoryBase<Like>
    {
        Task<Like> GetLike(string appUserId, Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default);
    }
}