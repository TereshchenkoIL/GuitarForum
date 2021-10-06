using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ILikeRepository : IRepositoryBase<Like>
    {
        Task<Like> GetLike(Guid appUserId, Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default);
    }
}