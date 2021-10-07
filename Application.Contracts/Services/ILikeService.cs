using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Contracts.Services
{
    public interface ILikeService 
    {
        Task<Like> GetLike(Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default);
        
        Task<IEnumerable<LikeDto>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);
        
        Task CreateAsync(LikeDto lokeForCreation, CancellationToken cancellationToken = default);
        Task DeleteAsync(LikeDto likeForDeletion, CancellationToken cancellationToken = default);
    }
}