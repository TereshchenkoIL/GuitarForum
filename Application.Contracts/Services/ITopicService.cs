using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Contracts.Services
{
    public interface ITopicService : IService<TopicDto>
    {
       Task<IEnumerable<TopicDto>> GetAllByCreatorIdAsync(Guid creatorId,bool trackChanges, 
           CancellationToken cancellationToken = default);
       
       Task<IEnumerable<TopicDto>> GetAllByCategoryIdAsync(Guid categoryId,bool trackChanges, CancellationToken cancellationToken = default);
        
       Task<TopicDto> GetByIdAsync(Guid topicId, bool trackChanges,  CancellationToken cancellationToken = default);
       
    }
}