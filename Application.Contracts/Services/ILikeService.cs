using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Contracts.Services
{
    public interface ILikeService 
    {
        
        Task<LikeDto> GetLike(string userId, Guid topicId, 
            CancellationToken cancellationToken = default);
        
        Task<IEnumerable<LikeDto>> GetAllAsync( CancellationToken cancellationToken = default);
        
        Task CreateAsync(LikeDto likeForCreation, CancellationToken cancellationToken = default);
        Task CreateAsync(Guid topicId, CancellationToken cancellationToken = default);
        Task DeleteAsync(LikeDto likeForDeletion, CancellationToken cancellationToken = default);
    }
}