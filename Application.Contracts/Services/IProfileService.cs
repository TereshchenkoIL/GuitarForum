﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IProfileService
    {
        Task<Contracts.Profile> GetDetails(string username, CancellationToken cancellationToken = default);

        Task<IEnumerable<TopicDto>> GetTopics(string username, CancellationToken cancellationToken = default);

        Task UpdateAsync(string displayName, string bio, CancellationToken cancellationToken = default);
        
        Task UpdateAsync(string username, string displayName, string bio, CancellationToken cancellationToken = default);

    }
}