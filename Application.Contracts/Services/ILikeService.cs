using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Contracts.Services
{
    public interface ILikeService : IService<LikeDto>
    {
        Task<Like> GetLike(Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default);
    }
}