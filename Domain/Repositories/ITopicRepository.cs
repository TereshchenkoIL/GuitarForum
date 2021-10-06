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
        Task<IQueryable<Topic>> GetAllByCreatorIdAsync(Guid creatorId,bool trackChanges, CancellationToken cancellationToken = default);
        
        Task<IQueryable<Topic>> GetAllByCategoryIdAsync(Guid categoryId,bool trackChanges, CancellationToken cancellationToken = default);
        
        Task<Topic> GetByIdAsync(Guid topicId, bool trackChanges,  CancellationToken cancellationToken = default);
        
        
    }
}