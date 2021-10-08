using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITopicRepository : IRepositoryBase<Topic>
    {
        Task<IEnumerable<Topic>> GetAllByCreatorIdAsync(string creatorId, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<Topic>> GetAllByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        
        Task<Topic> GetByIdAsync(Guid topicId,  CancellationToken cancellationToken = default);
        
        
    }
}