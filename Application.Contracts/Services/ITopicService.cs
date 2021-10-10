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
       Task<IEnumerable<TopicDto>> GetAllByCreatorIdAsync(string creatorId, CancellationToken cancellationToken = default);
       Task<IEnumerable<TopicDto>> GetAllByCreatorUsernameAsync(string username, CancellationToken cancellationToken = default);
       
       Task<PagedList<TopicDto>> GetAllByCategoryIdAsync(Guid categoryId, PagingParams pagingParams, CancellationToken cancellationToken = default);
        
       
       Task<TopicDto> GetByIdAsync(Guid topicId,  CancellationToken cancellationToken = default);
       
       Task<PagedList<TopicDto>> GetAllAsync(PagingParams pagingParams, CancellationToken cancellationToken = default);

       Task CreateAsync(TopicDto topicForCreation, CancellationToken cancellationToken = default);
       
       Task DeleteAsync(Guid topicId, CancellationToken cancellationToken = default);
       
       Task UpdateAsync(TopicDto topicForUpdation, CancellationToken cancellationToken = default);
       
    }
}