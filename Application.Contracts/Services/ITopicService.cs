using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Paging;

namespace Contracts.Services
{
    public interface ITopicService 
    {
       Task<IEnumerable<TopicDto>> GetAllByCreatorIdAsync(Guid creatorId, bool trackChanges, CancellationToken cancellationToken = default);
       
       Task<PagedList<TopicDto>> GetAllByCategoryIdAsync(Guid categoryId,PagingParams pagingParams, bool trackChanges, CancellationToken cancellationToken = default);
        
       Task<TopicDto> GetByIdAsync(Guid topicId, bool trackChanges,  CancellationToken cancellationToken = default);
       
       Task<PagedList<TopicDto>> GetAllAsync(PagingParams pagingParams, bool trackChanges, CancellationToken cancellationToken = default);

       Task CreateAsync(TopicDto topicForCreation, CancellationToken cancellationToken = default);
       
       Task DeleteAsync(TopicDto topicForDeletion, CancellationToken cancellationToken = default);
       
       Task UpdateAsync(TopicDto topicForUpdation, CancellationToken cancellationToken = default);
       
    }
}