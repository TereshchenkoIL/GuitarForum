using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Contracts.Services
{
    public interface ILikeService 
    {
        Task<LikeDto> GetLike(Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default);
        
        Task<IEnumerable<LikeDto>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);
        
        Task CreateAsync(LikeDto lokeForCreation, CancellationToken cancellationToken = default);
        Task DeleteAsync(LikeDto likeForDeletion, CancellationToken cancellationToken = default);
    }
}