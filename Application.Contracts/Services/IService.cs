using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);

        Task CreateAsync(T topicForCreation, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(T topicForCreation, CancellationToken cancellationToken = default);
       
        Task UpdateAsync(T topicForCreation, CancellationToken cancellationToken = default);
    }
}